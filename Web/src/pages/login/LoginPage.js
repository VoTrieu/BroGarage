import { Fragment, useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import { useTranslation } from "react-i18next";
import { useForm, Controller } from "react-hook-form";
import { useNavigate } from "react-router-dom";

import { classNames } from "primereact/utils";
import { Button } from "primereact/button";
import { Password } from "primereact/password";
import { InputText } from "primereact/inputtext";
import { Message } from "primereact/message";
import { ProgressSpinner } from "primereact/progressspinner";
import { loginRequest } from "../../store/auth-actions";
import classes from "./LoginPage.module.scss";
import { Card } from "primereact/card";

const LoginPage = () => {
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const { t } = useTranslation();
  const { errorMessage, loginSuccess, isTokenValid } = useSelector(
    (state) => state.auth
  );
  const isShowSpinner = useSelector((state) => state.ui.isShowSpinner);

  const defaultValues = {
    UserName: "",
    Password: "",
  };

  //check if is new token
  useEffect(() => {
    if (isTokenValid && loginSuccess) {
      navigate("/app/home");
    }
  }, [loginSuccess, isTokenValid, navigate]);

  const {
    control,
    formState: { errors },
    handleSubmit,
  } = useForm({ defaultValues });

  const onSubmitLogin = (formValue, e) => {
    e.nativeEvent.preventDefault();
    dispatch(loginRequest(formValue));
  };

  const getFormErrorMessage = (name) => {
    return (
      errors[name] && <small className="p-error">{errors[name].message}</small>
    );
  };

  const header = (
    <div className={classNames("text-center", classes.logo_container)}>
      <img className={classes.login_logo} alt="Bro Garage" src="/images/logo.png" />
      <h1>Bro Garage</h1>
      <p>Workshop operations, customers, repairs, and parts in one place.</p>
    </div>
  );

  return (
    <Fragment>
      <div className={classes.login_page}>
        <section className={classes.login_art}>
          <div className={classes.login_art_content}>
            <span>Premium garage workspace</span>
            <h2>Run every service order with more clarity.</h2>
            <p>Manage vehicles, customers, inventory, quotes, and delivery status from a focused dashboard built for daily shop work.</p>
          </div>
        </section>

        <Card header={header} className={classes.login_container}>
          {!loginSuccess && errorMessage && (
            <Message
              className="mb-3 justify-content-start"
              severity="error"
              sticky={true}
              closable={true}
              text={errorMessage}
            />
          )}
          <form onSubmit={handleSubmit(onSubmitLogin)} className="formgrid grid">
            <div className="field col-12 p-fluid">
              <label htmlFor="UserName">{t("login.username")}</label>
              <span className="p-input-icon-left w-full">
                <i className="pi pi-user" />
                <Controller
                  name="UserName"
                  control={control}
                  rules={{ required: t("login.usernameRequired") }}
                  render={({ field, fieldState }) => (
                    <InputText
                      id={field.name}
                      {...field}
                      autoFocus
                      className={classNames("block w-full", {
                        "p-invalid": fieldState.error,
                      })}
                    />
                  )}
                />
              </span>
              {getFormErrorMessage("UserName")}
            </div>

            <div className="field col-12 p-fluid">
              <label htmlFor="Password">{t("login.password")}</label>
              <Controller
                name="Password"
                control={control}
                rules={{ required: t("login.passwordRequired") }}
                render={({ field, fieldState }) => (
                  <Password
                    id={field.name}
                    {...field}
                    toggleMask
                    className={classNames("block w-full", {
                      "p-invalid": fieldState.error,
                    })}
                    feedback={false}
                  />
                )}
              />
              {getFormErrorMessage("Password")}
            </div>
            <div className="flex w-full">
              <Button
                disabled={isShowSpinner}
                label={t("login.button")}
                icon="pi pi-check"
                className="p-button-success w-full"
              />
            </div>
          </form>
        </Card>
        <ProgressSpinner
          style={{ width: "40px", height: "40px" }}
          strokeWidth="4"
          fill="var(--surface-ground)"
          animationDuration=".5s"
          className={[
            classes.login_spinner,
            classNames("absolute", {
              hidden: !isShowSpinner,
            }),
          ]}
        />
      </div>
    </Fragment>
  );
};

export default LoginPage;
