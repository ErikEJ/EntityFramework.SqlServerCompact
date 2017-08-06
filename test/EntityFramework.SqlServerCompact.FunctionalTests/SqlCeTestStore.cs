using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlServerCe;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage.Internal;

namespace Microsoft.EntityFrameworkCore.Specification.Tests
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
            => SqlCeTestStore.GetOrCreateShared(NorthwindName, () => { });

        public static SqlCeTestStore GetOrCreateShared(string name, Action initializeDatabase) =>
            new SqlCeTestStore(name).CreateShared(initializeDatabase);

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

        private SqlCeConnection _connection;
        //private SqlCeTransaction _transaction;
        private readonly string _name;
        private string _connectionString;
        private bool _deleteDatabase;

        public override string ConnectionString => _connectionString;

        public SqlCeTestStore(string name)
        {
            _name = name;
        }

        private SqlCeTestStore CreateShared(Action initializeDatabase)
        {
            _connectionString = CreateConnectionString(_name);
            _connection = new SqlCeConnection(_connectionString);

            CreateShared(typeof(SqlCeTestStore).Name + _name,
                () =>
                {
                    if (!Exists())
                    {
                        initializeDatabase?.Invoke();
                    }
                });

            return this;
        }

        private SqlCeTestStore CreateTransient(bool createDatabase)
        {
            _connectionString = CreateConnectionString(_name);

            _connection = new SqlCeConnection(_connectionString);

            if (createDatabase)
            {
                _connection.CreateEmptyDatabase();
                _connection.Open();
            }

            _deleteDatabase = true;

            return this;
        }

        public override DbConnection Connection => _connection;
        public override DbTransaction Transaction => null;

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
            return _connection.Exists();
        }

        private DbCommand CreateCommand(string commandText, object[] parameters)
        {
            var command = _connection.CreateCommand();

            command.CommandText = commandText;

            for (var i = 0; i < parameters.Length; i++)
            {
                command.Parameters.AddWithValue("p" + i, parameters[i]);
            }

            return command;
        }

        public override void Dispose()
        {
            Transaction?.Dispose();

            if (_deleteDatabase)
            {
                _connection.Drop(throwOnOpen: false);
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
    }
}
