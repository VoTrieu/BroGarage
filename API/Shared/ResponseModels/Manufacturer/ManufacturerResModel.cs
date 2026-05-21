using BroGarage.API.Shared.ResponseModels.CarType;

namespace BroGarage.API.Shared.ResponseModels.Manufacturer
{
    public class ManufacturerResModel
    {
        public int ManufacturerId { get; set; }

        public string ManufacturerName { get; set; } = "";

        public string CreatedDate { get; set; } = "";

        public string CreatedTime { get; set; } = "";

        public string CreatedDateTime => $"{CreatedDate} {CreatedTime}";
    }
}
