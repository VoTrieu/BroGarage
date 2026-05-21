namespace BroGarage.API.Shared.ResponseModels.TemplateDetail
{
    public class TemplateDetailResModel
    {
        public int TemplateDetailId { get; set; }

        public int TemplateId { get; set; }

        public int ProductId { get; set; }

        public string ProductCode { get; set; } = "";

        public string ProductName { get; set; } = "";

        public string UnitName { get; set; } = "";

        public long UnitPrice { get; set; }

        public int Quantity { get; set; }

        public string AvatarUrl { get; set; } = "";
    }
}
