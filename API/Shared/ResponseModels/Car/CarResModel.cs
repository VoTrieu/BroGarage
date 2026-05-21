namespace BroGarage.API.Shared.ResponseModels.Car
{
    public class CarResModel
    {
        public int CarId { get; set; }

        public int TypeId { get; set; }

        public string TypeName { get; set; } = "";

        public int ManufacturerId { get; set; }

        public string ManufacturerName { get; set; } = "";

        public CarCustomerResModel Customer { get; set; } = null!;

        public string Representative { get; set; } = "";

        public string LicensePlate { get; set; } = "";

        public int YearOfManufacture { get; set; }

        public string VIN { get; set; } = "";

        public string AvatarUrl { get; set; } = "";

        public string CreatedDate { get; set; } = "";

        public string CreatedTime { get; set; } = "";

        public string CreatedDateTime => $"{CreatedDate} {CreatedTime}";
    }

    public class CarCustomerResModel
    {
        public int CustomerId { get; set; }

        public int TypeId { get; set; }

        public string TypeName { get; set; } = string.Empty;

        public string FullName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;

        public string TaxCode { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string Note { get; set; } = string.Empty;

        public string CreatedDate { get; set; } = string.Empty;

        public string CreatedTime { get; set; } = string.Empty;
    }
}
