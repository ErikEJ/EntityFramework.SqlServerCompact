// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Relational;
using ErikEJ.Data.Entity.SqlServerCe;
//TODO
//using Microsoft.Data.Entity.Sqlite.Metadata;
//using Microsoft.Data.Entity.Sqlite.Migrations;
//using Microsoft.Data.Entity.Sqlite.Query;
//using Microsoft.Data.Entity.Sqlite.Update;
//using Microsoft.Data.Entity.Sqlite.ValueGeneration;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Utilities;
using ErikEJ.Data.Entity.SqlServerCe.Migrations;
using ErikEJ.Data.Entity.SqlServerCe.Update;

namespace Microsoft.Framework.DependencyInjection
{
    public static class SqliteEntityFrameworkServicesBuilderExtensions
    {
        public static EntityFrameworkServicesBuilder AddSqlServerCe([NotNull] this EntityFrameworkServicesBuilder services)
        {
            Check.NotNull(services, nameof(services));

            //TODO TODO TODO
            ((IAccessor<IServiceCollection>)services.AddRelational()).Service
                .AddSingleton<IDataStoreSource, SqlServerCeDataStoreSource>()
                .TryAdd(new ServiceCollection()
                    //.AddSingleton<ISqliteModelBuilderFactory, SqliteModelBuilderFactory>()
                    //.AddSingleton<ISqliteValueGeneratorCache, SqliteValueGeneratorCache>()
                    .AddSingleton<ISqlServerCeSqlGenerator, SqlServerCeSqlGenerator>()
                    .AddScoped<ISqlStatementExecutor, SqlStatementExecutor>()
                    //.AddScoped<ISqliteTypeMapper, SqliteTypeMapper>()
                    //.AddSingleton<ISqliteModificationCommandBatchFactory, SqliteModificationCommandBatchFactory>()
                    //.AddScoped<ISqliteCommandBatchPreparer, SqliteCommandBatchPreparer>()
                    //.AddSingleton<ISqliteModelSource, SqliteModelSource>()
                    //.AddSingleton<ISqliteValueBufferFactoryFactory, SqliteValueBufferFactoryFactory>()
                    //.AddScoped<ISqliteQueryContextFactory, SqliteQueryContextFactory>()
                    //.AddScoped<ISqliteValueGeneratorSelector, SqliteValueGeneratorSelector>()
                    //.AddScoped<ISqlServerCeBatchExecutor, SqlServerCeBatchExecutor>()
                    .AddScoped<ISqlServerCeDataStoreServices, SqlServerCeDataStoreServices>()
                    .AddScoped<ISqlServerCeDataStore, SqlServerCeDataStore>()
                    .AddScoped<ISqlServerCeConnection, SqlServerCeDataStoreConnection>()
                    //.AddScoped<ISqliteModelDiffer, SqliteModelDiffer>()
                    .AddScoped<ISqlServerCeDatabaseFactory, SqlServerCeDatabaseFactory>()
                    .AddScoped<ISqlServerCeMigrationSqlGenerator, SqlServerCeMigrationSqlGenerator>()
                    .AddScoped<ISqlServerCeDataStoreCreator, SqlServerCeDataStoreCreator>()
                    .AddScoped<ISqlServerCeHistoryRepository, SqlServerCeHistoryRepository>()
                    );

            return services;
        }
    }
}
