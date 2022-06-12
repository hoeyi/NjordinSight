﻿using Ichosoft.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NjordFinance.Context;
using NjordFinance.Model;
using NjordFinance.ModelService.Abstractions;
using System;

namespace NjordFinance.ModelService
{
    /// <summary>
    /// The class for servicing single CRUD requests against the 
    /// <see cref="SecurityAttributeMemberEntry"/> data store.
    /// </summary>
    internal class SecurityAttributeMemberservice : ModelBatchService<SecurityAttributeMemberEntry>
    {
        /// <summary>
        /// Creates a new <see cref="SecurityAttributeMemberservice"/> instance.
        /// </summary>
        /// <param name="contextFactory">An <see cref="IDbContextFactory{FinanceDbContext}" /> 
        /// instance.</param>
        /// <param name="modelMetadata">An <see cref="IModelMetadataService"/> instance.</param>
        /// <param name="logger">An <see cref="ILogger"/> instance.</param>
        public SecurityAttributeMemberservice(
                IDbContextFactory<FinanceDbContext> contextFactory,
                IModelMetadataService modelMetadata,
                ILogger logger)
            : base(contextFactory, modelMetadata, logger)
        {
        }

        public override bool ForParent(int parentId, out NotSupportedException e)
        {
            Reader = new ModelReaderService<SecurityAttributeMemberEntry>(
                this, _modelMetadata, _logger)
            {
                ParentExpression = x => x.SecurityId == parentId
            };

            Writer = new ModelWriterBatchService<SecurityAttributeMemberEntry>(
                this, _modelMetadata, _logger)
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