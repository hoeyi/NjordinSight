﻿using NjordinSight.EntityModel;
using NjordinSight.EntityModelService.Abstractions;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace NjordinSight.Test.EntityModelService
{
    [TestClass]
    [TestCategory("Integration")]
    public class InvestmentPerformanceAttributeServiceTest
        : ModelCollectionServiceTest<InvestmentPerformanceAttributeMemberEntry>
    {
        private const int _accountObjectId = -5;
        private const int _attributeMemberId = -100;

        protected override Expression<Func<InvestmentPerformanceAttributeMemberEntry, bool>>
            ParentExpression => x => x.AccountObjectId == _accountObjectId &&
                x.AttributeMemberId == _attributeMemberId;

        /// <inheritdoc/>
        /// <remarks>Always passes because <see cref="ReadAsync_Returns_Single_Model"/> the 
        /// <see cref="AccountAttributeMemberEntry"/> entity does not have a single-integer key.</remarks>
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

            Expression<Func<InvestmentPerformanceAttributeMemberEntry, bool>> expression = x =>
                x.AccountObjectId == model.AccountObjectId && x.AttributeMemberId == model.AttributeMemberId
                && x.FromDate == model.FromDate;

            var models = (await service.SelectAsync(predicate: expression, pageSize: 1)).Item1;;

            Assert.IsTrue(TestUtility.SimplePropertiesAreEqual(models.Last(), model));
        }

        [TestMethod]
        public override async Task Update_PendingSave_HasChanges_IsFalse()
        {
            var service = GetModelService();

            var model = (await service.SelectAsync()).FirstOrDefault();

            model.AverageCapital *= 1.1M;

            Assert.IsFalse(service.HasChanges);
        }

        protected override IModelCollectionService<
            InvestmentPerformanceAttributeMemberEntry, (AccountObject, ModelAttributeMember)> 
            GetModelService() => BuildModelService<
                InvestmentPerformanceAttributeService,
                (AccountObject, ModelAttributeMember)>()
                .WithParent((
                    new() { AccountObjectId = _accountObjectId, },
                    new() { AttributeMemberId = _attributeMemberId }
                ));
    }
}
