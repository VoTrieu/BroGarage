import { Fragment, useState } from "react";
import { useTranslation } from "react-i18next";
import { Fieldset } from "primereact/fieldset";
import { Calendar } from "primereact/calendar";
import { Button } from "primereact/button";
import { Link } from "react-router-dom";
import { classNames } from "primereact/utils";
import { useForm, Controller } from "react-hook-form";
import CarAutoComplete from "../../components/auto-complete/CarAutoComplete";
import CustomerAutoComplete from "../../components/auto-complete/CustomerAutoComplete";
import StatusDropdown from "../../components/dropdown/StatusDropdown";
import AppDataTable from "../../components/tables/AppDataTable";
import classes from "../repair-form/RepairForm.module.scss";
import {
  getRepairForms,
  getRepairFormsExport,
} from "../../services/repair-service";
import { getDateWithFormat } from "../../utils/Utils";

const defaultValues = {
  customerId: null,
  carId: null,
  createdFromDate: undefined,
  createdToDate: undefined,
  statusId: null,
  typeId: null,
  dateInFrom: undefined,
  dateInTo: undefined,
  dateOutFrom: undefined,
  dateOutTo: undefined,
};

const ReportPage = () => {
  const { t } = useTranslation();
  const [repairForms, setRepairForms] = useState([]);
  const [paginatorOptions, setPaginatorOptions] = useState();

  const {
    control,
    formState: { errors, isDirty, isValid },
    handleSubmit,
    setValue,
    getValues,
    reset,
  } = useForm({ defaultValues });

  const getFormErrorMessage = (name) => {
    return (
      errors[name] && <small className="p-error">{errors[name].message}</small>
    );
  };

  const validateFromDateToDate = (
    value,
    compareField,
    isFromDate,
    fieldLabel
  ) => {
    const compareDate = getValues()[compareField];
    if (!value && !compareDate) {
      return true;
    }

    if (!value && compareDate) {
      return t("report.required", { field: fieldLabel });
    }

    if (isFromDate && value > compareDate) {
      return t("report.dateRangeFromInvalid", { field: fieldLabel });
    }

    if (!isFromDate && value < compareDate) {
      return t("report.dateRangeToInvalid", { field: fieldLabel });
    }
  };

  const statusBodyTemplate = (rowData) => {
    return (
      <span
        className={classNames(
          classes.status_badge,
          classes[`status_${rowData.StatusId}`]
        )}
      >
        {rowData.StatusName}
      </span>
    );
  };

  const columns = [
    {
      field: "OrderId",
      header: t("report.orderCode"),
      body: (rowData) => {
        return (
          <Link to={`/app/repair-detail/${rowData.OrderId}`}>
            {rowData.OrderId}
          </Link>
        );
      },
    },
    {
      field: "Car.LicensePlate",
      header: t("report.licensePlate"),
    },
    {
      field: "Car.ManufacturerName",
      header: t("report.manufacturer"),
    },
    {
      field: "Car.TypeName",
      header: t("report.carType"),
    },
    {
      field: "Car.Customer.FullName",
      header: t("report.customerName"),
      body: (rowData) => {
        return (
          <Link to={`/app/customer-detail/${rowData.Car.Customer.CustomerId}`}>
            {rowData.Car.Customer.FullName}
          </Link>
        );
      },
    },
    {
      field: "StatusName",
      header: t("report.status"),
      body: statusBodyTemplate,
    },
    {
      field: "OrderDate",
      header: t("report.createdDate"),
    },
  ];

  const exportReport = (keyword) => {
    const { FullName, LicensePlate, ...searchParameters } = getValues();
    return getRepairFormsExport(keyword, searchParameters);
  };

  const getData = (pageSize, pageIndex, keyword) => {
    const { FullName, LicensePlate, ...searchParameters } = getValues();
    getRepairForms(pageSize, pageIndex, keyword, searchParameters).then(
      (response) => {
        const { Data, ...paginatorOptions } = response.data.Result;
        setPaginatorOptions(paginatorOptions);
        setRepairForms(Data);
      }
    );
  };

  const onSubmit = (data) => {
    const { pageSize, pageIndex } = paginatorOptions;
    const { FullName, LicensePlate, ...searchParameters } = data;
    getRepairForms(pageSize, pageIndex, "", searchParameters).then(
      (response) => {
        if (!response.data.IsSuccess) {
          return;
        }
        const { Data, ...paginatorOptions } = response.data.Result;
        setPaginatorOptions(paginatorOptions);
        setRepairForms(Data);
      }
    );
  };
  return (
    <Fragment>
      <Fieldset className="mb-4" legend={t("report.searchInfo")} collapsed={true} toggleable>
        <form onSubmit={handleSubmit(onSubmit)}>
          <div className="formgrid grid">
            <div className="field col-12 md:col-4">
              <label htmlFor="LicensePlate">{t("report.licensePlate")}</label>
              <Controller
                name="LicensePlate"
                control={control}
                render={({ field, fieldState }) => (
                  <CarAutoComplete
                    field={field}
                    fieldState={fieldState}
                    setValue={setValue}
                  />
                )}
              />
            </div>

            <div className="field col-12 md:col-4">
              <label htmlFor="FullName">{t("report.customerName")}</label>
              <Controller
                name="FullName"
                control={control}
                render={({ field, fieldState }) => (
                  <CustomerAutoComplete
                    field={field}
                    fieldState={fieldState}
                    setValue={setValue}
                  />
                )}
              />
            </div>

            <div className="field col-12 md:col-4">
              <label htmlFor="StatusId">{t("report.status")}</label>
              <Controller
                name="StatusId"
                control={control}
                render={({ field, fieldState }) => (
                  <StatusDropdown field={field} fieldState={fieldState} />
                )}
              />
            </div>

            <div className="field col-12 md:col-4">
              <label htmlFor="createdFromDate">{t("report.createdDate")}</label>
              <Controller
                name="createdFromDate"
                control={control}
                rules={{
                  validate: {
                    isDateValid: (value) =>
                      validateFromDateToDate(
                        value,
                        "createdToDate",
                        true,
                        t("report.createdDate")
                      ),
                  },
                }}
                render={({ field, fieldState }) => (
                  <Calendar
                    value={field.value}
                    onChange={(date) =>
                      field.onChange(getDateWithFormat(date.value))
                    }
                    showIcon
                    autoFocus
                    dateFormat="dd/mm/yy"
                    className={classNames("w-full", {
                      "p-invalid": fieldState.error,
                    })}
                  />
                )}
              />
              {getFormErrorMessage("createdFromDate")}
            </div>

            <div className="field col-12 md:col-4">
              <label htmlFor="createdToDate">{t("report.createdDate")}</label>
              <Controller
                name="createdToDate"
                control={control}
                rules={{
                  validate: {
                    isDateValue: (value) =>
                      validateFromDateToDate(
                        value,
                        "createdFromDate",
                        false,
                        t("report.createdDate")
                      ),
                  },
                }}
                render={({ field, fieldState }) => (
                  <Calendar
                    value={field.value}
                    onChange={(date) =>
                      field.onChange(getDateWithFormat(date.value))
                    }
                    showIcon
                    dateFormat="dd/mm/yy"
                    className={classNames("w-full", {
                      "p-invalid": fieldState.error,
                    })}
                  />
                )}
              />
              {getFormErrorMessage("createdToDate")}
            </div>

            <div className="col-12 md:col-4"></div>

            <div className="field col-12 md:col-4">
              <label htmlFor="dateInFrom">{t("report.dateInFrom")}</label>
              <Controller
                name="dateInFrom"
                control={control}
                rules={{
                      validate: {
                        isDateValue: (value) =>
                          validateFromDateToDate(
                            value,
                            "dateInTo",
                            true,
                            t("report.dateIn")
                          ),
                      },
                }}
                render={({ field, fieldState }) => (
                  <Calendar
                    value={field.value}
                    onChange={(date) =>
                      field.onChange(getDateWithFormat(date.value))
                    }
                    showIcon
                    autoFocus
                    dateFormat="dd/mm/yy"
                    className={classNames("w-full", {
                      "p-invalid": fieldState.error,
                    })}
                  />
                )}
              />
              {getFormErrorMessage("dateInFrom")}
            </div>

            <div className="field col-12 md:col-4">
              <label htmlFor="dateInTo">{t("report.dateInTo")}</label>
              <Controller
                name="dateInTo"
                control={control}
                rules={{
                      validate: {
                        isDateValue: (value) =>
                          validateFromDateToDate(
                            value,
                            "dateInFrom",
                            false,
                            t("report.dateIn")
                          ),
                      },
                }}
                render={({ field, fieldState }) => (
                  <Calendar
                    value={field.value}
                    onChange={(date) =>
                      field.onChange(getDateWithFormat(date.value))
                    }
                    showIcon
                    dateFormat="dd/mm/yy"
                    className={classNames("w-full", {
                      "p-invalid": fieldState.error,
                    })}
                  />
                )}
              />
              {getFormErrorMessage("dateInTo")}
            </div>

            <div className="col-12 md:col-4"></div>

            <div className="field col-12 md:col-4">
              <label htmlFor="dateOutFrom">{t("report.dateOutFrom")}</label>
              <Controller
                name="dateOutFrom"
                control={control}
                rules={{
                      validate: {
                        isDateValue: (value) =>
                          validateFromDateToDate(
                            value,
                            "dateOutTo",
                            true,
                            t("report.dateOut")
                          ),
                      },
                }}
                render={({ field, fieldState }) => (
                  <Calendar
                    value={field.value}
                    onChange={(date) =>
                      field.onChange(getDateWithFormat(date.value))
                    }
                    showIcon
                    autoFocus
                    dateFormat="dd/mm/yy"
                    className={classNames("w-full", {
                      "p-invalid": fieldState.error,
                    })}
                  />
                )}
              />
              {getFormErrorMessage("dateOutFrom")}
            </div>

            <div className="field col-12 md:col-4">
              <label htmlFor="dateOutTo">{t("report.dateOutTo")}</label>
              <Controller
                name="dateOutTo"
                control={control}
                rules={{
                      validate: {
                        isDateValue: (value) =>
                          validateFromDateToDate(
                            value,
                            "dateOutFrom",
                            false,
                            t("report.dateOut")
                          ),
                      },
                }}
                render={({ field, fieldState }) => (
                  <Calendar
                    value={field.value}
                    onChange={(date) =>
                      field.onChange(getDateWithFormat(date.value))
                    }
                    showIcon
                    dateFormat="dd/mm/yy"
                    className={classNames("w-full", {
                      "p-invalid": fieldState.error,
                    })}
                  />
                )}
              />
              {getFormErrorMessage("dateOutTo")}
            </div>
          </div>

          <div className="text-right">
            <Button
              disabled={!isDirty}
              type="button"
              label={t("report.reset")}
              icon="pi pi-refresh"
              className="mt-2 mr-4"
              onClick={() => reset(defaultValues)}
            />
            <Button
              disabled={!isValid || !isDirty}
              type="submit"
              label={t("report.search")}
              icon="pi pi-search"
              className="mt-2"
            />
          </div>
        </form>
      </Fieldset>

      <AppDataTable
        data={repairForms}
        columns={columns}
        dataKey="TemplateId"
        title={t("report.title")}
        isHideBodyActions={true}
        isHideCreateButton={true}
        excelExportable={true}
        excelFileName={t("report.report")}
        paginatorOptions={paginatorOptions}
        fnGetData={getData}
        fnGetAllDataForExport={exportReport}
      />
    </Fragment>
  );
};

export default ReportPage;
