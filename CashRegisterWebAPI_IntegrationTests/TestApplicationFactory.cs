using CashRegister.Infrastructure.Context;
using CashRegisterWebAPI_IntegrationTests.Properties;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
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
        private readonly Action<IServiceCollection> _serviceOverride;
        public TestApplicationFactory(Action<IServiceCollection> serviceOverride = null)
        {
            _serviceOverride = serviceOverride;
        }
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType ==
                        typeof(DbContextOptions<CashRegisterDBContext>));
                if (descriptor != null)
                    services.Remove(descriptor);
                services.AddDbContext<CashRegisterDBContext>(options =>
                {
                    options.UseInMemoryDatabase("InMemoryEmployeeTest");
                });
                var sp = services.BuildServiceProvider();
                using (var scope = sp.CreateScope())
                using (var appContext = scope.ServiceProvider.GetRequiredService<CashRegisterDBContext>())
                {
                    try
                    {
                        Utilities.InitializeDB(appContext);
                        //appContext.Database.EnsureCreated();
                    }
                    catch (Exception ex)
                    {
                        //Log errors or do anything you think it's needed
                        throw;
                    }
                }
            });
        }

        //private readonly Action<IServiceCollection>? _serviceOverride;

        //public TestApplicationFactory(Action<IServiceCollection>? serviceOverride = null)
        //{
        //    _serviceOverride = serviceOverride;
        //}
        //protected override IHost CreateHost(IHostBuilder builder)
        //{
        //    if (_serviceOverride is not null)
        //    {
        //        builder.ConfigureServices(_serviceOverride);
        //    }

        //    return base.CreateHost(builder);
        //}
    }
}