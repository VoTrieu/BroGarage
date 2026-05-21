using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BroGarage.API.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "PasswordHash", "Salt" },
                values: new object[] { "9c8bddf81ddbcdb97f891eda3cd108376a90fb2d45f4f0276cdfc8e3feaba2ea", "uh4naWyaApHldoTdVgLaOCi853g66HOIg51KMhhdo0vSZFCCoZsleyTVNVKUg7Ds1pfmSgGkyfpthBcpcpTP7fOx6C9uFWBpNI6WBKiVFzXYk9bkKin1JuVBYhaUXFHJ" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                columns: new[] { "PasswordHash", "Salt" },
                values: new object[] { "ea871309f6e9a5490bd909aaf2f80f57a7dca87397af1d454c95652fc4201deb", "ztSiY7kw5joGzZcwdtKqztSiY7kw5joGzZcwdtKqztSiY7kw5joGzZcwdtKqztSiY7kw5joGzZcwdtKqztSiY7kw5joGzZcwdtKqztSiY7kw5joGzZcwdtKqztSiY7kw" });
        }
    }
}
