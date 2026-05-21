import { useSelector } from "react-redux";
import { useTranslation } from "react-i18next";
import { SlideMenu } from "primereact/slidemenu";
import {
  useNavigate,
  useLocation,
  useMatch,
  matchRoutes,
} from "react-router-dom";
const SidebarMenu = () => {
  const isShowSlidebar = useSelector((state) => state.ui.slidebarIsVisible);
  const { t } = useTranslation();
  const navigate = useNavigate();
  const location = useLocation();
  const items = [
    {
      label: t("sidebar.home"),
      icon: "pi pi-fw pi-home",
      className: useMatch("/app/home") ? "surface-hover" : "",
      command: () => {
        navigate("/app/home");
      },
    },
    {
      label: t("sidebar.customers"),
      icon: "pi pi-fw pi-user",
      className: matchRoutes(
        [
          { path: "/app/customers" },
          { path: "/app/customer-detail/new" },
          { path: "/app/customer-detail/:id" },
        ],
        location
      )
        ? "surface-hover"
        : "",
      command: () => {
        navigate("/app/customers");
      },
    },
    {
      label: t("sidebar.sparePart"),
      icon: "pi pi-fw pi-box",
      className: matchRoutes([{ path: "/app/spare-part" }], location)
        ? "surface-hover"
        : "",
      command: () => {
        navigate("/app/spare-part");
      },
    },
    {
      label: t("sidebar.maintainanceCycles"),
      icon: "pi pi-fw pi-car",
      className: matchRoutes(
        [
          { path: "/app/maintainance-cycles" },
          { path: "/app/maintainance-cycle-detail/new" },
          { path: "/app/maintainance-cycle-detail/:id" },
        ],
        location
      )
        ? "surface-hover"
        : "",
      command: () => {
        navigate("/app/maintainance-cycles");
      },
    },
    {
      label: t("sidebar.repair"),
      icon: "pi pi-fw pi-cog",
      className: matchRoutes(
        [
          { path: "/app/repair" },
          { path: "/app/repair-detail/new" },
          { path: "/app/repair-detail/:id" },
        ],
        location
      )
        ? "surface-hover"
        : "",
      command: () => {
        navigate("/app/repair");
      },
    },
    // {
    //   label: "Báo cáo",
    //   icon: "pi pi-fw pi-book",
    //   className: matchRoutes(
    //     [
    //       { path: "/app/report" },
    //     ],
    //     location
    //   )
    //     ? "surface-hover"
    //     : "",
    //   command: () => {
    //     navigate("/app/report");
    //   },
    // },
  ];

  return (
    isShowSlidebar && (
      <div className="p-3 pb-0 pr-0">
        <div className="card h-full w-20rem overflow-hidden ">
          <SlideMenu
            model={items}
            className="h-full w-full"
            menuWidth={318}
            viewportHeight={800}
          ></SlideMenu>
        </div>
      </div>
    )
  );
};

export default SidebarMenu;
