﻿using Ichosys.DataModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using NjordinSight.EntityModel.Context;
using NjordinSight.EntityModel;
using NjordinSight.EntityModelService.Abstractions;
using System;

namespace NjordinSight.EntityModelService
{
    /// <summary>
    /// The class for servicing batch CRUD requests against the <see cref="BankTransactionCode"/> 
    /// data store.
    /// </summary>
    internal class BankTransactionCodeBatchService : ModelBatchService<BankTransactionCode>
    {
        /// <summary>
        /// Creates a new <see cref="BankTransactionCodeBatchService"/> instance.
        /// </summary>
        /// <param name="contextFactory">An <see cref="IDbContextFactory{FinanceDbContext}" /> 
        /// instance.</param>
        /// <param name="modelMetadata">An <see cref="IModelMetadataService"/> instance.</param>
        /// <param name="logger">An <see cref="ILogger"/> instance.</param>
        public BankTransactionCodeBatchService(
            IDbContextFactory<FinanceDbContext> contextFactory,
            IModelMetadataService modelMetadata,
            ILogger logger)
                : base(contextFactory, modelMetadata, logger)
        {
            ForParent(parentId: default, out _);
        }

        public override bool ForParent(int parentId, out Exception e)
        {
            Reader = new ModelReaderService<BankTransactionCode>(
                Context, _modelMetadata, _logger)
            {
                ParentExpression = x => true
            };

            Writer = new ModelWriterBatchService<BankTransactionCode>(
                Context, _modelMetadata, _logger)
            {
                ParentExpression = x => true,
                GetDefaultModelDelegate = () => new BankTransactionCode()
            };

            e = null;
            return true;
        }
    }
}