using System;
using System.Collections.Generic;
using Xunit;

namespace Microsoft.EntityFrameworkCore
{
    public class Issue541Test
    {
        [Fact]
        public void Issue541_Test()
        {
            using (var db = new Issue541Context())
            {
                db.Database.EnsureDeleted();
                db.Database.EnsureCreated();

                db.ServerData.AddRange(new List<ServerData>
                {
                    new ServerData { ServerId = 6, Type = 1, Value = 5, Date = DateTime.UtcNow },
                    new ServerData { ServerId = 6, Type = 2, Value = 6.56m, Date = DateTime.UtcNow },
                });

                //Fix issue by supplying a date

                db.SaveChanges();
            }
        }

        public class Issue541Context : DbContext
        {
            public DbSet<ServerData> ServerData { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder dbContextOptionsBuilder)
            {
                dbContextOptionsBuilder.UseSqlCe(@"Data Source=Issue541.sdf");
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<ServerData>(entity =>
                {
                    entity.HasKey(e => new { e.ServerId, e.Type, e.Date });

                    entity.Property(e => e.Date)
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("getdate()");

                    entity.Property(e => e.Value).HasColumnType("decimal(15, 1)");
                });
            }
        }

        public partial class ServerData
        {
            public long ServerId { get; set; }
            public DateTime Date { get; set; }
            public short Type { get; set; }
            public decimal Value { get; set; }
        }
    }
}
