using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace QuanLyTiecCuoi.Data
{
    public class WeddingDbContextFactory : IDesignTimeDbContextFactory<WeddingDbContext>
    {
        public WeddingDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<WeddingDbContext>();

            // Đọc connection string từ appsettings.json hoặc hardcode tùy bạn
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));

            return new WeddingDbContext(optionsBuilder.Options);
        }
    }
}
