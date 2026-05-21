using BroGarage.API.Shared.ResponseModels.Car;

namespace BroGarage.API.Shared.ResponseModels.Customer
{
    public class CustomerResModel
    {
        public int CustomerId { get; set; }

        public int TypeId { get; set; }

        public string TypeName { get; set; } = "";

        public string FullName { get; set; } = "";

        public string PhoneNumber { get; set; } = "";

        public string Representative { get; set; } = "";

        public string TaxCode { get; set; } = "";

        public string Address { get; set; } = "";

        public string Email { get; set; } = "";

        public string Note { get; set; } = "";

        public string AvatarUrl { get; set; } = "";

        public IEnumerable<CustomerCarResModel> Cars { get; set; } = null!;

        public string CreatedDate { get; set; } = "";

        public string CreatedTime { get; set; } = "";

        public string CreatedDateTime => $"{CreatedDate} {CreatedTime}";
    }

    public class CustomerCarResModel
    {
        public int CarId { get; set; }

        public int CarTypeId { get; set; }

        public string TypeName { get; set; } = "";

        public int ManufacturerId { get; set; }

        public string ManufacturerName { get; set; } = "";

        public string Representative { get; set; } = "";

        public string LicensePlate { get; set; } = "";

        public int YearOfManufacture { get; set; }

        public string VIN { get; set; } = "";

        public string AvatarUrl { get; set; } = "";

        public string CreatedDate { get; set; } = "";

        public string CreatedTime { get; set; } = "";

        public string CreatedDateTime => $"{CreatedDate} {CreatedTime}";
    }
}
