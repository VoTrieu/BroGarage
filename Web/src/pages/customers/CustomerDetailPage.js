import { useState, useRef, useEffect } from "react";
import { useTranslation } from "react-i18next";
import { useForm, Controller } from "react-hook-form";
import { useParams, useNavigate } from "react-router-dom";
import ToggleablePanel from "../../components/panels/ToogleablePanel";
import CarTable from "../../components/tables/CarTable";
import { InputText } from "primereact/inputtext";
import { InputTextarea } from "primereact/inputtextarea";
import { Dropdown } from "primereact/dropdown";
import { classNames } from "primereact/utils";
import Footer from "../../components/layout/footer/Footer";
import {
  getCustomerDetail,
  createNewCustomer,
  updateCustomer,
} from "../../services/customer-service";


const defaultValues = {
  CustomerId: "",
  Email: "",
  FullName: "",
  PhoneNumber: "",
  Remark: "",
  Representative: "",
  TaxCode: "",
  TypeId: 1,
};

const CustomerDetailPage = () => {
  const { t } = useTranslation();
  const customerTypes = [
    { label: t("customer.personal"), value: 1 },
    { label: t("customer.enterprise"), value: 2 },
  ];
  const [existedCars, setExistedCars] = useState([]);
  const formRef = useRef();
  const params = useParams();
  const navigate = useNavigate();
  const [isSubmitting, setIsSubmitting] = useState(false);
  const selectedCustomerId = params?.id;

  const {
    control,
    formState: { errors, isDirty },
    handleSubmit,
    setValue,
    reset,
  } = useForm({ defaultValues });

  useEffect(() => {
    if (selectedCustomerId) {
      getCustomerDetail(selectedCustomerId).then((response) => {
        const customer = response.data.Result;
        setExistedCars(customer.Cars);
        reset(customer);
      });
    }
  }, [reset, selectedCustomerId]);

  const functionButtons = [
    {
      label: t("common.save"),
      icon: "pi pi-check",
      disabled: !isDirty || isSubmitting,
      className: "p-button-success",
      action: () => {
        formRef.current.requestSubmit();
      },
    },
  ];

  const handleCarsChange = (cars) => {
    setValue("Cars", cars, { shouldDirty: true });
  };

  const onSubmit = (formData, e) => {
    e.nativeEvent.preventDefault();
    if (formData.CustomerId) {
      updateCustomer(formData);
      return;
    }

    createNewCustomer(formData)
      .then((res) => {
        if (res.data.IsSuccess) {
          const id = res.data.Result;
          navigate(`/app/customer-detail/${id}`);
        }
      })
      .finally(() => setIsSubmitting(false));
  };

  const getFormErrorMessage = (name) => {
    return (
      errors[name] && <small className="p-error">{errors[name].message}</small>
    );
  };

  return (
    <div className="relative h-full pb-8">
      <ToggleablePanel header={t("customer.customer")} className="pb-2" toggleable>
        <form
          ref={formRef}
          onSubmit={handleSubmit(onSubmit)}
          className="formgrid grid"
        >
          <div className="field col-12 md:col-4">
            <label htmlFor="FullName">
              {t("customer.fullName")} <b className="p-error">*</b>
            </label>
            <Controller
              name="FullName"
              control={control}
              rules={{ required: t("customer.customerRequired") }}
              render={({ field, fieldState }) => (
                <InputText
                  id={field.name}
                  {...field}
                  className={classNames("block w-full", {
                    "p-invalid": fieldState.error,
                  })}
                />
              )}
            />
            {getFormErrorMessage("FullName")}
          </div>
          <div className="field col-12 md:col-4">
            <label htmlFor="TypeId">
              {t("customer.type")} <b className="p-error">*</b>
            </label>
            <Controller
              name="TypeId"
              control={control}
              rules={{ required: t("customer.typeRequired") }}
              render={({ field }) => (
                <Dropdown
                  id={field.name}
                  value={field.value}
                  onChange={(e) => field.onChange(e.value)}
                  optionLabel="label"
                  options={customerTypes}
                  className="w-full"
                  placeholder={t("customer.selectCustomerType")}
                />
              )}
            />
            {getFormErrorMessage("TypeId")}
          </div>
          <div className="field col-12 md:col-4">
            <label htmlFor="PhoneNumber">
              {t("customer.phone")} <b className="p-error">*</b>
            </label>
            <Controller
              name="PhoneNumber"
              control={control}
              rules={{
                required: t("customer.phoneRequired"),
                pattern: {
                  value: /(03|05|07|08|09|01[2|6|8|9])+([0-9]{8})\b/,
                  message: t("customer.phoneInvalid"),
                },
              }}
              render={({ field, fieldState }) => (
                <InputText
                  id={field.name}
                  {...field}
                  className={classNames("block w-full", {
                    "p-invalid": fieldState.error,
                  })}
                />
              )}
            />
            {getFormErrorMessage("PhoneNumber")}
          </div>
          <div className="field col-12 md:col-4">
            <label htmlFor="Representative">{t("customer.representative")}</label>
            <Controller
              name="Representative"
              control={control}
              render={({ field, fieldState }) => (
                <InputText
                  id={field.name}
                  {...field}
                  className="block w-full"
                />
              )}
            />
          </div>
          <div className="field col-12 md:col-4">
            <label htmlFor="TaxCode">{t("customer.taxCode")}</label>
            <Controller
              name="TaxCode"
              control={control}
              render={({ field, fieldState }) => (
                <InputText
                  id={field.name}
                  {...field}
                  className="block w-full"
                />
              )}
            />
          </div>
          <div className="field col-12 md:col-4">
            <label htmlFor="Email">{t("customer.email")}</label>
            <Controller
              name="Email"
              control={control}
              rules={{
                pattern: {
                  value: /^[A-Z0-9._%+-]+@[A-Z0-9.-]+\.[A-Z]{2,4}$/i,
                  message: t("customer.emailInvalid"),
                },
              }}
              render={({ field, fieldState }) => (
                <InputText
                  id={field.name}
                  {...field}
                  className={classNames("block w-full", {
                    "p-invalid": fieldState.error,
                  })}
                />
              )}
            />
            {getFormErrorMessage("Email")}
          </div>
          <div className="field col-12 md:col-4">
            <label htmlFor="Address">{t("customer.address")}</label>
            <Controller
              name="Address"
              control={control}
              render={({ field, fieldState }) => (
                <InputTextarea
                  rows={5}
                  cols={30}
                  id={field.name}
                  {...field}
                  className="block w-full"
                />
              )}
            />
          </div>
          <div className="field col-12 md:col-4">
            <label htmlFor="Note">{t("table.note")}</label>
            <Controller
              name="Note"
              control={control}
              render={({ field }) => (
                <InputTextarea
                  rows={5}
                  cols={30}
                  id={field.name}
                  {...field}
                  className="block w-full"
                />
              )}
            />
          </div>
        </form>
      </ToggleablePanel>
      <ToggleablePanel header="Xe" className="pb-2" toggleable>
        <CarTable
          existedCars={existedCars}
          handleCarsChange={handleCarsChange}
        />
      </ToggleablePanel>
      <Footer items={functionButtons} />
    </div>
  );
};

export default CustomerDetailPage;
