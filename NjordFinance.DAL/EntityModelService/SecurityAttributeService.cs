﻿using Ichosys.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NjordFinance.EntityModel.Context;
using NjordFinance.EntityModel;
using NjordFinance.EntityModelService.Abstractions;
using System;

namespace NjordFinance.EntityModelService
{
    /// <summary>
    /// The class for servicing single CRUD requests against the 
    /// <see cref="SecurityAttributeMemberEntry"/> data store.
    /// </summary>
    internal class SecurityAttributeService : ModelBatchService<SecurityAttributeMemberEntry>
    {
        /// <summary>
        /// Creates a new <see cref="SecurityAttributeService"/> instance.
        /// </summary>
        /// <param name="contextFactory">An <see cref="IDbContextFactory{FinanceDbContext}" /> 
        /// instance.</param>
        /// <param name="modelMetadata">An <see cref="IModelMetadataService"/> instance.</param>
        /// <param name="logger">An <see cref="ILogger"/> instance.</param>
        public SecurityAttributeService(
                IDbContextFactory<FinanceDbContext> contextFactory,
                IModelMetadataService modelMetadata,
                ILogger logger)
            : base(contextFactory, modelMetadata, logger)
        {
        }

        public override bool ForParent(int parentId, out Exception e)
        {
            Reader = new ModelReaderService<SecurityAttributeMemberEntry>(
                Context, _modelMetadata, _logger)
            {
                ParentExpression = x => x.SecurityId == parentId
            };

            Writer = new ModelWriterBatchService<SecurityAttributeMemberEntry>(
                Context, _modelMetadata, _logger)
            {
                ParentExpression = x => x.SecurityId == parentId,
                GetDefaultModelDelegate = () => new()
                {
                    SecurityId = parentId
                }
            };

            e = null;
            return true;
        }
    }
}