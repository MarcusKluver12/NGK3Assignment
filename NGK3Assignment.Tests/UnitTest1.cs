using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore.Infrastructure;
using NGK3Assignment.Controllers;
using NGK3Assignment.Data;
using NGK3Assignment.Hubs;
using NGK3Assignment.Models;
using Xunit;

namespace NGK3Assignment.Tests
{
    public abstract class TestWithSqlite : IDisposable
    {
        private const string InMemoryConnectionString = "DataSource=:memory:";
        private readonly SqliteConnection _connection;

        protected readonly AppDbContext _DbContext;

        protected TestWithSqlite()
        {
            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlite(_connection)
                .Options;
            _DbContext = new AppDbContext(options);
            _DbContext.Database.EnsureCreated();
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

            Assert.True(await _DbContext.Database.CanConnectAsync());

        }


        [Fact]
        public void tableShouldGetCreated()
        {
            var weather1 = new WeatherStation()
            {
                Place = "her",
                Celcius = 20,
                Humidity = 50
            };

            _DbContext.WeatherStations.Add(weather1);
            _DbContext.SaveChanges();

            Assert.Equal(1, _DbContext.WeatherStations.Count());
        }

        [Fact]
        public void findOneDate()
        {
            var weather1 = new WeatherStation()
            {
                Place = "her",
                Celcius = 20,
                Humidity = 50
            };
            var weather2 = new WeatherStation()
            {
                Place = "her",
                Celcius = 20,
                Humidity = 50,
                Date = DateTime.Today
            };
            var weather3 = new WeatherStation()
            {
                Place = "her",
                Celcius = 20,
                Humidity = 50,
                Date = DateTime.Today.AddDays(-1)
            };

            _DbContext.WeatherStations.Add(weather1);
            _DbContext.WeatherStations.Add(weather2);
            _DbContext.WeatherStations.Add(weather3);
            _DbContext.SaveChanges();

            var weatherController = new WeatherStationsController(_DbContext);

            var weather = new List<Task<List<WeatherStation>>>();

            weather.Add(weatherController.GetTodaysWeatherStations(DateTime.Today.AddDays(-1).ToString()));

            Assert.Equal(1, weather.Count);
        }

        [Fact]
        public void findTwoDates()
        {
            var weather1 = new WeatherStation()
            {
                Place = "her",
                Celcius = 20,
                Humidity = 50
            };
            var weather2 = new WeatherStation()
            {
                Place = "Der",
                Celcius = 29,
                Humidity = 50,
                Date = DateTime.Today.AddDays(-1)
            };
            var weather3 = new WeatherStation()
            {
                Place = "her",
                Celcius = 20,
                Humidity = 50,
                Date = DateTime.Today.AddDays(-1)
            };

            _DbContext.WeatherStations.Add(weather1);
            _DbContext.WeatherStations.Add(weather2);
            _DbContext.WeatherStations.Add(weather3);
            _DbContext.SaveChanges();

            var weatherController = new WeatherStationsController(_DbContext);

            var weatherList  = (weatherController.GetTodaysWeatherStations(DateTime.Today.AddDays(-1).ToString()));

            Assert.Equal(2, weatherList.Result.Count);
        }
    }
}

