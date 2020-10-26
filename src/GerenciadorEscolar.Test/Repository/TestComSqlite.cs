using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using GerenciadorEscolar.Entity;

namespace GerenciadorEscolar.Test.Repository
{
    public abstract class TestComSqlite : IDisposable
    {
        private const string ConnectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;

        protected readonly GerenciadorEscolarDbContext Context;

        protected TestComSqlite()
        {
            _connection = new SqliteConnection(ConnectionString);
            _connection.Open();

            var options = new DbContextOptionsBuilder<GerenciadorEscolarDbContext>()
                .UseSqlite(_connection)
                .Options;

            Context = new GerenciadorEscolarDbContext(options);
            Context.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}