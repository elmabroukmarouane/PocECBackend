using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Poc.Ecare.Server.Test.DatabaseConnectionTests
{
    public class OracleConnectionTest
    {
        private string _connectionString;
        private readonly IConfiguration _configuration;
        public OracleConnectionTest()
        {
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddUserSecrets<Startup>();
            _configuration = builder.Build();
            _connectionString = _configuration["ConnectionStrings:OracleConnection"];
        }

        //[Fact]
        //public void ConnectionData_ShouldOpenOracleConnection_WhenConnectionStringIsFilled()
        //{
        //    // Arrange 
        //    using var oracleConnection = new OracleConnection(_connectionString);

        //    // Act
        //    oracleConnection.Open();

        //    // Assert
        //    Assert.Equal(ConnectionState.Open, oracleConnection.State);

        //}

        //[Fact]
        //public void ConnectionData_ShouldReturnClosedOracleConnection_WhenConnectionStringIsFilled()
        //{
        //    // Arrange 
        //    using var oracleConnection = new OracleConnection(_connectionString);

        //    // Act
        //    oracleConnection.Open();

        //    // Assert
        //    Assert.NotEqual(ConnectionState.Closed, oracleConnection.State);

        //}

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
