﻿using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;

namespace Test_HandlingConcurrencyConflicts
{
    public class PersonContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Requires NuGet package Microsoft.EntityFrameworkCore.SqlServer
            optionsBuilder
                .UseLazyLoadingProxies()
                .UseMySql("Server=localhost;Database=ConcurrencyConflicts;User=root;Password=1234;",
                    mySqlOptions =>
                {
                    //TODO maybe use MariaDB?
                    mySqlOptions.ServerVersion(new Version(8, 0, 12), ServerType.MySql);
                });
        }
    }
}