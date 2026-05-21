import { Fragment, useState, useRef } from "react";
import { useTranslation } from "react-i18next";
import { Button } from "primereact/button";
import { useForm, Controller } from "react-hook-form";
import { Dialog } from "primereact/dialog";
import { Dropdown } from "primereact/dropdown";
import { InputText } from "primereact/inputtext";
import { InputNumber } from "primereact/inputnumber";
import { InputTextarea } from "primereact/inputtextarea";
import { classNames } from "primereact/utils";
import {
  getSparePart,
  deleteSparePart,
  createNewSparePart,
  updateSparePart,
  getAllSparePart,
} from "../../services/spare-part-service";

import AppDataTable from "../../components/tables/AppDataTable";

const emptySparePart = {
  ProductCode: "",
  ProductName: "",
  UnitName: "",
  UnitPrice: 0,
  Quantity: 0,
  Remark: "",
};

const SparePartPage = () => {
  const { t } = useTranslation();
  const units = [
    t("units.box"),
    t("units.piece"),
    t("units.liter"),
    t("units.kg"),
    t("units.bottle"),
    t("units.set"),
    t("units.can"),
  ];
  const formRef = useRef();
  const [spareParts, setSpareParts] = useState(null);
  const [showSparePartDetailDialog, setShowSparePartDetailDialog] =
    useState(false);
  const [isShowCancelDialog, setIsShowCancelDialog] = useState(false);
  const [paginatorOptions, setPaginatorOptions] = useState();
  const {
    control,
    formState: { errors, isDirty },
    handleSubmit,
    reset,
  } = useForm({ emptySparePart });

  const getData = (pageSize, pageIndex, keyword) => {
    getSparePart(pageSize, pageIndex, keyword).then((response) => {
      const { Data, ...paginatorOptions } = response.data.Result;
      paginatorOptions.keyword = keyword;
      setPaginatorOptions(paginatorOptions);
      setSpareParts(Data);
    });
  };

  const refreshData = () => {
    getSparePart(
      paginatorOptions.PageSize,
      paginatorOptions.PageIndex,
      paginatorOptions.keyword
    ).then((response) => {
      const data = response.data.Result.Data;
      setSpareParts(data);
    });
  };

  const columns = [
    {
      field: "ProductCode",
      header: t("sparePart.productCode"),
    },
    {
      field: "ProductName",
      header: t("sparePart.description"),
    },
    {
      field: "UnitName",
      header: t("table.unit"),
    },
    {
      field: "UnitPrice",
      header: t("table.unitPrice"),
      style: {
        width: "10rem",
      },
      body: (rowData) => {
        return (
          <div className="text-right">
            {new Intl.NumberFormat("vi-VN").format(rowData.UnitPrice)}
          </div>
        );
      },
    },
    {
      field: "Quantity",
      header: t("sparePart.stockQuantity"),
      style: {
        width: "10rem",
      },
      body: (rowData) => {
        return (
          <div className="text-right">
            {new Intl.NumberFormat("vi-VN").format(rowData.Quantity)}
          </div>
        );
      },
    },
    {
      field: "Remark",
      header: t("table.note"),
    },
  ];

  const onDeletedSparePart = (selectedSparePart) => {
    deleteSparePart(selectedSparePart.ProductId).then(() => {
      refreshData();
    });
  };

  const onUpdateSparePart = (rowData) => {
    reset(rowData);
    setShowSparePartDetailDialog(true);
  };

  const onCreateNewSparePart = () => {
    reset(emptySparePart);
    setShowSparePartDetailDialog(true);
  };

  const onSubmit = (formData) => {
    if (formData.ProductId) {
      updateSparePart(formData).then((response) => {
        const {
          data: { IsSuccess },
        } = response;
        if (IsSuccess) {
          refreshData();
          setShowSparePartDetailDialog(false);
        }
      });
      return;
    }

    createNewSparePart(formData).then((response) => {
      const {
        data: { IsSuccess },
      } = response;
      if (IsSuccess) {
        refreshData();
        setShowSparePartDetailDialog(false);
      }
    });
  };

  const getFormErrorMessage = (name) => {
    return (
      errors[name] && <small className="p-error">{errors[name].message}</small>
    );
  };

  const onSparePartCancel = () => {
    if (isDirty) {
      setIsShowCancelDialog(true);
      return;
    }
    setShowSparePartDetailDialog(false);
  };

  const sparePartDialogFooter = (
    <Fragment>
      <Button
        label={t("common.cancel")}
        icon="pi pi-times"
        className="p-button-text"
        onClick={onSparePartCancel}
      />
      <Button
        label={t("common.save")}
        disabled={!isDirty}
        icon="pi pi-check"
        className="p-button-text"
        onClick={() => formRef.current.requestSubmit()}
      />
    </Fragment>
  );

  const cancelConfirmDialogFooter = (
    <Fragment>
      <Button
          label={t("common.cancel")}
        icon="pi pi-times"
        className="p-button-text"
        onClick={() => {
          setIsShowCancelDialog(false);
        }}
      />
      <Button
          label={t("common.cancel")}
        icon="pi pi-check"
        className="p-button-text"
        onClick={() => {
          setIsShowCancelDialog(false);
          setShowSparePartDetailDialog(false);
        }}
      />
    </Fragment>
  );

  return (
    <Fragment>
      <AppDataTable
        data={spareParts}
        columns={columns}
        dataKey="ProductId"
      title={t("sparePart.title")}
        deleteSelectedItem={onDeletedSparePart}
        createNewItem={onCreateNewSparePart}
        updateItem={onUpdateSparePart}
        excelExportable={true}
      excelFileName={t("sparePart.title")}
        paginatorOptions={paginatorOptions}
        fnGetData={getData}
        fnGetAllDataForExport={getAllSparePart}
      />

      <Dialog
        visible={showSparePartDetailDialog}
        style={{ width: "450px" }}
        header={t("sparePart.detailTitle")}
        modal
        className="p-fluid"
        footer={sparePartDialogFooter}
        onHide={onSparePartCancel}
      >
        <form ref={formRef} onSubmit={handleSubmit(onSubmit)}>
          <div className="field">
            <label htmlFor="ProductCode">
              {t("sparePart.productCode")} <b className="p-error">*</b>
            </label>
            <Controller
              name="ProductCode"
              control={control}
              rules={{ required: t("sparePart.productCodeRequired") }}
              render={({ field, fieldState }) => (
                <InputText
                  id={field.name}
                  {...field}
                  className={classNames("w-full", {
                    "p-invalid": fieldState.error,
                  })}
                />
              )}
            />
            {getFormErrorMessage("ProductCode")}
          </div>

          <div className="field">
            <label htmlFor="ProductName">
              {t("sparePart.description")} <b className="p-error">*</b>
            </label>
            <Controller
              name="ProductName"
              control={control}
              rules={{ required: t("sparePart.descriptionRequired") }}
              render={({ field, fieldState }) => (
                <InputText
                  id={field.name}
                  {...field}
                  className={classNames({
                    "p-invalid": fieldState.error,
                  })}
                />
              )}
            />
            {getFormErrorMessage("ProductName")}
          </div>

          <div className="field">
            <label htmlFor="UnitPrice">
              {t("table.unitPrice")} <b className="p-error">*</b>
            </label>
            <Controller
              name="UnitPrice"
              control={control}
              rules={{
                required: t("sparePart.unitPriceRequired")
              }}
              render={({ field, fieldState }) => (
                <InputNumber
                  id={field.name}
                  ref={field._f?.ref}
                  value={field.value}
                  onBlur={field.onBlur}
                  onValueChange={(e) => {
                    field.onChange(e);
                  }}
                  min={0}
                  mode="currency"
                  currency="VND"
                  currencyDisplay="code"
                  locale="vi-VN"
                  className={classNames({
                    "p-invalid": fieldState.error,
                  })}
                />
              )}
            />
            {getFormErrorMessage("UnitPrice")}
          </div>

          <div className="field">
            <label htmlFor="UnitName">
              {t("table.unit")} <b className="p-error">*</b>
            </label>
            <Controller
              name="UnitName"
              control={control}
              rules={{ required: t("sparePart.unitNameRequired") }}
              render={({ field, fieldState }) => (
                <Dropdown
                  id={field.name}
                  value={field.value}
                  onChange={(e) => field.onChange(e.value)}
                  options={units}
                  className={classNames({
                    "p-invalid": fieldState.error,
                  })}
                  placeholder={t("sparePart.selectUnit")}
                />
              )}
            />
            {getFormErrorMessage("UnitName")}
          </div>

          <div className="field">
            <label htmlFor="Quantity">{t("sparePart.stockQuantity")}</label>
            <Controller
              name="Quantity"
              control={control}
              render={({ field }) => (
                <InputNumber
                  id={field.name}
                  value={field.value}
                  onBlur={field.onBlur}
                  onValueChange={(e) => field.onChange(e)}
                  min={0}
                  locale="vi-VN"
                />
              )}
            />
          </div>

          <div className="field">
            <label htmlFor="Remark">{t("table.note")}</label>
            <Controller
              name="Remark"
              control={control}
              render={({ field }) => (
                <InputTextarea rows={5} cols={30} id={field.name} {...field} />
              )}
            />
          </div>
        </form>
      </Dialog>

      <Dialog
        visible={isShowCancelDialog}
        style={{ width: "450px" }}
        header={t("common.confirm")}
        modal
        footer={cancelConfirmDialogFooter}
        onHide={() => {
          setIsShowCancelDialog(false);
        }}
      >
        <div className="confirmation-content">
          <i
            className="pi pi-exclamation-triangle mr-3"
            style={{ fontSize: "2rem" }}
          />
          {t("common.cancelConfirmation")}
        </div>
      </Dialog>
    </Fragment>
  );
};

export default SparePartPage;
