using System;
using System.Data.Common;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NGK3Assignment.Data;
using NGK3Assignment.Models;
using Xunit;

namespace NGK3Assignment.Tests
{
    public abstract class TestWithSqlite : IDisposable
    {
        private const string InMemoryConnectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;

        protected readonly DbContext DbContext;

        protected TestWithSqlite()
        {
            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();
            var options = new DbContextOptionsBuilder<DbContext>()
                .UseSqlite(_connection)
                .Options;
            DbContext = new DbContext(options);
            DbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
    public class UnitTest1 : TestWithSqlite
    {
        [Fact]
        public async Task CanBeConnected()
        {
            Assert.True(await DbContext.Database.CanConnectAsync());
        }


        [Fact]
        public void tableShouldGetCreated()
        {
            //arrange
            var wether1 = new WeatherStation()
            {
                Place = "Herning",
                Date = DateTime.Today,
                Celcius = 20,
                Humidity = 50,
                Airpressure = 100
            };
            var wether2 = new WeatherStation()
            {
                Place = "Herning",
                Date = DateTime.Today,
                Celcius = 20,
                Humidity = 50,
                Airpressure = 100
            };
            var wether3 = new WeatherStation()
            {
                Place = "Herning",
                Date = DateTime.Today,
                Celcius = 20,
                Humidity = 50,
                Airpressure = 100
            };

            DbContext.Add(wether1);
            DbContext.Add(wether2);
            DbContext.Add(wether3);
            DbContext.SaveChanges();

            //act

            //assert
            //Assert.Equal(3, DbContext.Find());
        }
    }
}

