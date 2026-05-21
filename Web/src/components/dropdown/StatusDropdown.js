import { useState, useEffect } from "react";
import { useTranslation } from "react-i18next";
import { Dropdown } from "primereact/dropdown";
import { classNames } from "primereact/utils";
import { getRepairStatus } from "../../services/repair-service";

const StatusDropdown = (props) => {
  const { t } = useTranslation();
  const [repairStatus, setRepairStatus] = useState(null);

  useEffect(() => {
    getRepairStatus().then((res) => {
      const status = res.data.Result;
      setRepairStatus(status);
    });
  }, []);

  return (
    <Dropdown
      id={props.field?.name}
      value={props.field.value}
      onChange={(date) => props.field.onChange(date)}
      options={repairStatus}
      optionLabel="StatusName"
      optionValue="StatusId"
      placeholder={t("dropdown.selectStatus")}
      className={classNames("w-full", {
        "p-invalid": props.fieldState?.error,
      })}
    />
  );
};

export default StatusDropdown;
