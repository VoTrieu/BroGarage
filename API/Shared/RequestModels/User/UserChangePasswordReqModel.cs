using System.ComponentModel.DataAnnotations;

namespace BroGarage.API.Shared.RequestModels.User
{
    public class UserChangePasswordReqModel : BaseReqModel
    {
        [Required(ErrorMessage = REQ_MSG)]
        [StringLength(50, ErrorMessage = MAX_STR)]
        public string? CurrentPassword { get; set; }

        [Required(ErrorMessage = REQ_MSG)]
        [StringLength(50, ErrorMessage = MAX_STR)]
        public string? NewPassword { get; set; }

        [Required(ErrorMessage = REQ_MSG)]
        [StringLength(50, ErrorMessage = MAX_STR)]
        public string? NewPasswordConfirm { get; set; }
    }
}
