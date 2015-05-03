// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using ErikEJ.Data.Entity.SqlServerCe.Migrations;
using JetBrains.Annotations;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata.Builders;
using Microsoft.Data.Entity.Query;
using Microsoft.Data.Entity.Relational;
using Microsoft.Data.Entity.Relational.Migrations.History;
using Microsoft.Data.Entity.Relational.Migrations.Infrastructure;
using Microsoft.Data.Entity.Relational.Migrations.Sql;
//TODO
//using Microsoft.Data.Entity.Sqlite.Metadata;
//using Microsoft.Data.Entity.Sqlite.Query;
//using Microsoft.Data.Entity.Sqlite.ValueGeneration;
using Microsoft.Data.Entity.Storage;
using Microsoft.Data.Entity.Utilities;
using Microsoft.Data.Entity.ValueGeneration;
using Microsoft.Framework.DependencyInjection;

namespace ErikEJ.Data.Entity.SqlServerCe
{
    public class SqlServerCeDataStoreServices : ISqlServerCeDataStoreServices
    {
        private readonly IServiceProvider _services;

        public SqlServerCeDataStoreServices([NotNull] IServiceProvider services)
        {
            Check.NotNull(services, nameof(services));

            _services = services;
        }

        public virtual IDataStoreConnection Connection => _services.GetRequiredService<ISqlServerCeConnection>();
        public virtual IDataStoreCreator Creator => _services.GetRequiredService<ISqlServerCeDataStoreCreator>();
        public virtual IDatabaseFactory DatabaseFactory => _services.GetRequiredService<ISqlServerCeDatabaseFactory>();
        public virtual IHistoryRepository HistoryRepository => _services.GetRequiredService<ISqlServerCeHistoryRepository>();
        public virtual IMigrationSqlGenerator MigrationSqlGenerator => _services.GetRequiredService<ISqlServerCeMigrationSqlGenerator>();
        //TODO Implement!
        //public virtual IModelBuilderFactory ModelBuilderFactory => _services.GetRequiredService<ISqliteModelBuilderFactory>();
        //public virtual IModelDiffer ModelDiffer => _services.GetRequiredService<ISqliteModelDiffer>();
        //public virtual IModelSource ModelSource => _services.GetRequiredService<ISqliteModelSource>();
        //public virtual IQueryContextFactory QueryContextFactory => _services.GetRequiredService<ISqliteQueryContextFactory>();
        public virtual IRelationalConnection RelationalConnection => _services.GetRequiredService<ISqlServerCeConnection>();
        public virtual ISqlGenerator SqlGenerator => _services.GetRequiredService<ISqlServerCeSqlGenerator>();
        public virtual IDataStore Store => _services.GetRequiredService<ISqlServerCeDataStore>();
        //public virtual IValueGeneratorSelector ValueGeneratorSelector => _services.GetRequiredService<ISqliteValueGeneratorSelector>();
    }
}
