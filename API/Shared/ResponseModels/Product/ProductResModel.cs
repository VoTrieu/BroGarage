namespace BroGarage.API.Shared.ResponseModels.Product
{
    public class ProductResModel
    {
        public int ProductId { get; set; }

        public string ProductCode { get; set; } = "";

        public string ProductName { get; set; } = "";

        public string Remark { get; set; } = "";

        public string UnitName { get; set; } = "";

        public long UnitPrice { get; set; }

        public int Quantity { get; set; }

        public string AvatarUrl { get; set; } = "";

        public string CreatedDate { get; set; } = "";

        public string CreatedTime { get; set; } = "";

        public string CreatedDateTime => $"{CreatedDate} {CreatedTime}";
    }
}
