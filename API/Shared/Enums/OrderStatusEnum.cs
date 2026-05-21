namespace BroGarage.API.Shared.Enums
{
    public class OrderStatusEnum
    {
        /// <summary>
        /// Báo giá
        /// </summary>
        public const int PRICE_QUOTATION_1 = 1;

        /// <summary>
        /// Đang sửa chữa
        /// </summary>
        public const int REPAIRING_2 = 2;

        /// <summary>
        /// Chờ giao xe
        /// </summary>
        public const int WAITING_FOR_DELEVERY_3 = 3;

        /// <summary>
        /// Hoàn thành
        /// </summary>
        public const int COMPLETED_4 = 4;
    }
}
