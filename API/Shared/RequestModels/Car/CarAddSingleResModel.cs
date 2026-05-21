using System.ComponentModel.DataAnnotations;

namespace BroGarage.API.Shared.RequestModels.Car
{
    public class CarAddSingleResModel : BaseReqModel
    {
        public int CustomerId { get; set; }

        public int CarTypeId { get; set; }

        private string? _LicensePlate;

        [Required(ErrorMessage = REQ_MSG)]
        [StringLength(50, ErrorMessage = MAX_STR)]
        public string? LicensePlate
        {
            get { return _LicensePlate; }
            set { _LicensePlate = value?.Trim(); }
        }

        [Range(0, 3_000, ErrorMessage = INVALID_VALUE)]
        public int YearOfManufacture { get; set; }

        private string _VIN = "";

        [StringLength(50, ErrorMessage = MAX_STR)]
        public string VIN
        {
            get { return _VIN; }
            set { _VIN = value.Trim(); }
        }

        [StringLength(500, ErrorMessage = MAX_STR)]
        public string AvatarUrl { get; set; } = "";
    }
}
