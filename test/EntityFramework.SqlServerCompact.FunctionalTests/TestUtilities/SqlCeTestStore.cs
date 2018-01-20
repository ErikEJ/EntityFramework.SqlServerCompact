using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.Internal;

namespace Microsoft.EntityFrameworkCore.TestUtilities
{
    public class SqlCeTestStore : RelationalTestStore
    {
#if SQLCE35
        private const string NorthwindName = "NorthwindEF735";
#else
        private const string NorthwindName = "NorthwindEF7";
#endif
        private static int _scratchCount;

        public static readonly string NorthwindConnectionString = CreateConnectionString(NorthwindName);

        public static SqlCeTestStore GetNorthwindStore()
            => GetOrCreateShared(NorthwindName, () => { });

        public static SqlCeTestStore GetOrCreateShared(string name, Action initializeDatabase) =>
            new SqlCeTestStore(name).CreateShared(initializeDatabase);

        public static SqlCeTestStore Create(string name)
            => new SqlCeTestStore(name).CreateTransient(true);

        public static SqlCeTestStore CreateScratch(bool createDatabase)
        {
            string name;
            do
            {
                name = "scratch-" + Interlocked.Increment(ref _scratchCount);
            }
            while (File.Exists(name + ".sdf"));

            return new SqlCeTestStore(name).CreateTransient(createDatabase);
        }

        public static Task<SqlCeTestStore> CreateScratchAsync(bool createDatabase = true)
        {
            return Task.FromResult(CreateScratch(createDatabase));
        }

        private bool _deleteDatabase;
        private string _name;

        private SqlCeTestStore(string name, bool shared = true) : base(name, shared)
        {
            _name = name;
        }

        private SqlCeTestStore CreateShared(Action initializeDatabase)
        {
            ConnectionString = CreateConnectionString(_name);

            Connection = new SqlCeConnection(ConnectionString);

            if (!Exists())
            {
                initializeDatabase?.Invoke();
            }

            return this;
        }

        private SqlCeTestStore CreateTransient(bool createDatabase)
        {
            ConnectionString = CreateConnectionString(_name);

            Connection = new SqlCeConnection(ConnectionString);

            if (createDatabase)
            {
                ((SqlCeConnection)Connection).CreateEmptyDatabase();
                Connection.Open();
            }

            _deleteDatabase = true;

            return this;
        }

        public int ExecuteNonQuery(string sql, params object[] parameters)
        {
            using (var command = CreateCommand(sql, parameters))
            {
                return command.ExecuteNonQuery();
            }
        }

        public IEnumerable<T> Query<T>(string sql, params object[] parameters)
        {
            using (var command = CreateCommand(sql, parameters))
            {
                using (var dataReader = command.ExecuteReader())
                {
                    var results = Enumerable.Empty<T>();

                    while (dataReader.Read())
                    {
                        results = results.Concat(new[] { dataReader.GetFieldValue<T>(0) });
                    }

                    return results;
                }
            }
        }

        public bool Exists()
        {
            return ((SqlCeConnection)Connection).Exists();
        }

        private DbCommand CreateCommand(string commandText, object[] parameters)
        {
            var command = Connection.CreateCommand();

            command.CommandText = commandText;

            for (var i = 0; i < parameters.Length; i++)
            {
                command.Parameters.Add(new SqlCeParameter("p" + i, parameters[i]));
            }

            return command;
        }

        public override void Dispose()
        {
            if (_deleteDatabase)
            {
                ((SqlCeConnection)Connection).Drop(throwOnOpen: false);
            }
            Connection?.Dispose();
            base.Dispose();
        }

        public static string CreateConnectionString(string name)
        {
#if SQLCE35
            return $"Data Source={name}.sdf";
#else
            return new SqlCeConnectionStringBuilder
            {
                DataSource = name + ".sdf"
            }
            .ToString();
#endif
        }

        public override DbContextOptionsBuilder AddProviderOptions(DbContextOptionsBuilder builder)
            => builder.UseSqlCe(Connection);

        public override void Clean(DbContext context)
            => context.Database.EnsureClean();
    }
}
