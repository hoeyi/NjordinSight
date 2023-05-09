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
    /// The class for servicing single CRUD requests against the <see cref="MODEL"/> 
    /// data store.
    /// </summary>
    internal class AccountAttributeMemberService : ModelBatchService<AccountAttributeMemberEntry>
    {
        /// <summary>
        /// Creates a new <see cref="AccountAttributeMemberService"/> instance.
        /// </summary>
        /// <param name="contextFactory">An <see cref="IDbContextFactory{FinanceDbContext}" /> 
        /// instance.</param>
        /// <param name="modelMetadata">An <see cref="IModelMetadataService"/> instance.</param>
        /// <param name="logger">An <see cref="ILogger"/> instance.</param>
        public AccountAttributeMemberService(
                IDbContextFactory<FinanceDbContext> contextFactory,
                IModelMetadataService modelMetadata,
                ILogger logger)
            : base(contextFactory, modelMetadata, logger)
        {
            Reader = new ModelReaderService<AccountAttributeMemberEntry>(
                contextFactory, modelMetadata, logger);
            Writer = new ModelWriterBatchService<AccountAttributeMemberEntry>(
                contextFactory, modelMetadata, logger);
        }

        public override bool ForParent(int parentId, out Exception e)
        {
            Reader = new ModelReaderService<AccountAttributeMemberEntry>(
                Context, _modelMetadata, _logger)
            {
                ParentExpression = x => x.AccountObjectId == parentId
            };

            Writer = new ModelWriterBatchService<AccountAttributeMemberEntry>(
                Context, _modelMetadata, _logger)
            {
                ParentExpression = x => x.AccountObjectId == parentId,
                GetDefaultModelDelegate = () => new AccountAttributeMemberEntry()
                {
                    AccountObjectId = parentId
                }
            };

            e = null;
            return true;
        }
    }
}