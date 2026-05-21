namespace BroGarage.API.Shared.ResponseModels.Order
{
    public class OrderResModel
    {
        public int OrderId { get; set; }

        public int CarId { get; set; }

        public OrderCarResModel Car { get; set; } = null!;

        public int StatusId { get; set; }

        public string StatusName { get; set; } = "";

        public int TypeId { get; set; }

        public string TypeName { get; set; } = "";

        public int? TemplateId { get; set; } = null;

        public string OrderCode { get; set; } = "";

        public string OrderDate { get; set; } = "";

        public string DateIn { get; set; } = "";

        public string DateOutEstimated { get; set; } = "";

        public string DateOutActual { get; set; } = "";

        public decimal ODOCurrent { get; set; }

        public decimal ODONext { get; set; }

        public string ODOUnit { get; set; } = "";

        public string ExpiredInDate { get; set; } = "";

        public bool IsInvoice { get; set; }

        public long AdvancePayment { get; set; }

        public string PaymentMethod { get; set; } = "";

        public string Diagnosis { get; set; } = "";

        public string CustomerNote { get; set; } = string.Empty;

        public string InternalNote { get; set; } = string.Empty;

        public decimal VAT { get; set; }

        public long Discount { get; set; }

        public IEnumerable<OrderDetailResModel> OrderDetails { get; set; } = null!;

        public long TotalBeforeVAT => OrderDetails.Sum(n => n.Total) - Discount;

        public decimal TotalAfterVAT
        {
            get
            {
                if (IsInvoice)
                {
                    return TotalBeforeVAT + TotalBeforeVAT * VAT;
                }
                else
                {
                    return TotalBeforeVAT;
                }
            }
        }

        public string CreatedDate { get; set; } = string.Empty;

        public string CreatedTime { get; set; } = string.Empty;
    }

    public class OrderCarResModel
    {
        public int CarId { get; set; }

        public int CarTypeId { get; set; }

        public string TypeName { get; set; } = string.Empty;

        public int ManufaturerId { get; set; }

        public string ManufacturerName { get; set; } = string.Empty;

        public int CustomerId { get; set; }

        public string LicensePlate { get; set; } = string.Empty;

        public int YearOfManufacture { get; set; }

        public string VIN { get; set; } = string.Empty;

        public OrderCarCustomerResModel Customer { get; set; } = null!;

        public string CreatedDate { get; set; } = string.Empty;

        public string CreatedTime { get; set; } = string.Empty;
    }

    public class OrderCarCustomerResModel
    {
        public int CustomerId { get; set; }

        public int TypeId { get; set; }

        public string TypeName { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string Representative { get; set; } = "";

        public string TaxCode { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Note { get; set; } = string.Empty;

        public string CreatedDate { get; set; } = string.Empty;

        public string CreatedTime { get; set; } = string.Empty;
    }

    public record OrderDetailResModel
    {
        public int OrderDetailId { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public string ProductCode { get; set; } = "";

        public string ProductName { get; set; } = "";

        public string UnitName { get; set; } = "";

        public long UnitPrice { get; set; }

        public int Quantity { get; set; }

        public long Total => UnitPrice * Quantity;

        public string Comment { get; set; } = "";

        public bool IsHideProduct { get; set; }

        public string CreatedDate { get; set; } = "";

        public string CreatedTime { get; set; } = "";
    }
}
