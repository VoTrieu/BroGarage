using System.ComponentModel.DataAnnotations;

namespace BroGarage.API.Shared.RequestModels.User
{
    public class UserRefreshTokenReqModel : BaseReqModel
    {
        [Required(ErrorMessage = REQ_MSG)]
        [MaxLength(50, ErrorMessage = MAX_STR)]
        public string? RefreshToken { get; set; }
    }
}
