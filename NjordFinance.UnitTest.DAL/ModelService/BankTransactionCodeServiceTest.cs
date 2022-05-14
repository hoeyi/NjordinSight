﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NjordFinance.Exceptions;
using NjordFinance.Model;
using NjordFinance.ModelService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NjordFinance.UnitTest.ModelService
{
    /// <inheritdoc/>
    [TestClass]
    public partial class BankTransactionCodeServiceTest
    {
        /// <inheritdoc/>
        [TestMethod]
        public override async Task DeleteAsync_ValidModel_Returns_True()
        {
            var service = GetModelService();

            BankTransactionCode deleted = (await service.SelectWhereAysnc(x =>
                x.TransactionCode == DeleteModelSuccessSample.TransactionCode, 1))
                .First();

            var result = await service.DeleteAsync(deleted);

            using var context = CreateDbContext();

            Assert.IsTrue(result && !context.BankTransactionCodes
                .Any(x => x.TransactionCodeId == deleted.TransactionCodeId));
        }

        /// <inheritdoc/>
        [TestMethod]
        public override async Task UpdateAsync_ValidModel_Returns_True()
        {
            var service = GetModelService();

            BankTransactionCode original = (await service.SelectWhereAysnc(
                predicate: a => a.TransactionCode == UpdateModelSuccessSample.TransactionCode,
                maxCount: 1))
                .First();

            original.DisplayName = "Test update pass EDIT";

            var result = await service.UpdateAsync(original);

            using var context = CreateDbContext();

            var updated = context.BankTransactionCodes
                .FirstOrDefault(a => a.TransactionCodeId == original.TransactionCodeId);

            Assert.IsTrue(UnitTest.SimplePropertiesAreEqual(updated, original));
        }
    }

    public partial class BankTransactionCodeServiceTest 
        : ModelServiceBaseTest<BankTransactionCode>
    {
        /// <inheritdoc/>
        protected override BankTransactionCode CreateModelSuccessSample => new()
        {
            TransactionCode = "CREATE",
            DisplayName = "Test create pass."
        };

        /// <inheritdoc/>
        protected override BankTransactionCode DeleteModelSuccessSample => new()
        {
            TransactionCode = "DELPASS",
            DisplayName = "Test delete pass."
        };

        /// <inheritdoc/>
        protected override BankTransactionCode DeleteModelFailSample => new()
        {
            TransactionCodeId = -1000,
            TransactionCode = "DELFAIL",
            DisplayName = "Test delete fail."
        };

        /// <inheritdoc/>
        protected override BankTransactionCode UpdateModelSuccessSample => new()
        {
            TransactionCode = "UPDATE",
            DisplayName = "Test update pass."
        };

        /// <inheritdoc/>
        [TestCleanup]
        public override void CleanUp()
        {
            Logger.LogInformation("Cleaning up {test}.", nameof(BankTransactionCodeServiceTest));

            using var context = CreateDbContext();

            int recordsDeleted = context.Database.ExecuteSqlRaw(
                "DELETE FROM NjordDbTest.FinanceApp.BankTransactionCode WHERE TransactionCodeID > 0;");

            Logger.LogInformation("Deleted {count} records.", recordsDeleted);
        }

        /// <inheritdoc/>
        [TestInitialize]
        public override void Initialize()
        {
            Logger.LogInformation("Seeding with test data {list}.", (object)new[]
            {
                new
                {
                    DeleteModelSuccessSample.TransactionCode,
                    DeleteModelSuccessSample.DisplayName
                },
                new
                {
                    DeleteModelFailSample.TransactionCode,
                    DeleteModelFailSample.DisplayName
                }
            });

            SeedModelsIfNotExists(
                including: null,
                (
                    DeleteModelSuccessSample,
                    x => x.TransactionCode == DeleteModelSuccessSample.TransactionCode
                ),
                (
                    UpdateModelSuccessSample,
                    x => x.TransactionCode == UpdateModelSuccessSample.TransactionCode
                ));
        }

        /// <inheritdoc/>
        protected override int GetKey(BankTransactionCode model) => model.TransactionCodeId;

        /// <inheritdoc/>
        protected override IModelService<BankTransactionCode> GetModelService() =>
            BuildModelService<BankTransactionCodeService>();

    }
}
