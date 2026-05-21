using System.ComponentModel.DataAnnotations;

namespace BroGarage.API.Shared.RequestModels.OrderDetail
{
    public class OrderDetailAddReqModel : BaseReqModel
    {
        public int ProductId { get; set; }

        [Range(0, 1_000, ErrorMessage = INVALID_VALUE)]
        public int Quantity { get; set; }

        [MaxLength(500, ErrorMessage = MAX_STR)]
        public string Comment { get; set; } = "";

        public bool IsHideProduct { get; set; }
    }
}
