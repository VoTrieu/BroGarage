namespace BroGarage.API.Shared.ResponseModels.User
{
    public class UserLoginResModel
    {
        public string UserName { get; set; } = "";

        public string FullName { get; set; } = "";

        public string AccessToken { get; set; } = "";

        public string RefreshToken { get; set; } = "";

        public long ExpirationTime { get; set; }
    }
}
