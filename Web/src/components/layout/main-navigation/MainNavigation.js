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
      <img
        alt="logo"
        src='/images/logo.png'
        height="40"
        className="mr-2 md:w-13rem md:mr-8 hidden md:inline"
      ></img>
      <Button icon="pi pi-bars" onClick={toggleSlidebarMenu} />
    </Fragment>
  );
  const end = (
    <div className="flex align-items-center">
      <span className="mr-2">{fullName}</span>
      <Button icon="pi pi-cog" onClick={(event) => menu.current.toggle(event)} aria-controls="popup_menu" aria-haspopup />
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
