namespace BroGarage.API.Shared.ResponseModels.CarType
{
    public class CarTypeResModel
    {
        public int TypeId { get; set; }

        public int ManufacturerId { get; set; }

        public string TypeName { get; set; } = "";

        public string CreatedDate { get; set; } = "";

        public string CreatedTime { get; set; } = "";

        public string CreatedDateTime => $"{CreatedDate} {CreatedTime}";
    }
}
