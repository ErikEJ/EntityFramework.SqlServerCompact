using System;
using Xunit;

namespace Microsoft.EntityFrameworkCore
{
    public class Issue305Test
    {
        [Fact]
        public void Issue305_Test()
        {
            using (var db = new TiffFilesContext())
            {
                db.Database.EnsureDeleted();

                db.Database.Migrate();
            }
        }

        public class TiffFilesContext : DbContext
        {
            public DbSet<FileInfo> Files { get; set; }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlCe(@"Data Source=Issue305Database.sdf");
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                modelBuilder.Entity<FileInfo>().Property(f => f.Path).IsRequired();
            }
        }

        public class FileInfo
        {
            public int FileInfoId { get; set; }
            public string Path { get; set; }
            public string BlindedName { get; set; }
            public bool ContainsSynapse { get; set; }
            public int Quality { get; set; }
        }
    }
}
