using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Relational.Migrations.History;
using Microsoft.Data.Entity.Relational.Migrations.Operations;

namespace ErikEJ.Data.Entity.SqlServerCompact.Migrations
{
    public class SqlServerCompactHistoryRepository : ISqlServerCompactHistoryRepository
    {
        public virtual string BeginIfExists(string migrationId)
        {
            throw new NotImplementedException();
        }

        public virtual string BeginIfNotExists(string migrationId)
        {
            throw new NotImplementedException();
        }

        public virtual string Create(bool ifNotExists)
        {
            throw new NotImplementedException();
        }

        public virtual string EndIf()
        {
            throw new NotImplementedException();
        }

        public virtual bool Exists()
        {
            throw new NotImplementedException();
        }

        public virtual IReadOnlyList<IHistoryRow> GetAppliedMigrations()
        {
            throw new NotImplementedException();
        }

        public virtual MigrationOperation GetDeleteOperation(string migrationId)
        {
            throw new NotImplementedException();
        }

        public virtual MigrationOperation GetInsertOperation(IHistoryRow row)
        {
            throw new NotImplementedException();
        }
    }
}
