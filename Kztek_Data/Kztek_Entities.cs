using System;
using System.Threading.Tasks;
using Kztek_Model.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
//using MySql.Data.MySqlClient;

namespace Kztek_Data
{
    public class Kztek_Entities : DbContext
    {
        public Kztek_Entities(DbContextOptions<Kztek_Entities> options) : base(options)
        {

        }

        //Main
        public DbSet<User> Users { get; set; }

        public DbSet<Role> SY_Roles { get; set; }

        public DbSet<MenuFunction> SY_MenuFunctions { get; set; }

        public DbSet<UserRole> SY_Map_User_Roles { get; set; }

        public DbSet<RoleMenu> SY_Map_Role_Menus { get; set; }

        public DbSet<tblSystemConfig> tblSystemConfigs { get; set; }


        public DbSet<MenuFunctionConfig> MenuFunctionConfigs { get; set; }


        public DbSet<tblLog> tblLogs { get; set; }


        public DbSet<User_AuthGroup> User_AuthGroups { get; set; }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<App> Apps { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<CDKey> CDKeys { get; set; }
        public DbSet<ActiveKey> ActiveKeys { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<tblSystemConfig>(entity =>
            {
                entity.Ignore(e => e.SortOrder);
            }); 

          
          
        }

     
    }
}
