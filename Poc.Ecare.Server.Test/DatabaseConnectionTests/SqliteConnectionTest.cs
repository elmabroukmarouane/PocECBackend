using Microsoft.Extensions.Configuration;
using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using System.IO;

namespace Poc.Ecare.Server.Test.DatabaseConnectionTests
{
    public class SqliteConnectionTest
    {
        private string _connectionString;
        private string _dataSourcePath;
        private readonly IConfiguration _configuration;
        public SqliteConnectionTest()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<Startup>();
            _configuration = builder.Build();
            _connectionString = _configuration["ConnectionStrings:SqliteConnection"];
            using var SqliteConnection = new SqliteConnection(_connectionString);
            _dataSourcePath = Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.FullName + "\\Poc.Ecare.Server", SqliteConnection.DataSource.Replace(@"/", @"\"));
        }

        [Fact]
        public void ConnectionData_ShouldOpenSqliteConnection_WhenConnectionStringIsFilled()
        {
            // Arrange 
            using var SqliteConnectionTest = new SqliteConnection(@"Data Source=" + _dataSourcePath + ";");

            // Act
            SqliteConnectionTest.Open();

            // Assert
            Assert.Equal(ConnectionState.Open, SqliteConnectionTest.State);

        }

        [Fact]
        public void ConnectionData_ShouldReturnClosedSqliteConnection_WhenConnectionStringIsFilled()
        {
            // Arrange 
            using var SqliteConnectionTest = new SqliteConnection(@"Data Source=" + _dataSourcePath + ";");

            // Act
            SqliteConnectionTest.Open();

            // Assert
            Assert.NotEqual(ConnectionState.Closed, SqliteConnectionTest.State);

        }

        [Fact]
        public void ConnectionString_ShouldReturnNotNull_WhenConnectionStringIsNotFilled()
        {
            // Arrange 

            // Act
            _connectionString = !string.IsNullOrEmpty(_connectionString) ? _connectionString : null;

            // Assert
            Assert.NotNull(_connectionString);

        }
    }
}
