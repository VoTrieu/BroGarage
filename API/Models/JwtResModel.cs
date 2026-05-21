namespace BroGarage.API.Models;

public class JwtResModel
{
    public string AccessToken { get; set; } = "";

    public long ExpirationTime { get; set; }

    public string RefreshToken { get; set; } = "";
}
