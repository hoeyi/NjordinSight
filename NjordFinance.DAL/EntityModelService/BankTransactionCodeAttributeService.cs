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
    /// <see cref="BankTransactionCodeAttributeMemberEntry"/> data store.
    /// </summary>
    internal class BankTransactionCodeAttributeService 
        : ModelBatchService<BankTransactionCodeAttributeMemberEntry>
    {
        /// <summary>
        /// Creates a new <see cref="BankTransactionCodeAttributeService"/> instance.
        /// </summary>
        /// <param name="contextFactory">An <see cref="IDbContextFactory{FinanceDbContext}" /> 
        /// instance.</param>
        /// <param name="modelMetadata">An <see cref="IModelMetadataService"/> instance.</param>
        /// <param name="logger">An <see cref="ILogger"/> instance.</param>
        public BankTransactionCodeAttributeService(
                IDbContextFactory<FinanceDbContext> contextFactory,
                IModelMetadataService modelMetadata,
                ILogger logger)
            : base(contextFactory, modelMetadata, logger)
        {
        }

        public override bool ForParent(int parentId, out Exception e)
        {
            Reader = new ModelReaderService<BankTransactionCodeAttributeMemberEntry>(
                Context, _modelMetadata, _logger)
            {
                ParentExpression = x => x.TransactionCodeId == parentId
            };

            Writer = new ModelWriterBatchService<BankTransactionCodeAttributeMemberEntry>(
                Context, _modelMetadata, _logger)
            {
                ParentExpression = x => x.TransactionCodeId == parentId,
                GetDefaultModelDelegate = () => new BankTransactionCodeAttributeMemberEntry()
                {
                    TransactionCodeId = parentId
                }
            };

            e = null;
            return true;
        }
    }
}