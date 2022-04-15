﻿//using CashRegister.Infrastructure.Context;
//using Microsoft.AspNetCore.Hosting;
//using Microsoft.AspNetCore.Mvc.Testing;
//using Microsoft.EntityFrameworkCore.InMemory;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CashRegisterWebAPI_IntegrationTests
//{
//    public class TestingWebAppFactory<TEntryPoint> : WebApplicationFactory<Program> where TEntryPoint : Program
//    {
//        protected override void ConfigureWebHost(IWebHostBuilder builder)
//        {
//            builder.ConfigureServices(services =>
//            {
//                var descriptor = services.SingleOrDefault(
//                    d => d.ServiceType ==
//                    typeof(DbContextOptions<CashRegisterDBContext>));
//                if (descriptor != null)
//                    services.Remove(descriptor);

//                services.AddDbContext<CashRegisterDBContext>(options =>
//                {
//                    options.UseInMemoryDatabase("InMemoryCashRegisterTest");
//                });
//                var sp = services.BuildServiceProvider();
//                using (var scope = sp.CreateScope())
//                using (var appContext = scope.ServiceProvider.GetRequiredService<CashRegisterDBContext>())
//                {
//                    try
//                    {
//                        if (appContext.Database.ProviderName != "Microsoft.EntityFrameworkCore.InMemory")
//                            appContext.Database.Migrate();
//                            //appContext.Database.EnsureCreated();
//                    }
//                    catch (Exception ex)
//                    {
//                        throw;
//                    }
//                }
//            });
//        }
//    }
//}
