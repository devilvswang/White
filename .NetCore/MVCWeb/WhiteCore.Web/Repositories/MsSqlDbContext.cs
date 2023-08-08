using System;
using WhiteCore.Web.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace WhiteCore.Web.Repositories
{
    public class MsSqlDbContext : DbContext
    {
        private IConfiguration Configuration;

        public MsSqlDbContext(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptions)
        {
            dbContextOptions.UseSqlServer(Configuration.GetConnectionString("mssqldb"), builder =>
            {
                builder.EnableRetryOnFailure(
                    maxRetryCount: 5,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: new int[] { 2 });
            });
        }

        public DbSet<UserInfoEntity> UserInfo { get; set; }
    }
}
