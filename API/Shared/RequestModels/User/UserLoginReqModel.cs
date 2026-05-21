using System.ComponentModel.DataAnnotations;

namespace BroGarage.API.Shared.RequestModels.User
{
    public class UserLoginReqModel : BaseReqModel
    {
        [Required(ErrorMessage = REQ_MSG)]
        [MinLength(3, ErrorMessage = MIN_STR)]
        [StringLength(100, ErrorMessage = MAX_STR)]
        public string? UserName { get; set; }

        [Required(ErrorMessage = REQ_MSG)]
        [StringLength(200, ErrorMessage = MAX_STR)]
        public string? Password { get; set; }
    }
}
