import { Fragment, useState } from "react";
import { useTranslation } from "react-i18next";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";

import AppDataTable from "../../components/tables/AppDataTable";
import { useNavigate } from "react-router-dom";
import { deleteCustomer, getCustomers } from "../../services/customer-service";

const CustomersPage = () => {
  const { t } = useTranslation();
  const navigate = useNavigate();
  const [customers, setCustomers] = useState(null);
  const [paginatorOptions, setPaginatorOptions] = useState();

  const getData = (pageSize, pageIndex, keyword) => {
    getCustomers(pageSize, pageIndex, keyword).then((response) => {
      const { Data, ...paginatorOptions } = response.data.Result;
      setPaginatorOptions(paginatorOptions);
      setCustomers(Data);
    });
  };

  const columns = [
    {
      field: "FullName",
      header: t("customer.fullName"),
    },
    {
      field: "PhoneNumber",
      header: t("customer.phone"),
    },
    {
      field: "Address",
      header: t("customer.address"),
    },
    {
      field: "Email",
      header: t("customer.email"),
    },
    {
      field: "TaxCode",
      header: t("customer.taxCode"),
    },
    {
      field: "Note",
      header: t("table.note"),
    },
  ];

  const deletedSelectedCustomer = (selectedCustomer) => {
    deleteCustomer(selectedCustomer.CustomerId).then(() => {
      const updatedCustomerList = customers.filter(
        (customer) => customer.CustomerId !== selectedCustomer.CustomerId
      );
      setCustomers(updatedCustomerList);
    });
  };

  const updateCustomer = (selectedCustomer) => {
    navigate(`/app/customer-detail/${selectedCustomer.CustomerId}`);
  };

  const createNewCustomer = () => {
    navigate("/app/customer-detail/new");
  };

  const rowExpansionTemplate = (customer) => {
    return (
      <div className="orders-subtable md:ml-8">
        <DataTable
          value={customer.Cars}
          responsiveLayout="stack"
          breakpoint="960px"
        >
          <Column field="LicensePlate" header={t("table.licensePlate")}></Column>
          <Column field="TypeName" header={t("table.carType")}></Column>
          <Column field="ManufacturerName" header={t("table.manufacturer")}></Column>
          <Column field="YearOfManufacture" header={t("table.yearOfManufacture")}></Column>
          <Column field="VIN" header={t("table.vin")}></Column>
        </DataTable>
      </div>
    );
  };

  return (
    <Fragment>
      <AppDataTable
        data={customers}
        columns={columns}
        dataKey="CustomerId"
        title={t("customer.title")}
        deleteSelectedItem={deletedSelectedCustomer}
        rowExpansionTemplate={rowExpansionTemplate}
        createNewItem={createNewCustomer}
        updateItem={updateCustomer}
        paginatorOptions={paginatorOptions}
        fnGetData={getData}
      />
    </Fragment>
  );
};

export default CustomersPage;
