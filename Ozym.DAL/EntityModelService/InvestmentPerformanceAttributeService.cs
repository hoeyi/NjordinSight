﻿using Ichosys.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ozym.EntityModel.Context;
using Ozym.EntityModel;
using Ozym.EntityModelService.Abstractions;
using System;

namespace Ozym.EntityModelService
{
    /// <summary>
    /// The class for servicing single CRUD requests against the 
    /// <see cref="InvestmentPerformanceAttributeMemberEntry"/> data store.
    /// </summary>
    internal class InvestmentPerformanceAttributeService : 
        ModelCollectionService<InvestmentPerformanceAttributeMemberEntry>
    {
        /// <summary>
        /// Creates a new <see cref="InvestmentPerformanceAttributeService"/> instance.
        /// </summary>
        /// <param name="contextFactory">An <see cref="IDbContextFactory{FinanceDbContext}" /> 
        /// instance.</param>
        /// <param name="modelMetadata">An <see cref="IModelMetadataService"/> instance.</param>
        /// <param name="logger">An <see cref="ILogger"/> instance.</param>
        public InvestmentPerformanceAttributeService(
                IDbContextFactory<FinanceDbContext> contextFactory,
                IModelMetadataService modelMetadata,
                ILogger logger)
            : base(contextFactory, modelMetadata, logger)
        {
            Reader = new ModelReaderService<InvestmentPerformanceAttributeMemberEntry>(
                ContextFactory, ModelMetadata, Logger)
            {
                IncludeDelegate = (queryable) => queryable
                    .Include(x => x.AttributeMember)
                    .ThenInclude(y => y.Attribute)
            };
        }
    }
}
