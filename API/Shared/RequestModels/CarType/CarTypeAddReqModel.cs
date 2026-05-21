using System.ComponentModel.DataAnnotations;

namespace BroGarage.API.Shared.RequestModels.CarType
{
    public class CarTypeAddReqModel : BaseReqModel
    {
        public int ManufacturerId { get; set; }

        private string? _TypeName;

        [Required(ErrorMessage = REQ_MSG)]
        [MaxLength(100, ErrorMessage = MAX_STR)]
        public string? TypeName
        {
            get { return _TypeName; }
            set { _TypeName = value?.Trim(); }
        }
    }
}
