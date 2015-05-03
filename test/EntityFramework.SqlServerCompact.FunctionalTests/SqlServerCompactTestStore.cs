using System;
using System.Data.Common;
using System.IO;
using System.Threading;
using Microsoft.Data.Entity.Relational.FunctionalTests;
using System.Data.SqlServerCe;
using ErikEJ.Data.Entity.SqlServerCe.Extensions;
using System.Collections.Generic;
using System.Linq;

namespace ErikEJ.Data.Entity.SqlServerCe.FunctionalTests
{
    public class SqlServerCeTestStore : RelationalTestStore
    {
        private static int _scratchCount;

        //public static SqlServerCeTestStore GetOrCreateShared(string name, Action initializeDatabase) =>
        //    new SqlServerCeTestStore(name).CreateShared(initializeDatabase);

        public static SqlServerCeTestStore CreateScratch(bool createDatabase) =>
            new SqlServerCeTestStore("scratch-" + Interlocked.Increment(ref _scratchCount)).CreateTransient(createDatabase);

        private SqlCeConnection _connection;
        private SqlCeTransaction _transaction;
        private readonly string _name;
        private bool _deleteDatabase;

        public SqlServerCeTestStore(string name)
        {
            _name = name;
        }

        //private SqlServerCeTestStore CreateShared(Action initializeDatabase)
        //{
        //    CreateShared(typeof(SqlServerCeTestStore).Name + _name, initializeDatabase);

        //    _connection = new SqlCeConnection(CreateConnectionString(_name));

        //    _connection.Open();
        //    _transaction = _connection.BeginTransaction();

        //    return this;
        //}

        private SqlServerCeTestStore CreateTransient(bool createDatabase)
        {
            _connection = new SqlCeConnection(CreateConnectionString(_name));

            if (createDatabase)
            {
                _connection.CreateEmptyDatabase();
                _connection.Open();
            }

            _deleteDatabase = true;

            return this;
        }

        public override DbConnection Connection => _connection;
        public override DbTransaction Transaction => _transaction;

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

            if (_transaction != null)
            {
                command.Transaction = _transaction;
            }

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

            _connection.Drop();            

            _connection.Dispose();
        }

        public static string CreateConnectionString(string name) =>
            new SqlCeConnectionStringBuilder
            {
                DataSource = name + ".sdf"
            }
            .ToString();
    }
}
