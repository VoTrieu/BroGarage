import { Fragment, useState, useRef, useEffect } from "react";
import { useTranslation } from "react-i18next";
import { useForm, Controller } from "react-hook-form";
import { useParams, useNavigate } from "react-router-dom";
import ToggleablePanel from "../../components/panels/ToogleablePanel";
import { InputNumber } from "primereact/inputnumber";
import { Dropdown } from "primereact/dropdown";
import { InputText } from "primereact/inputtext";
import { useReactToPrint } from "react-to-print";
import { classNames } from "primereact/utils";
import {
  createRepairForm,
  getRepairFormDetail,
  updateRepairForm,
} from "../../services/repair-service";
import {
  getCurrentDate,
  getExpiredDate,
  getDateWithFormat,
  convertDDMMYYY_To_MMDDYYYY,
} from "../../utils/Utils";
import { Calendar } from "primereact/calendar";
import { Checkbox } from "primereact/checkbox";
import { InputTextarea } from "primereact/inputtextarea";
import Footer from "../../components/layout/footer/Footer";
import CarAutoComplete from "../../components/auto-complete/CarAutoComplete";
import MaintainanceCycleAutoComplete from "../../components/auto-complete/MaintainanceCycleAutoComplete";
import StatusDropdown from "../../components/dropdown/StatusDropdown";
import RepairTypeDropdown from "../../components/dropdown/RepairTypeDropdown";
import SparePartTable from "../../components/tables/SparePartTable";
import RepairFormPdf from "../repair-form/RepairFormPdf";

const defaultValues = {
  CarId: null,
  StatusId: 1,
  OrderCode: "",
  OrderDate: getCurrentDate(),
  DateIn: getCurrentDate(),
  DateOutEstimated: "",
  ODOCurrent: 0,
  ODONext: 5000,
  ODOUnit: "Km",
  ExpiredInDate: getExpiredDate(15),
  IsInvoice: false,
  AdvancePayment: 0,
  PaymentMethod: "CASH",
  Diagnosis: "",
  CustomerNote: "",
  InternalNote: "",
  Car: {
    LicensePlate: "",
  },
  OrderDetails: null,
};

