using BroGarage.API.Shared.RequestModels.TemplateDetail;

namespace BroGarage.API.Shared.RequestModels.Template
{
    public class TemplateEditReqModel : TemplateAddReqModel
    {
        public int TemplateId { get; set; }

        public new List<TemplateDetailEditReqModel> TemplateDetails { get; set; } = null!;
    }
}
