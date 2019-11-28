using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace Test_DateTimeOffset_MySQL
{
    public class SimpleEntityContext : DbContext
    {
        public DbSet<SimpleEntity> SimpleEntities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseMySql("Server=localhost;Database=TestDateTimeOffset;User=root;Password=1234;",
                    mySqlOptions =>
                    {
                        mySqlOptions.ServerVersion(new Version(8, 0, 12), ServerType.MySql);
                    });
        }
    }
}
