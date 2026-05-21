using System.ComponentModel.DataAnnotations;

namespace BroGarage.API.Shared.RequestModels.Product
{
    public class ProductEditReqModel : BaseReqModel
    {
        public int ProductId { get; set; }

        private string? _ProductCode;

        [Required(ErrorMessage = REQ_MSG)]
        [StringLength(50, ErrorMessage = MAX_STR)]
        public string? ProductCode
        {
            get { return _ProductCode; }
            set { _ProductCode = value?.Trim(); }
        }

        private string? _ProductName;

        [Required(ErrorMessage = REQ_MSG)]
        [StringLength(100, ErrorMessage = MAX_STR)]
        public string? ProductName
        {
            get { return _ProductName; }
            set { _ProductName = value?.Trim(); }
        }

        [StringLength(500, ErrorMessage = MAX_STR)]
        public string Remark { get; set; } = "";

        [StringLength(50, ErrorMessage = MAX_STR)]
        public string UnitName { get; set; } = "";

        [Range(0, long.MaxValue, ErrorMessage = INVALID_VALUE)]
        public long UnitPrice { get; set; }

        [Range(int.MinValue, int.MaxValue, ErrorMessage = INVALID_VALUE)]
        public int Quantity { get; set; }

        [MaxLength(500, ErrorMessage = MAX_STR)]
        public string AvatarUrl { get; set; } = "";
    }
}
