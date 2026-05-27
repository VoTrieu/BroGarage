using BroGarage.API.Data.Entities;

namespace BroGarage.API.Data.Seeds
{
    public class UserSeed : BaseSeed
    {
        public static List<UserEntity> Data => new()
        {
            new UserEntity()
            {
                UserId = 1,
                UserName = "admin",
                FullName = "Admin",
                Salt = "uh4naWyaApHldoTdVgLaOCi853g66HOIg51KMhhdo0vSZFCCoZsleyTVNVKUg7Ds1pfmSgGkyfpthBcpcpTP7fOx6C9uFWBpNI6WBKiVFzXYk9bkKin1JuVBYhaUXFHJ",
                PasswordHash = "9c8bddf81ddbcdb97f891eda3cd108376a90fb2d45f4f0276cdfc8e3feaba2ea",
                CreatedDate = CreatedDate,
                CreatedTime = CreatedTime,
                CreatedTimeStamp = CreatedTimeStamp
            }
        };
    }
}
