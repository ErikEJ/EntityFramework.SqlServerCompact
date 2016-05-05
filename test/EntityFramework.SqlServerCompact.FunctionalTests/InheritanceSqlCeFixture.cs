using System;
using Microsoft.EntityFrameworkCore.Specification.Tests.TestModels.Inheritance;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
{
    public class InheritanceSqlCeFixture : InheritanceRelationalFixture
    {
        private readonly DbContextOptions _options;
        private readonly IServiceProvider _serviceProvider;

        public InheritanceSqlCeFixture()
        {
            _serviceProvider
                = new ServiceCollection()
                    .AddEntityFrameworkSqlCe()
                    .AddSingleton(TestSqlCeModelSource.GetFactory(OnModelCreating))
                    .AddSingleton<ILoggerFactory>(new TestSqlLoggerFactory())
                    .BuildServiceProvider();

            var testStore = SqlCeTestStore.CreateScratch(createDatabase: true);

            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder
                .EnableSensitiveDataLogging()
                .UseSqlCe(testStore.Connection)
                .UseInternalServiceProvider(_serviceProvider);

            _options = optionsBuilder.Options;

            // TODO: Do this via migrations

            testStore.ExecuteNonQuery(@"
                CREATE TABLE Country (
                    Id int NOT NULL PRIMARY KEY,
                    Name nvarchar(100) NOT NULL
                );");

            testStore.ExecuteNonQuery(@"
                CREATE TABLE Animal (
                    Species nvarchar(100) NOT NULL PRIMARY KEY,
                    Name nvarchar(100) NOT NULL,
                    CountryId int NOT NULL,
                    IsFlightless bit NOT NULL,
                    EagleId nvarchar(100),
                    [Group] int,
                    FoundOn tinyint,
                    Discriminator nvarchar(255) NOT NULL
                );");

            testStore.ExecuteNonQuery(@"
                ALTER TABLE [Animal]
                    ADD CONSTRAINT[EagleId_FK]
                    FOREIGN KEY ([EagleId])
                    REFERENCES[Animal]([Species]) 
                    ON DELETE NO ACTION ON UPDATE NO ACTION;");

            testStore.ExecuteNonQuery(@"
                ALTER TABLE [Animal]
                    ADD CONSTRAINT[CountryId_FK]
                    FOREIGN KEY ([CountryId])
                    REFERENCES[Country]([Id]) 
                    ON DELETE NO ACTION ON UPDATE NO ACTION;");

            testStore.ExecuteNonQuery(@"
                CREATE TABLE Plant(
                    Genus int NOT NULL,
                    Species nvarchar(100) NOT NULL PRIMARY KEY,
                    Name nvarchar(100) NOT NULL,
                    CountryId int NULL,
                    HasThorns bit
                );");

            testStore.ExecuteNonQuery(@"
                ALTER TABLE [Plant]
                    ADD CONSTRAINT[PlantCountryId_FK]
                    FOREIGN KEY ([CountryId])
                    REFERENCES[Country]([Id]) 
                    ON DELETE NO ACTION ON UPDATE NO ACTION;");

            using (var context = CreateContext())
            {
                SeedData(context);
            }
        }

        public override InheritanceContext CreateContext() => new InheritanceContext(_options);
    }
}
