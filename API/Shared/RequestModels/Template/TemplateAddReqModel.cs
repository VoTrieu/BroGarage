using BroGarage.API.Shared.RequestModels.TemplateDetail;
using System.ComponentModel.DataAnnotations;

namespace BroGarage.API.Shared.RequestModels.Template
{
    public class TemplateAddReqModel : BaseReqModel
    {
        public int CarTypeId { get; set; }

        public int ManufaturerId { get; set; }

        [Range(0, 3_000, ErrorMessage = INVALID_VALUE)]
        public int YearOfManufactureFrom { get; set; }

        [Range(0, 3_000, ErrorMessage = INVALID_VALUE)]
        public int YearOfManufactureTo { get; set; }

        [StringLength(500, ErrorMessage = MAX_STR)]
        public string Note { get; set; } = "";

        public List<TemplateDetailAddReqModel> TemplateDetails { get; set; } = null!;
    }
}
