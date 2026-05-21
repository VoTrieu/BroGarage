using BroGarage.API.Shared.RequestModels.Car;
using System.ComponentModel.DataAnnotations;

namespace BroGarage.API.Shared.RequestModels.Customer
{
    public class CustomerAddReqModel : BaseReqModel
    {
        public int TypeId { get; set; }

        private string? _FullName;

        [Required(ErrorMessage = REQ_MSG)]
        [StringLength(70, ErrorMessage = MAX_STR)]
        public string? FullName
        {
            get { return _FullName; }
            set { _FullName = value?.Trim(); }
        }

        private string? _PhoneNumber;

        [Required(ErrorMessage = REQ_MSG)]
        [StringLength(30, ErrorMessage = MAX_STR)]
        public string? PhoneNumber
        {
            get { return _PhoneNumber; }
            set { _PhoneNumber = value?.Trim(); }
        }

        private string _Representative = "";

        [StringLength(70)]
        public string Representative
        {
            get { return _Representative; }
            set { _Representative = value; }
        }

        private string _TaxCode = "";

        [StringLength(50, ErrorMessage = MAX_STR)]
        public string TaxCode
        {
            get { return _TaxCode; }
            set { _TaxCode = value; }
        }

        [StringLength(500, ErrorMessage = MAX_STR)]
        public string Address { get; set; } = "";

        private string _Email = "";

        [StringLength(70, ErrorMessage = MAX_STR)]
        public string Email
        {
            get { return _Email; }
            set { _Email = value.Trim(); }
        }

        [StringLength(500, ErrorMessage = MAX_STR)]
        public string Note { get; set; } = "";

        [StringLength(500, ErrorMessage = MAX_STR)]
        public string AvatarUrl { get; set; } = "";

        public List<CarAddReqModel> Cars { get; set; } = new();
    }
}
