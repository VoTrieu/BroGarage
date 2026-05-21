import { forwardRef, Fragment } from "react";
import { useTranslation } from "react-i18next";
import { DataTable } from "primereact/datatable";
import { Column } from "primereact/column";
import { ColumnGroup } from "primereact/columngroup";
// import { InputTextarea } from 'primereact/inputtextarea';
import { classNames } from "primereact/utils";
import { Row } from "primereact/row";
import { sumBy } from "lodash";
import classes from "./RepairFormPdf.module.scss";

const RepairFormPdf = forwardRef((props, ref) => {
  const { t } = useTranslation();
  const { data } = props;
  const orderDetails = data?.OrderDetails.filter(
    (item) => item.IsHideProduct === false
  );

  const priceBodyTemplate = (rowData, field) => {
    return formatAmount(rowData[field]);
  };

  const titleFooterTemplate = () => {
    return (
      <Fragment>
        <div className="py-1">{t("pdf.subtotalA")}</div>
        <div className="py-1">
          {t("pdf.discountB", { discount: data.Discount || 0 })}
        </div>
        <div className="py-1">{t("pdf.taxC")}</div>
        <div className="py-1">{t("pdf.advancePaymentD")}</div>
        <div className="py-1">{t("pdf.grandTotal")}</div>
      </Fragment>
    );
  };

  const formatAmount = (number) => {
    return new Intl.NumberFormat("vi-VN", {
      style: "decimal",
      maximumFractionDigits: 0,
    }).format(number);
  };

  const totalFooterTemplate = () => {
    const total = sumBy(orderDetails, (item) => item.Quantity * item.UnitPrice);
    const discount = total * (data.Discount / 100);
    const tax = data.IsInvoice ? (total - discount) * 0.1 : 0;
    const finalAmount = total + tax - data.AdvancePayment;
    const totalElement = formatAmount(total);
    const discountElement = formatAmount(discount);
    const totalIncludedTaxElement = formatAmount(tax);
    const advancePaymentElement = formatAmount(data.AdvancePayment);
    const finalAmountElement = formatAmount(finalAmount);
    return (
      <Fragment>
        <div className="py-1">{totalElement}</div>
        <div className="py-1">{discountElement}</div>
        <div className="py-1">{totalIncludedTaxElement}</div>
        <div className="py-1">{advancePaymentElement}</div>
        <div className="py-1">{finalAmountElement}</div>
      </Fragment>
    );
  };

  let footerGroup = (
    <ColumnGroup>
      <Row>
        <Column
          footer={titleFooterTemplate}
          colSpan={4}
          className={classNames(classes.pdf_font_size, 'text-right')}
        />
        <Column
          footer={totalFooterTemplate}
          colSpan={2}
          className={classNames(classes.pdf_font_size, 'text-right')}
        />
      </Row>
    </ColumnGroup>
  );

  return (
    data?.OrderId && (
      <div
        className={classNames(classes.pdf_container, classes.pdf_font_size)}
        ref={ref}
      >
        <div className="header-info mb-5">
          <h3 className="mb-1">{t("pdf.companyName")}</h3>
          <div className="flex my-1 mb-1">
            <b className="w-2">{t("pdf.address")}: </b>
            <span>{t("pdf.companyAddress")}</span>
          </div>
          <div className="flex my-1 mb-1">
            <b className="w-2">{t("pdf.hotline")}: </b>
            <span className="w-3">0937640052</span>
            <b className="w-3">{t("pdf.taxNumber")}: </b>
            <span className="w-3">0315337688</span>
          </div>
          <div className="flex my-1 mb-1">
            <b className="w-2">{t("pdf.bankAccount")}: </b>
            <span className="w-3">0171003472793</span>
            <b className="w-3">{t("pdf.bankBranch")}: </b>
            <span className="w-3">Vietcombank</span>
          </div>
        </div>
        <div className="text-center">
          <h3 className="mb-2">
            {data.StatusId === 1 ? t("pdf.quotation") : t("pdf.invoice")}
          </h3>
          <span>{t("pdf.date", { date: data.CreatedDate })}</span>
        </div>
        <div className="text-right mt-2 p-1 flex justify-content-end">
          <span>{t("pdf.orderNumber")}</span>
          <div className="w-2">{data.OrderId}</div>
        </div>
        <section>
          <div className="surface-400 mt-0 p-1">
            <h4 className="my-0">{t("pdf.customerInfo")}</h4>
          </div>
          <div className="px-3 grid">
            <div className="col-6">
              <div className="flex my-1">
                <span className="w-3">{t("pdf.customerName")}</span>
                <span className="mx-2">:</span>
                <span>{data.Car.Customer.FullName}</span>
              </div>
              <div className="flex my-1">
                <span className="w-3">{t("pdf.representative")}</span>
                <span className="mx-2">:</span>
                <span>{data.Car.Customer.Representative}</span>
              </div>
              <div className="flex my-1">
                <span className="w-3">{t("pdf.taxCode")}</span>
                <span className="mx-2">:</span>
                <span>{data.Car.Customer.TaxCode}</span>
              </div>
            </div>
            <div className="col-6">
              <div className="flex my-1">
                <span className="w-4">{t("pdf.address")}</span>
                <span className="mx-2">:</span>
                <span>{data.Car.Customer.Address}</span>
              </div>
              <div className="flex my-1">
                <span className="w-4">{t("pdf.phoneNumber")}</span>
                <span className="mx-2">:</span>
                <span>{data.Car.Customer.PhoneNumber}</span>
              </div>
              <div className="flex my-1">
                <span className="w-4">{t("pdf.email")}</span>
                <span className="mx-2">:</span>
                <span>{data.Car.Customer.Email}</span>
              </div>
            </div>
          </div>
        </section>
        <section className="my-1">
          <div className="surface-400 mt-0 p-1">
            <h4 className="my-0">{t("pdf.vehicleInfo")}</h4>
          </div>
          <div className="grid px-3">
            <div className="col-6">
              <div className="flex my-1">
                <span className="w-3">{t("repairForm.licensePlate")}</span>
                <span className="mx-2">:</span>
                <span>{data.Car.LicensePlate}</span>
              </div>
              <div className="flex my-1">
                <span className="w-3">{t("table.vin")}</span>
                <span className="mx-2">:</span>
                <span>{data.Car.VIN}</span>
              </div>
              <div className="flex my-1">
                <span className="w-3">{t("repairForm.currentOdo")}</span>
                <span className="mx-2">:</span>
                <span>{data.ODOCurrent}</span>
              </div>
              <div className="flex my-1">
                <span className="w-3">{t("report.dateIn")}</span>
                <span className="mx-2">:</span>
                <span>{data.DateIn}</span>
              </div>
            </div>
            <div className="col-6">
              <div className="flex my-1">
                <span className="w-4">{t("repairForm.carType")}</span>
                <span className="mx-2">:</span>
                <span>{data.Car.TypeName}</span>
              </div>
              <div className="flex my-1">
                <span className="w-4">{t("repairForm.odoNext")}</span>
                <span className="mx-2">:</span>
                <span>{data.ODONext}</span>
              </div>
              <div className="flex my-1">
                <span className="w-4">{t("repairForm.expectedDeliveryDate")}</span>
                <span className="mx-2">:</span>
                <span>{data.DateOutEstimated}</span>
              </div>
            </div>
          </div>
        </section>
        <section className="my-1">
          <div className="surface-400 mt-0 p-1">
            <h4 className="my-0">{t("repairForm.customerNote")}</h4>
          </div>
          <p className="border w-full px-3">{data.CustomerNote}</p>
          {/* <InputTextarea value={data.CustomerNote} className="border w-full px-3" rows={3} cols={30}/> */}
        </section>

        <section className="my-1">
          <DataTable
            headerclassname={classes.spare_part_pdf_table}
            value={orderDetails}
            footerColumnGroup={footerGroup}
            size="small"
          >
            <Column
              field="ProductName"
              header={t("pdf.workContent")}
              className={classes.pdf_font_size}
            ></Column>
            <Column
              field="UnitName"
              header={t("pdf.unit")}
              className={classes.pdf_font_size}
            ></Column>
            <Column
              field="Quantity"
              header={t("pdf.quantity")}
              className={classNames(classes.pdf_font_size, "text-right")}
            ></Column>
            <Column
              field="UnitPrice"
              className={classNames(classes.pdf_font_size, "text-right")}
              body={(rowData) => priceBodyTemplate(rowData, "UnitPrice")}
              header={t("pdf.unitPrice")}
            ></Column>
            <Column
              field="Total"
              className={classNames(classes.pdf_font_size, "text-right")}
              body={(rowData) => priceBodyTemplate(rowData, "Total")}
              header={t("pdf.amount")}
            ></Column>
          </DataTable>
        </section>

        {/* signature */}
        <section className="my-8">
          <div className="grid my-8">
            <div className="col-6 text-center">
              <p>
                <b>{t("pdf.garageConfirmation")}</b>
              </p>
                <span>{t("pdf.signature")}</span>
            </div>

            <div className="col-6 text-center">
              <p>
                <b>{t("pdf.customerConfirmation")}</b>
              </p>
                <span>{t("pdf.signature")}</span>
            </div>
          </div>
        </section>
        <section className="pt-4">
          <p>{t("pdf.warrantyNote")}</p>
          {data.StatusId === 1 && (
            <p>{t("pdf.quotationExpiryNote")}</p>
          )}
        </section>
      </div>
    )
  );
});

export default RepairFormPdf;
