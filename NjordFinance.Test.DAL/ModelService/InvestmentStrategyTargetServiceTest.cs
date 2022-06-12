﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using NjordFinance.Model;
using NjordFinance.ModelService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NjordFinance.Test.ModelService
{
    [TestClass]
    public class InvestmentStrategyTargetServiceTest
        : ModelBatchServiceTest<InvestmentStrategyTarget>
    {
        private const int _investmentStrategyId = -3;
        protected override Expression<Func<InvestmentStrategyTarget, bool>> ParentExpression =>
            x => x.InvestmentStrategyId == _investmentStrategyId;

        /// <inheritdoc/>
        /// <remarks>Always passes because <see cref="UpdatePendingSave_IsDirty_Is_True"/> the 
        /// <see cref="MarketIndexPrice"/> entity does not have updatable members.</remarks>
        [TestMethod]
        public override void UpdatePendingSave_IsDirty_Is_True()
        {
            return;
        }

        protected override IModelBatchService<InvestmentStrategyTarget> GetModelService() =>
            BuildModelService<InvestmentStrategyTargetService>().WithParent(parentId: _investmentStrategyId);

    }
}
