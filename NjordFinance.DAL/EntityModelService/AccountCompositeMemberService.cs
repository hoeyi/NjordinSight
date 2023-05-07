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
    /// The class for servicing single CRUD requests against the <see cref="AccountCompositeMember"/> 
    /// data store.
    /// </summary>
    internal class AccountCompositeMemberService : ModelBatchService<AccountCompositeMember>
    {
        /// <summary>
        /// Creates a new <see cref="AccountCompositeMemberService"/> instance.
        /// </summary>
        /// <param name="contextFactory">An <see cref="IDbContextFactory{FinanceDbContext}" /> 
        /// instance.</param>
        /// <param name="modelMetadata">An <see cref="IModelMetadataService"/> instance.</param>
        /// <param name="logger">An <see cref="ILogger"/> instance.</param>
        public AccountCompositeMemberService(
                IDbContextFactory<FinanceDbContext> contextFactory,
                IModelMetadataService modelMetadata,
                ILogger logger)
            : base(contextFactory, modelMetadata, logger)
        {
        }

        public override bool ForParent(int parentId, out Exception e)
        {
            Reader = new ModelReaderService<AccountCompositeMember>(
                Context, _modelMetadata, _logger)
            {
                ParentExpression = x => x.AccountCompositeId == parentId
            };

            Writer = new ModelWriterBatchService<AccountCompositeMember>(
                Context, _modelMetadata, _logger)
            {
                ParentExpression = x => x.AccountCompositeId == parentId,
                GetDefaultModelDelegate = () => new AccountCompositeMember()
                {
                    AccountCompositeId = parentId
                }
            };
            
            e = null;
            return true;
        }
    }
}