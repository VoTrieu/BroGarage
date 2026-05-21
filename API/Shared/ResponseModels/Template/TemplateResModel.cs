using BroGarage.API.Shared.ResponseModels.TemplateDetail;

namespace BroGarage.API.Shared.ResponseModels.Template
{
    public class TemplateResModel
    {
        public int TemplateId { get; set; }

        public int CarTypeId { get; set; }

        public string CarTypeName { get; set; } = "";

        public int ManufaturerId { get; set; }

        public string ManufacturerName { get; set; } = "";

        public int YearOfManufactureFrom { get; set; }

        public int YearOfManufactureTo { get; set; }

        public string Note { get; set; } = "";

        public string CreatedDate { get; set; } = "";

        public string CreatedTime { get; set; } = "";

        public string CreatedDateTime => $"{CreatedDate} {CreatedTime}";

        public IEnumerable<TemplateDetailResModel> TemplateDetails { get; set; } = null!;
    }
}
