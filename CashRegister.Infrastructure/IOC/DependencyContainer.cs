using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using CashRegister.Domain.Core.Bus;
using CashRegister.Infrastructure.InfraBus;
using CashRegister.Domain.Commands;
using CashRegister.Domain.CommandHandlers;
using CashRegister.Application.Interfaces;
using CashRegister.Application.Services;
using CashRegister.Domain.Interfaces;
using CashRegister.Infrastructure.Repositories;
using CashRegister.Infrastructure.Context;

namespace CashRegister.Infrastructure
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Domain-InMemoryBus
            services.AddScoped<IMediatorHandler, InMemoryBus>();
            //Domain handlers
            services.AddScoped<IRequestHandler<CreateBillCommand, bool>, CreateBillCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateBillCommand, bool>, UpdateBillCommandHandler>();
            services.AddScoped<IRequestHandler<AddProductsCommand, bool>, AddProductsToBillHandler>();
            services.AddScoped<IRequestHandler<CreateProductCommand, bool>, CreateProductCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateProductCommand, bool>, UpdateProductCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteBillCommand, bool>, DeleteBillCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteProductCommand, bool>, DeleteProductCommandHandler>();
            //Application layer
            services.AddScoped<IBillService, BillService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IProductBillService, ProductBillService>();
            services.AddScoped<ICurrencyExchangeService, CurrencyExchangeService>();
            //Infrastructure layer
            services.AddScoped<IProductBillRepository, ProductBillRepository>();
            services.AddScoped<IBillRepository, BillRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<CashRegisterDBContext>();
        }
    }
}
