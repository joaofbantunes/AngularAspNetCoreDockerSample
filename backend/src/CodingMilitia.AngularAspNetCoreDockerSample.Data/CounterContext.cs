using CodingMilitia.AngularAspNetCoreDockerSample.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodingMilitia.AngularAspNetCoreDockerSample.Data
{
    public class CounterContext : DbContext
    {
        public DbSet<Counter> Counters { get; set; }
        public CounterContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("public");

            modelBuilder.Entity<Counter>()
                .HasKey(e => e.Id);

            modelBuilder.Entity<Counter>()
                .Property(e => e.Id)
                .UseNpgsqlSerialColumn();
        }
    }
}