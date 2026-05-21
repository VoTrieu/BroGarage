using System.ComponentModel.DataAnnotations;

namespace BroGarage.API.Shared.RequestModels.Manufacturer
{
    public class ManufacturerAddReqModel : BaseReqModel
    {
        private string? _ManufacturerName;

        [Required(ErrorMessage = REQ_MSG)]
        [MaxLength(100, ErrorMessage = MAX_STR)]
        public string? ManufacturerName
        {
            get { return _ManufacturerName; }
            set { _ManufacturerName = value?.Trim(); }
        }
    }
}
