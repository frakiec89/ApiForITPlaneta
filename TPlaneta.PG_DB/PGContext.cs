using Microsoft.EntityFrameworkCore;
using Npgsql.EntityFrameworkCore.PostgreSQL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TPlaneta.PG_DB
{
    public class PGContext : DbContext 
    {
        //private string connectionString =
        //    "Server=192.168.49.180;port=5432; database = DbForTPanet; user id = stud; password=stud;";

        private string connectionString =
            "Server=85.236.170.146; port=5432; database = DbForTPanet; user id = stud; password=stud;";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(connectionString);
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Session> Sessions { get; set; }
    }
}
