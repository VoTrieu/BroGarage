namespace BroGarage.API.Shared.RequestModels.Order
{
    public class OrderEditStatusReqModel : BaseReqModel
    {
        public int OrderId { get; set; }

        public int StatusId { get; set; }
    }
}
