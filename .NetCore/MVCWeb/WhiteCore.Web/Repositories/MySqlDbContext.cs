using System;
using System.Runtime.CompilerServices;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WhiteCore.Web.Models;

namespace WhiteCore.Web.Repositories
{
    public class MySqlDbContext : DbContext
    {
        private IConfiguration Configuration;

        public MySqlDbContext(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(Configuration.GetConnectionString("mysqldb"));
        }

        public DbSet<UserEntity> Users { get; set; }
    }
}
