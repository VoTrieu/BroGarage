using System.ComponentModel.DataAnnotations;

namespace BroGarage.API.Shared.RequestModels.TemplateDetail
{
    public class TemplateDetailAddReqModel : BaseReqModel
    {
        public int ProductId { get; set; }

        [Range(0, 1_000, ErrorMessage = INVALID_VALUE)]
        public int Quantity { get; set; }
    }
}
