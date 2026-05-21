using BroGarage.API.Shared.RequestModels.OrderDetail;

namespace BroGarage.API.Shared.RequestModels.Order
{
    public class OrderEditResModel : OrderAddReqModel
    {
        public int OrderId { get; set; }

        public new List<OrderDetailEditReqModel> OrderDetails { get; set; } = null!;
    }
}
