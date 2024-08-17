using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace HospitalManagementAPI.Data
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApiContext>
    {
        public ApiContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();


            var optionsBuilder = new DbContextOptionsBuilder<ApiContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            return new ApiContext(optionsBuilder.Options);
        }
    }
}