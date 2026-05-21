import { Fragment, useState } from "react";
import { useTranslation } from "react-i18next";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";

import AppDataTable from "../../components/tables/AppDataTable";
import { useNavigate } from "react-router-dom";
import {
  deleteMaintainanceCycle,
  getMaintainanceCycle,
} from "../../services/maintainance-cycle-service";

const MaintainanceCyclePage = () => {
  const { t } = useTranslation();
  const navigate = useNavigate();
  const [maintainanceCycles, setMaintainanceCycles] = useState(null);
  const [paginatorOptions, setPaginatorOptions] = useState();

  const getData = (pageSize, pageIndex, keyword) => {
    getMaintainanceCycle(pageSize, pageIndex, keyword).then((response) => {
      const { Data, ...paginatorOptions } = response.data.Result;
      setPaginatorOptions(paginatorOptions);
      setMaintainanceCycles(Data);
    });
  };

  const columns = [
    {
      field: "ManufacturerName",
      header: t("maintainanceCycle.manufacturer"),
    },
    {
      field: "CarTypeName",
      header: t("maintainanceCycle.carType"),
    },
    {
      field: "YearOfManufactureFrom",
      header: t("maintainanceCycle.yearFrom"),
    },
    {
      field: "YearOfManufactureTo",
      header: t("maintainanceCycle.yearTo"),
    },
    {
      field: "Note",
      header: t("maintainanceCycle.note"),
    },
  ];

  const deletedSelectedMaintainanceCycle = (maintainanceCycle) => {
    deleteMaintainanceCycle(maintainanceCycle.TemplateId).then(() => {
      const updatedList = maintainanceCycles.filter(
        (item) => item.TemplateId !== maintainanceCycle.TemplateId
      );
      setMaintainanceCycles(updatedList);
    });
  };

  const updateMaintainanceCycle = (maintainanceCycle) => {
    navigate(`/app/maintainance-cycle-detail/${maintainanceCycle.TemplateId}`);
  };

  const createMaintainanceCycle = () => {
    navigate("/app/maintainance-cycle-detail/new");
  };

  const rowExpansionTemplate = (maintainanceCycle) => {
    return (
      <div className="orders-subtable md:ml-8">
        <DataTable
          value={maintainanceCycle.TemplateDetails}
          responsiveLayout="stack"
          breakpoint="960px"
        >
          <Column field="ProductCode" header={t("sparePart.productCode")}></Column>
          <Column field="ProductName" header={t("sparePart.description")}></Column>
          <Column field="Quantity" header={t("table.quantity")}></Column>
          <Column field="UnitName" header={t("table.unit")}></Column>
          <Column field="UnitPrice" header={t("table.unitPrice")}></Column>
        </DataTable>
      </div>
    );
  };

  return (
    <Fragment>
      <AppDataTable
        data={maintainanceCycles}
        columns={columns}
        dataKey="TemplateId"
        title={t("maintainanceCycle.title")}
        deleteSelectedItem={deletedSelectedMaintainanceCycle}
        rowExpansionTemplate={rowExpansionTemplate}
        createNewItem={createMaintainanceCycle}
        updateItem={updateMaintainanceCycle}
        paginatorOptions={paginatorOptions}
        fnGetData={getData}
      />
    </Fragment>
  );
};

export default MaintainanceCyclePage;
