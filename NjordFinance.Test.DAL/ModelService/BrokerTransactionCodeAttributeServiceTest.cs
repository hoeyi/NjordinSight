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
    public class BrokerTransactionCodeAttributeServiceTest
        : ModelBatchServiceTest<BrokerTransactionCodeAttributeMemberEntry>
    {
        private const int _transactionCodeId = -20;

        protected override Expression<Func<BrokerTransactionCodeAttributeMemberEntry, bool>> ParentExpression =>
            x => x.TransactionCodeId == _transactionCodeId;

        [TestMethod]
        public override Task ReadAsync_Returns_Single_Model()
        {
            return Task.CompletedTask;
        }

        [TestMethod]
        public override async Task SelectWhereAsync_Returns_Model_ExpectedCollection()
        {
            var model = GetLast(ParentExpression);

            var service = GetModelService();

            Expression<Func<BrokerTransactionCodeAttributeMemberEntry, bool>> expression = x =>
                x.TransactionCodeId == model.TransactionCodeId && x.AttributeMemberId == model.AttributeMemberId
                && x.EffectiveDate == model.EffectiveDate;

            var models = await service.SelectWhereAysnc(predicate: expression, maxCount: 1);

            Assert.IsTrue(TestUtility.SimplePropertiesAreEqual(models.Last(), model));
        }

        [TestMethod]
        public override void UpdatePendingSave_IsDirty_Is_True()
        {
            var service = GetModelService();

            var model = service.SelectAllAsync().Result.FirstOrDefault();

            model.Weight *= 0.5M;

            Assert.IsTrue(service.IsDirty);
        }

        protected override IModelBatchService<BrokerTransactionCodeAttributeMemberEntry> GetModelService() =>
            BuildModelService<BrokerTransactionCodeAttributeService>().WithParent(parentId: _transactionCodeId);
    }
}
