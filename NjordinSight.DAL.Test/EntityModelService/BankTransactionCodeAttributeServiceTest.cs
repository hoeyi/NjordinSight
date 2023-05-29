﻿using NjordinSight.EntityModel;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NjordinSight.Test.EntityModelService
{
    [TestClass]
    public class BankTransactionCodeAttributeServiceTest
        : ModelBatchServiceTest<BankTransactionCodeAttributeMemberEntry>
    {
        private const int _transactionCodeId = -9;
        protected override Expression<Func<BankTransactionCodeAttributeMemberEntry, bool>> ParentExpression =>
            x => x.TransactionCodeId == _transactionCodeId;

        /// <inheritdoc/>
        /// <remarks>Always passes because <see cref="ReadAsync_Returns_Single_Model"/> the 
        /// <see cref="BankTransactionCodeAttributeMemberEntry"/> entity does not have a single-integer key.</remarks>
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

            Expression<Func<BankTransactionCodeAttributeMemberEntry, bool>> expression = x =>
                x.TransactionCodeId == model.TransactionCodeId && x.AttributeMemberId == model.AttributeMemberId
                && x.EffectiveDate == model.EffectiveDate;

            var models = (await service.SelectAsync(predicate: expression, pageSize: 1)).Item1;;

            Assert.IsTrue(TestUtility.SimplePropertiesAreEqual(models.Last(), model));
        }

        [TestMethod]
        public override void UpdatePendingSave_IsDirty_Is_True()
        {
            var service = GetModelService();

            var model = service.SelectAsync().Result.FirstOrDefault();

            model.Weight *= 0.5M;

            Assert.IsTrue(service.IsDirty);
        }

        protected override IModelBatchService<BankTransactionCodeAttributeMemberEntry> GetModelService() =>
            BuildModelService<BankTransactionCodeAttributeService>().WithParent(parentId: _transactionCodeId);

    }
}
