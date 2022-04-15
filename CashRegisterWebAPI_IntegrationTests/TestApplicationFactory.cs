using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CashRegisterWebAPI_IntegrationTests
{
    [TestFixture]
    internal class TestApplicationFactory : WebApplicationFactory<Program>
    {
        private readonly Action<IServiceCollection>? _serviceOverride;

        public TestApplicationFactory(Action<IServiceCollection>? serviceOverride = null)
        {
            _serviceOverride = serviceOverride;
        }
        protected override IHost CreateHost(IHostBuilder builder)
        {
            if (_serviceOverride is not null)
            {
                builder.ConfigureServices(_serviceOverride);
            }

            return base.CreateHost(builder);
        }
    }
}