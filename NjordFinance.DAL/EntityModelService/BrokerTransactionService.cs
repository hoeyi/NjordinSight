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
    /// The class for servicing single CRUD requests against the <see cref="BrokerTransaction"/> 
    /// data store.
    /// </summary>
    internal class BrokerTransactionService : ModelBatchService<BrokerTransaction>
    {
        /// <summary>
        /// Creates a new <see cref="BrokerTransactionService"/> instance.
        /// </summary>
        /// <param name="contextFactory">An <see cref="IDbContextFactory{FinanceDbContext}" /> 
        /// instance.</param>
        /// <param name="modelMetadata">An <see cref="IModelMetadataService"/> instance.</param>
        /// <param name="logger">An <see cref="ILogger"/> instance.</param>
        public BrokerTransactionService(
                IDbContextFactory<FinanceDbContext> contextFactory,
                IModelMetadataService modelMetadata,
                ILogger logger)
            : base(contextFactory, modelMetadata, logger)
        {
        }

        public override bool ForParent(int parentId, out Exception e)
        {
            Reader = new ModelReaderService<BrokerTransaction>(
                Context, _modelMetadata, _logger)
            {
                ParentExpression = x => x.AccountId == parentId,
                IncludeDelegate = (queryable) => queryable.Include(x => x.TransactionCode)
            };

            Writer = new ModelWriterBatchService<BrokerTransaction>(
                Context, _modelMetadata, _logger)
            {
                ParentExpression = x => x.AccountId == parentId,
                GetDefaultModelDelegate = () => new BrokerTransaction()
                {
                    AccountId = parentId
                }
            };

            e = null;
            return true;
        }
    }
}