const RepairFormDetailPage = () => {
  const { t } = useTranslation();

  const navigate = useNavigate();
  const {
    control,
    formState: { errors, isDirty },
    handleSubmit,
    setValue,
    getValues,
    trigger,
    reset,
  } = useForm({ defaultValues });

  const formRef = useRef();
  const printComponentRef = useRef();
  const [selectedCar, setSelectedCar] = useState({});
  const [sparePartFromTemplate, setSparePartFromTemplate] = useState([]);
  const [advancePayment, setAdvancePayment] = useState(0);
  const [isCountTax, setIsCountTax] = useState(getValues("IsInvoice"));
  const [discountPercent, setDiscountPercent] = useState();
  const [printData, setPrintData] = useState(null);
  const [isProcessing, setIsProcessing] = useState({
    printing: false,
    saving: false,
  });
  const params = useParams();
  const selectedRepairFormId = params?.id;
  const orderId = useRef("");

  //get repairForm Detail
  useEffect(() => {
    if (selectedRepairFormId) {
      getRepairFormDetail(selectedRepairFormId).then((response) => {
        const data = response.data.Result;
        orderId.current = data.OrderId;
        setSparePartFromTemplate(data.OrderDetails);
        setIsCountTax(data.IsInvoice);
        setDiscountPercent(data.Discount);
        setAdvancePayment(data.AdvancePayment);
        setSelectedCar(data.Car);

        //convert date to IsoDateTime
        data.DateOutEstimated = convertDDMMYYY_To_MMDDYYYY(
          data.DateOutEstimated
        );
        if (data.DateOutActual) {
          data.DateOutActual = convertDDMMYYY_To_MMDDYYYY(data.DateOutActual);
        }
        reset(data);
      });
    }
  }, [reset, selectedRepairFormId]);

  const getFormErrorMessage = (name) => {
    return (
      errors[name] && <small className="p-error">{errors[name].message}</small>
    );
  };

  const updateNextODO = (value) => {
    if (value === null) {
      setValue("ODONext", 0);
      return;
    }
    const unit = getValues("ODOUnit");
    const currentODO = getValues("ODOCurrent");
    if (unit === "Km") {
      setValue("ODONext", currentODO + 5000);
    } else {
      setValue("ODONext", currentODO + 3100);
    }
  };

  const onMaintainanceCycleSelect = (maintainanceCycle) => {
    const sparePartList = maintainanceCycle.TemplateDetails;
    const existedSpareParts = getValues("OrderDetails");
    const updatedSparePartList = [...existedSpareParts, ...sparePartList];
    setSparePartFromTemplate(updatedSparePartList);
    setValue("TemplateId", maintainanceCycle.TemplateId);
  };

  const onHandleSparePartsChange = (spareParts) => {
    setValue("OrderDetails", spareParts, { shouldDirty: true });
  };

  const handlePrint = useReactToPrint({
    content: () => printComponentRef.current,
    documentTitle: orderId.current
  });

  const functionButtons = [
    {
      label: t("repairForm.printPdf"),
      icon: "pi pi-print",
      className: "p-button-success",
      disabled: !selectedRepairFormId || isProcessing.printing,
      action: async () => {
        setIsProcessing({ ...isProcessing, printing: true });
        const res = await getRepairFormDetail(selectedRepairFormId);
        const data = res.data.Result;
        setPrintData(data);
        //wait for printData updated
        setTimeout(() => {
          handlePrint();
          setIsProcessing({ ...isProcessing, printing: false });
        }, 100);
      },
    },
    {
      label: t("common.save"),
      icon: "pi pi-check",
      className: "p-button-success",
      disabled: !isDirty || isProcessing.saving,
      action: async () => {
        const isFormValid = await trigger();
        if (isFormValid) {
          setIsProcessing({ ...isProcessing, saving: true });
          formRef.current.requestSubmit();
        }
      },
    },
  ];

  const onSubmit = (formValue) => {
    const { LicensePlate, DateOutEstimated, DateOutActual,ExpiredInDate, ...data } =
      formValue;
    data.DateOutEstimated = getDateWithFormat(DateOutEstimated);
    data.ExpiredInDate = getDateWithFormat(ExpiredInDate);
    if (DateOutActual) {
      data.DateOutActual = getDateWithFormat(DateOutActual);
    }
    if (data.OrderId) {
      updateRepairForm(data).finally(() => {
        setIsProcessing({ ...isProcessing, saving: false });
      });
      return;
    }
    createRepairForm(data)
      .then((res) => {
        if(res.data.IsSuccess){
          const orderId = res.data.Result;
          navigate(`/app/repair-detail/${orderId}`);
        }
      })
      .finally(() => {
        setIsProcessing({ ...isProcessing, saving: false });
      });
  };

  return (
    <Fragment>
      <div className="relative h-full pb-8">
        <form ref={formRef} onSubmit={handleSubmit(onSubmit)}>
          <ToggleablePanel header={t("repairForm.repairInfo")} className="pb-2" toggleable>
            <div className="formgrid grid">
              <div className="field col-12 md:col-4">
                <label htmlFor="OrderId">{t("repairForm.orderCode")}</label>
                <InputText
                  value={orderId.current}
                  disabled
                  className="w-full"
                />
              </div>
              <div className="field col-12 md:col-4">
                <label htmlFor="OrderDate">{t("repairForm.createdDate")}</label>
                <Controller
                  name="OrderDate"
                  control={control}
                  render={({ field, fieldState }) => (
                    <InputText
                      id={field.name}
                      {...field}
                      disabled
                      className={classNames("w-full", {
                        "p-invalid": fieldState.error,
                      })}
                    />
                  )}
                />
              </div>
              <div className="field col-12 md:col-4">
                <label htmlFor="StatusId">
                  {t("repairForm.status")} <b className="p-error">*</b>
                </label>
                <Controller
                  name="StatusId"
                  control={control}
                  rules={{ required: t("repairForm.statusRequired") }}
                  render={({ field, fieldState }) => (
                    <StatusDropdown field={field} fieldState={fieldState} />
                  )}
                />
                {getFormErrorMessage("StatusName")}
              </div>
              <div className="field col-12 md:col-4">
                <label htmlFor="TypeId">
                  {t("repairForm.type")} <b className="p-error">*</b>
                </label>
                <Controller
                  name="TypeId"
                  control={control}
                  rules={{ required: t("repairForm.repairTypeRequired") }}
                  render={({ field, fieldState }) => (
                    <RepairTypeDropdown field={field} fieldState={fieldState} />
                  )}
                />
                {getFormErrorMessage("TypeName")}
              </div>
              <div className="field col-12 md:col-4">
                <label htmlFor="ODOCurrent">
                  {t("repairForm.currentOdo")} <b className="p-error">*</b>
                </label>

                <Controller
                  name="ODOCurrent"
                  control={control}
                  rules={{
                    required: t("repairForm.currentOdoRequired"),
                    min: 0,
                  }}
                  render={({ field, fieldState }) => (
                    <InputNumber
                      id={field.name}
                      ref={field.ref}
                      value={field.value}
                      onBlur={field.onBlur}
                      onValueChange={(e) => {
                        field.onChange(e);
                        updateNextODO(e.value);
                      }}
                      className={classNames("w-full", {
                        "p-invalid": fieldState.error,
                      })}
                    />
                  )}
                />
                {getFormErrorMessage("ODOCurrent")}
              </div>

              <div className="field col-12 md:col-4">
                <label htmlFor="ODOUnit">
                  {t("repairForm.odoUnit")} <b className="p-error">*</b>
                </label>
                <Controller
                  name="ODOUnit"
                  control={control}
                  rules={{ required: t("repairForm.odoUnitRequired") }}
                  render={({ field, fieldState }) => (
                    <Dropdown
                      id={field.name}
                      {...field}
                      options={["Km", "Miles"]}
                      placeholder={t("repairForm.selectOdoUnit")}
                      onSelect={(e) => updateNextODO(e.value)}
                      className={classNames("w-full", {
                        "p-invalid": fieldState.error,
                      })}
                    />
                  )}
                />
                {getFormErrorMessage("ODOUnit")}
              </div>

              <div className="field col-12 md:col-4">
                <label htmlFor="DateIn">{t("repairForm.dateIn")}</label>
                <Controller
                  name="DateIn"
                  control={control}
                  render={({ field, fieldState }) => (
                    <InputText
                      id={field.name}
                      {...field}
                      disabled
                      className={classNames("w-full", {
                        "p-invalid": fieldState.error,
                      })}
                    />
                  )}
                />
              </div>

              <div className="field col-12 md:col-4">
                <label htmlFor="ODONext">{t("repairForm.odoNext")}</label>
                <Controller
                  name="ODONext"
                  control={control}
                  render={({ field, fieldState }) => (
                    <InputNumber
                      id={field.name}
                      {...field}
                      mode="decimal"
                      className={classNames("w-full", {
                        "p-invalid": fieldState.error,
                      })}
                    />
                  )}
                />
              </div>

              <div className="field col-12 md:col-4">
                <label htmlFor="DateOutEstimated">
                  {t("repairForm.expectedDeliveryDate")} <b className="p-error">*</b>
                </label>
                <Controller
                  name="DateOutEstimated"
                  control={control}
                  rules={{
                    required: t("repairForm.expectedDeliveryDateRequired"),
                  }}
                  render={({ field, fieldState }) => (
                    <Calendar
                      id={field.name}
                      {...field}
                      showIcon
                      dateFormat="dd/mm/yy"
                      className={classNames("w-full", {
                        "p-invalid": fieldState.error,
                      })}
                    />
                  )}
                />
                {getFormErrorMessage("DateOutEstimated")}
              </div>

              <div className="field col-12 md:col-4">
                <label htmlFor="ExpiredInDate">{t("repairForm.quotationExpiry")}</label>
                <Controller
                  name="ExpiredInDate"
                  rules={{
                    required: t("repairForm.quotationExpiryRequired"),
                  }}
                  control={control}
                  render={({ field, fieldState }) => (
                    <Calendar
                      id={field.name}
                      {...field}
                      showIcon
                      dateFormat="dd/mm/yy"
                      className={classNames("w-full", {
                        "p-invalid": fieldState.error,
                      })}
                    />
                  )}
                />
                {getFormErrorMessage("ExpiredInDate")}
              </div>

              <div className="field col-12 md:col-4">
                <label htmlFor="AdvancePayment">{t("repairForm.advancePayment")}</label>
                <Controller
                  name="AdvancePayment"
                  control={control}
                  render={({ field }) => (
                    <InputNumber
                      id={field.name}
                      ref={field.ref}
                      value={field.value}
                      onBlur={field.onBlur}
                      onValueChange={(e) => {
                        field.onChange(e);
                        setAdvancePayment(e.value);
                      }}
                      mode="currency"
                      currency="VND"
                      currencyDisplay="code"
                      locale="vi-VN"
                      className="w-full"
                    />
                  )}
                />
              </div>

              <div className="field col-12 md:col-4">
                <label htmlFor="Discount">{t("repairForm.discount")}</label>
                <Controller
                  name="Discount"
                  control={control}
                  render={({ field }) => (
                    <InputNumber
                      id={field.name}
                      ref={field.ref}
                      value={field.value}
                      onBlur={field.onBlur}
                      onValueChange={(e) => {
                        field.onChange(e);
                        setDiscountPercent(e.value);
                      }}
                      suffix=" %"
                      mode="decimal"
                      min={0}
                      max={100}
                      className="w-full"
                    />
                  )}
                />
              </div>

              <div className="field col-12 md:col-4">
                <label htmlFor="DateOutActual">{t("repairForm.actualDeliveryDate")}</label>
                <Controller
                  name="DateOutActual"
                  control={control}
                  render={({ field, fieldState }) => (
                    <Calendar
                      id={field.name}
                      {...field}
                      showIcon
                      dateFormat="dd/mm/yy"
                      className={classNames("w-full", {
                        "p-invalid": fieldState.error,
                      })}
                    />
                  )}
                />
              </div>

              <div className="field col-12 md:col-4">
                <label htmlFor="PaymentMethod">
                  {t("repairForm.paymentMethod")} <b className="p-error">*</b>
                </label>
                <Controller
                  name="PaymentMethod"
                  control={control}
                  rules={{ required: t("repairForm.paymentMethodRequired") }}
                  render={({ field, fieldState }) => (
                    <Dropdown
                      id={field.name}
                      {...field}
                      optionLabel="label"
                      optionValue="id"
                      options={[
                        { id: "CASH", label: t("common.paymentCash") },
                        { id: "TRANSFER", label: t("common.paymentTransfer") },
                      ]}
                      placeholder={t("common.selectPaymentMethod")}
                      onSelect={(e) => updateNextODO(e.value)}
                      className={classNames("w-full", {
                        "p-invalid": fieldState.error,
                      })}
                    />
                  )}
                />
                {getFormErrorMessage("PaymentMethod")}
              </div>

              <div className="field col-12 md:col-4">
                <label htmlFor="IsInvoice">{t("repairForm.invoice")}</label>
                <Controller
                  name="IsInvoice"
                  control={control}
                  render={({ field }) => (
                    <div className="w-full py-2">
                      <Checkbox
                        inputId={field.name}
                        onChange={(e) => {
                          field.onChange(e.checked);
                          setIsCountTax(e.checked);
                        }}
                        checked={field.value}
                      />
                    </div>
                  )}
                />
              </div>

              <div className="field col-12 md:col-4">
                <label htmlFor="Diagnosis">
                  {t("repairForm.diagnosis")} <b className="p-error">*</b>
                </label>
                <Controller
                  name="Diagnosis"
                  control={control}
                  rules={{ required: t("repairForm.diagnosisRequired") }}
                  render={({ field, fieldState }) => (
                    <InputTextarea
                      id={field.name}
                      {...field}
                      rows={5}
                      className={classNames("w-full", {
                        "p-invalid": fieldState.error,
                      })}
                    />
                  )}
                />
                {getFormErrorMessage("Diagnosis")}
              </div>

              <div className="field col-12 md:col-4">
                <label htmlFor="CustomerNote">{t("repairForm.customerNote")}</label>
                <Controller
                  name="CustomerNote"
                  control={control}
                  render={({ field }) => (
                    <InputTextarea
                      id={field.name}
                      {...field}
                      rows={5}
                      className="w-full"
                    />
                  )}
                />
              </div>

              <div className="field col-12 md:col-4">
                <label htmlFor="InternalNote">{t("repairForm.internalNote")}</label>
                <Controller
                  name="InternalNote"
                  control={control}
                  render={({ field }) => (
                    <InputTextarea
                      id={field.name}
                      rows={5}
                      {...field}
                      className="w-full"
                    />
                  )}
                />
              </div>
            </div>
          </ToggleablePanel>
          <ToggleablePanel
            header={t("repairForm.vehicleInfo")}
            className="pb-2"
            toggleable
          >
            <div className="formgrid grid">
              <div className="field col-12 md:col-4">
                <label htmlFor="LicensePlate">
                  {t("repairForm.licensePlate")} <b className="p-error">*</b>
                </label>
                <Controller
                  name="Car.LicensePlate"
                  control={control}
                  rules={{ required: t("repairForm.licensePlateRequired") }}
                  render={({ field, fieldState }) => (
                    <CarAutoComplete
                      field={field}
                      fieldState={fieldState}
                      setValue={setValue}
                      onSelectCar={setSelectedCar}
                    />
                  )}
                />
                {getFormErrorMessage("LicensePlate")}
              </div>

              <div className="field col-12 md:col-4">
                <label htmlFor="MaintainCycle">{t("repairForm.maintainanceCycle")}</label>
                <MaintainanceCycleAutoComplete
                  id="MaintainCycle"
                  onSelect={onMaintainanceCycleSelect}
                />
              </div>

              <div className="field col-12 md:col-4">
                <label htmlFor="ManufacturerName">{t("repairForm.manufacturer")}</label>
                <InputText
                  id="ManufacturerName"
                  value={selectedCar.ManufacturerName || ""}
                  disabled
                  className="w-full"
                />
              </div>
              <div className="field col-12 md:col-4">
                <label htmlFor="TypeName">{t("repairForm.carType")}</label>
                <InputText
                  id="TypeName"
                  value={selectedCar.TypeName || ""}
                  disabled
                  className="w-full"
                />
              </div>

              <div className="field col-12 md:col-4">
                <label htmlFor="YearOfManufacture">{t("repairForm.yearOfManufacture")}</label>
                <InputText
                  id="YearOfManufacture"
                  value={selectedCar.YearOfManufacture || ""}
                  disabled
                  className="w-full"
                />
              </div>

              <div className="field col-12 md:col-4">
                <label htmlFor="VIN">{t("repairForm.vin")}</label>
                <InputText
                  id="VIN"
                  value={selectedCar.VIN || ""}
                  disabled
                  className="w-full"
                />
              </div>

              <div className="field col-12 md:col-4">
                <label htmlFor="PhoneNumber">{t("customer.phone")}</label>
                <InputText
                  id="PhoneNumber"
                  value={selectedCar.Customer?.PhoneNumber || ""}
                  disabled
                  className="w-full"
                />
              </div>

              <div className="field col-12 md:col-4">
                <label htmlFor="FullName">{t("customer.fullName")}</label>
                <InputText
                  id="FullName"
                  value={selectedCar.Customer?.FullName || ""}
                  disabled
                  className="w-full"
                />
              </div>

              <div className="field col-12 md:col-4">
                <label htmlFor="Email">Email</label>
                <InputText
                  id="Email"
                  value={selectedCar.Customer?.Email || ""}
                  disabled
                  className="w-full"
                />
              </div>

              <div className="field col-12 md:col-4">
                <label htmlFor="Address">{t("customer.address")}</label>
                <InputText
                  id="Address"
                  value={selectedCar.Customer?.Address || ""}
                  disabled
                  className="w-full"
                />
              </div>

              <div className="field col-12 md:col-4">
                <label htmlFor="CustomerTypeName">{t("customer.type")}</label>
                <InputText
                  id="CustomerTypeName"
                  value={selectedCar.Customer?.TypeName || ""}
                  disabled
                  className="w-full"
                />
              </div>

              <div className="field col-12 md:col-4">
                <label htmlFor="CustomerTaxCode">{t("customer.taxCode")}</label>
                <InputText
                  id="CustomerTaxCode"
                  value={selectedCar.Customer?.TaxCode || ""}
                  disabled
                  className="w-full"
                />
              </div>
            </div>
          </ToggleablePanel>
        </form>
        <ToggleablePanel
          header={t("repairForm.repairCost")}
          className="pb-2 mb-8"
          toggleable
        >
          <SparePartTable
            existedSpareParts={sparePartFromTemplate}
            handleSparePartsChange={onHandleSparePartsChange}
            advancePayment={advancePayment}
            discountPercent={discountPercent}
            isRepairForm={true}
            isCountTax={isCountTax}
          />
        </ToggleablePanel>
        <Footer items={functionButtons} />
      </div>
      <div style={{ display: "none" }}>
        <RepairFormPdf ref={printComponentRef} data={printData} />
      </div>
    </Fragment>
  );
};

export default RepairFormDetailPage;
