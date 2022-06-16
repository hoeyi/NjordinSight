﻿using NjordFinance.Controllers;
using NjordFinance.Controllers.Abstractions;
using NjordFinance.Model;
using NjordFinance.ModelService;
using Microsoft.Extensions.DependencyInjection;

namespace NjordFinance
{
    /// <summary>
    /// Container for service registration extension methods to support 
    /// depedency injection for external projects.
    /// </summary>
    public static class DependencyInjectionExtension
    {
        /// <summary>
        /// Adds the model services to the collection.
        /// </summary>
        /// <param name="services"></param>
        public static void AddModelServices(this IServiceCollection services)
        {
            // Add reference data service for querying lookup lists.
            services.AddScoped<IReferenceDataService, ReferenceDataService>();

            services
                .AddScoped<IModelService<Account>, AccountService>()
                .AddScoped<IModelService<AccountCustodian>, AccountCustodianService>()
                .AddScoped<IModelBatchService<AccountWallet>, AccountWalletService>();
        }

        /// <summary>
        /// Adds the controllers to the collection.
        /// </summary>
        /// <param name="services"></param>
        public static void AddModelControllers(this IServiceCollection services)
        {
            services
                .AddScoped<IController<Account>, ModelController<Account>>()
                .AddScoped<IController<AccountCustodian>, ModelController<AccountCustodian>>()
                .AddScoped<IBatchController<AccountWallet>, ModelBatchController<AccountWallet>>();
        }
    }
}
