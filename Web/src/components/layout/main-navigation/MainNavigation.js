import { useRef } from 'react';
import { useDispatch, useSelector } from "react-redux";
import { useTranslation } from "react-i18next";
import { Menubar } from "primereact/menubar";
import { Menu } from 'primereact/menu';
import { uiActions } from "../../../store/ui-slice";
import ChangePassword from "../../account/ChangePassword";
import { Button } from "primereact/button";

// import classes from "./MainNavigation.module.css";
import { Fragment } from "react";

function MainNavigation() {
  const menu = useRef(null);
  const dispatch = useDispatch();
  const fullName = useSelector((state) => state.auth.fullName);

  const { t, i18n } = useTranslation();

  const toggleSlidebarMenu = () => {
    dispatch(uiActions.toggleSlidebar());
  };

  const changeLanguage = (lang) => {
    i18n.changeLanguage(lang);
    localStorage.setItem("lang", lang);
  };

  const menuItems = [
    {
      label: t("menu.account"),
      items: [
        {
          label: t("menu.changePassword"),
          icon: "pi pi-user-edit",
          command: () => {
            dispatch(uiActions.showChangePasswordDialog(true));
          },
        },
      ],
    },
    {
      label: t("menu.language"),
      items: [
        {
          label: t("language.en"),
          command: () => changeLanguage("en"),
        },
        {
          label: t("language.vi"),
          command: () => changeLanguage("vi"),
        },
      ],
    },
  ];

  const start = (
    <Fragment>
      <div className="app-brand">
        <img alt="Bro Garage" src="/images/logo.png" />
        <div>
          <strong>Bro Garage</strong>
          <span>Service desk</span>
        </div>
      </div>
      <Button className="nav-icon-button" icon="pi pi-bars" onClick={toggleSlidebarMenu} />
    </Fragment>
  );
  const end = (
    <div className="app-user-menu">
      <span>{fullName}</span>
      <Button className="nav-icon-button" icon="pi pi-cog" onClick={(event) => menu.current.toggle(event)} aria-controls="popup_menu" aria-haspopup />
      <Menu model={menuItems} popup ref={menu} id="popup_menu" />
    </div>
  );

  return (
    <header>
      <Menubar model={[]} start={start} end={end} />
      <ChangePassword />
    </header>
  );
}

export default MainNavigation;
