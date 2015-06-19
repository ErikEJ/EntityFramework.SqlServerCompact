using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.FunctionalTests;
using Microsoft.Data.Entity.FunctionalTests.TestModels.Inheritance;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Relational.FunctionalTests;
using Microsoft.Framework.DependencyInjection;
using Microsoft.Framework.Logging;

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests
{
    public class InheritanceSqlCeFixture : InheritanceFixtureBase
    {
        private readonly DbContextOptions _options;
        private readonly IServiceProvider _serviceProvider;

        public InheritanceSqlCeFixture()
        {
            _serviceProvider
                = new ServiceCollection()
                    .AddEntityFramework()
                    .AddSqlCe()
                    .ServiceCollection()
                    .AddSingleton(TestSqlCeModelSource.GetFactory(OnModelCreating))
                    .AddInstance<ILoggerFactory>(new TestSqlLoggerFactory())
                    .BuildServiceProvider();

            var testStore = SqlCeTestStore.CreateScratch(createDatabase: true);

            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseSqlCe(testStore.Connection);
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
                ALTER TABLE[Animal]
                    ADD CONSTRAINT[EagleId_FK]
                    FOREIGN KEY ([EagleId])
                    REFERENCES[Animal]([Species]) 
                    ON DELETE NO ACTION ON UPDATE NO ACTION;");

            testStore.ExecuteNonQuery(@"
                ALTER TABLE[Animal]
                    ADD CONSTRAINT[CountryId_FK]
                    FOREIGN KEY ([CountryId])
                    REFERENCES[Country]([Id]) 
                    ON DELETE NO ACTION ON UPDATE NO ACTION;");

            using (var context = CreateContext())
            {
                SeedData(context);
            }
        }

        public override AnimalContext CreateContext()
        {
            return new AnimalContext(_serviceProvider, _options);
        }

        public override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // TODO: Code First this

            var animal = modelBuilder.Entity<Animal>().Metadata;

            var discriminatorProperty
                = animal.AddProperty("Discriminator", typeof(string), shadowProperty: true);

            discriminatorProperty.IsNullable = false;
            //discriminatorProperty.IsReadOnlyBeforeSave = true; // #2132
            discriminatorProperty.IsReadOnlyAfterSave = true;
            discriminatorProperty.IsValueGeneratedOnAdd = true;

            animal.Relational().DiscriminatorProperty = discriminatorProperty;
        }
    }
}
