using HospitalManagementAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HospitalManagementAPI.Data
{
    public class ApiContext : DbContext
    {

        public DbSet<Hospital> Hospitals { get; set; } = null!;

        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {

        }
    }
}
