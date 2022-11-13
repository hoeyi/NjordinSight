﻿using Ichosys.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NjordFinance.Context;
using NjordFinance.Model;
using NjordFinance.Model.ConstraintType;
using NjordFinance.ModelService.Abstractions;

namespace NjordFinance.ModelService
{
    /// <summary>
    /// The class for servicing single CRUD requests against the <see cref="SecurityType"/> 
    /// data store.
    /// </summary>
    internal class SecurityTypeService : ModelService<SecurityType>
    {
        /// <summary>
        /// Creates a new <see cref="SecurityTypeService"/> instance.
        /// </summary>
        /// <param name="contextFactory">An <see cref="IDbContextFactory{FinanceDbContext}" /> 
        /// instance.</param>
        /// <param name="modelMetadata">An <see cref="IModelMetadataService"/> instance.</param>
        /// <param name="logger">An <see cref="ILogger"/> instance.</param>
        public SecurityTypeService(
                IDbContextFactory<FinanceDbContext> contextFactory,
                IModelMetadataService modelMetadata,
                ILogger logger)
            : base(contextFactory, modelMetadata, logger)
        {
            Reader = new ModelReaderService<SecurityType>(
                contextFactory, modelMetadata, logger)
            {
                IncludeDelegate = (queryable) => queryable
                    .Include(a => a.AttributeMemberNavigation)
                    .Include(a => a.SecurityTypeGroup)
            };

            Writer = new ModelWriterService<SecurityType>(
                contextFactory, modelMetadata, logger)
            {
                CreateDelegate = async (context, model) =>
                {
                    model.AttributeMemberNavigation.DisplayName = model.SecurityTypeName;

                    var result = await context
                        .MarkForCreation(model)
                        .SaveChangesAsync() > 0;

                    return new DbActionResult<SecurityType>(model, result);
                },
                GetDefaultDelegate = () => new SecurityType()
                {
                    AttributeMemberNavigation = new()
                    {
                        AttributeId = (int)ModelAttributeEnum.SecurityType
                    }
                },
                UpdateDelegate = async (context, model) =>
                {
                    model.AttributeMemberNavigation.DisplayName = model.SecurityTypeName;

                    var result = await context
                        .MarkForUpdate(model)
                        .SaveChangesAsync() > 0;

                    return new DbActionResult<bool>(result, result);
                }
            };
        }
    }
}
