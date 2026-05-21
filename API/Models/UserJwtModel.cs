namespace BroGarage.API.Models;

public class UserJwtModel
{
    public int UserId { get; set; }

    public string UserName { get; set; } = "";

    public string FullName { get; set; } = "";
}
