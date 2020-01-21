﻿using System;
using Yuniql.Core;
using Yuniql.MySql;
using Yuniql.PostgreSql;
using Yuniql.SqlServer;

namespace Yuniql.PlatformTests
{
    //https://github.com/dotnet/samples/tree/master/core/tutorials/Unloading
    public class TestDataServiceFactory : ITestDataServiceFactory
    {
        public TestDataServiceFactory()
        {
        }

        public ITestDataService Create(string platform)
        {
            var traceService = new FileTraceService();
            switch (platform.ToLower())
            {
                case "sqlserver":
                    {
                        return new SqlServerTestDataService(new SqlServerDataService(traceService));
                    }
                case "postgresql":
                    {
                        return new PostgreSqlTestDataService(new PostgreSqlDataService(traceService));
                    }
                case "mysql":
                    {
                        return new MySqlTestDataService(new MySqlDataService(traceService));
                    }
                default:
                    throw new NotSupportedException($"The target database platform {platform} is not supported or plugins location was not correctly configured. " +
                        $"See WIKI for supported database platforms and usage guide.");
            }
        }
    }
}

