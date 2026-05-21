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
                FullName = "Quản trị viên",
                Salt = "ztSiY7kw5joGzZcwdtKqztSiY7kw5joGzZcwdtKqztSiY7kw5joGzZcwdtKqztSiY7kw5joGzZcwdtKqztSiY7kw5joGzZcwdtKqztSiY7kw5joGzZcwdtKqztSiY7kw",
                PasswordHash = "ea871309f6e9a5490bd909aaf2f80f57a7dca87397af1d454c95652fc4201deb",
                CreatedDate = CreatedDate,
                CreatedTime = CreatedTime,
                CreatedTimeStamp = CreatedTimeStamp
            }
        };
    }
}
