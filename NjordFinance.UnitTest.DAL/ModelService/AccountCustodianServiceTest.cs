﻿using Ichosoft.DataModel;
using Microsoft.EntityFrameworkCore;
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
    public partial class AccountCustodianServiceTest
    {
        /// <inheritdoc/>
        [TestMethod]
        public override async Task CreateAsync_ValidModel_Returns_Single_Model()
        {
            var service = GetModelService();

            AccountCustodian custodian = await service.CreateAsync(CreateModelSuccessSample);

            using var context = CreateDbContext();

            // Verify the primary key assigned.
            var savedCustodian = context.AccountCustodians
                .First(a => a.AccountCustodianId == custodian.AccountCustodianId);

            // Check the return object attributes match the submitted object.
            Assert.IsTrue(savedCustodian.AccountCustodianId > 0);

            Assert.IsTrue(UnitTest.SimplePropertiesAreEqual(
                savedCustodian, custodian));
        }

        /// <inheritdoc/>
        [TestMethod]
        public override async Task DeleteAsync_ValidModel_Returns_True()
        {
            var service = GetModelService();

            AccountCustodian custodian = (await service.SelectWhereAysnc(a =>
                a.CustodianCode == DeleteModelSuccessSample.CustodianCode, 1))
                .First();

            var result = await service.DeleteAsync(custodian);

            using var context = CreateDbContext();

            Assert.IsTrue(result &&
                !context.AccountCustodians.Any(
                    a => a.AccountCustodianId == DeleteModelSuccessSample.AccountCustodianId));
        }

        /// <inheritdoc/>
        [TestMethod]
        public override async Task SelectWhereAsync_Returns_Model_List()
        {
            AccountCustodian expected = GetLast();

            var service = GetModelService();

            var observed = (await service.SelectWhereAysnc(
                predicate: a => a.CustodianCode == expected.CustodianCode,
                maxCount: 1))
                .First();

            Assert.IsTrue(UnitTest.SimplePropertiesAreEqual(expected, observed));
        }

        /// <inheritdoc/>
        [TestMethod]
        public override async Task UpdateAsync_ValidModel_Returns_True()
        {
            var service = GetModelService();

            AccountCustodian custodian = (await service.SelectWhereAysnc(
                predicate: x =>
                    x.CustodianCode == UpdateModelSuccessSample.CustodianCode,
                maxCount: 1))
                .First();

            custodian.DisplayName = "Test custodian UPDATED";

            var result = await service.UpdateAsync(custodian);

            using var context = CreateDbContext();

            var savedCustodian = context.AccountCustodians.FirstOrDefault(a =>
                a.AccountCustodianId == custodian.AccountCustodianId);

            Assert.IsTrue(UnitTest.SimplePropertiesAreEqual(
                savedCustodian, custodian));
        }
    }

    /// <inheritdoc/>
    public partial class AccountCustodianServiceTest : ModelServiceBaseTest<AccountCustodian>
    {
        /// <inheritdoc/>
        protected override AccountCustodian CreateModelSuccessSample => new()
        {
            CustodianCode = "TESTCREATE",
            DisplayName = "Test custodian create."
        };

        /// <inheritdoc/>
        protected override AccountCustodian DeleteModelSuccessSample => new()
        {
            CustodianCode = "TESTDELPASS",
            DisplayName = "Test custodian delete."
        };

        /// <inheritdoc/>
        protected override AccountCustodian DeleteModelFailSample => new()
        {
            AccountCustodianId = -1000,
            CustodianCode = "TESTDELFAIL",
            DisplayName = "Test custodian delete FAIL."
        };

        /// <inheritdoc/>
        protected override AccountCustodian UpdateModelSuccessSample => new()
        {
            CustodianCode = "TESTUPDATE",
            DisplayName = "Test custodian update."
        };

        /// <inheritdoc/>
        [TestCleanup]
        public override void CleanUp()
        {
            Logger.LogInformation("Cleaning up {test}.", nameof(AccountCustodianServiceTest));

            using var context = CreateDbContext();

            int recordsDeleted = context.Database.ExecuteSqlRaw(
                "DELETE FROM NjordDbTest.FinanceApp.AccountCustodian WHERE AccountCustodianID > 0;");

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
                    DeleteModelSuccessSample.CustodianCode,
                    DeleteModelSuccessSample.DisplayName
                },
                new
                {
                    DeleteModelFailSample.CustodianCode,
                    DeleteModelFailSample.DisplayName
                }
            });

            SeedModelsIfNotExists(
                including: null,
                (
                    DeleteModelSuccessSample, 
                    x => x.CustodianCode == DeleteModelSuccessSample.CustodianCode),
                (
                    UpdateModelSuccessSample, 
                    x => x.CustodianCode == UpdateModelSuccessSample.CustodianCode));
        }

        /// <inheritdoc/>
        protected override int GetKey(AccountCustodian model) => model.AccountCustodianId;

        /// <inheritdoc/>
        protected override IModelService<AccountCustodian> GetModelService() =>
            BuildModelService<AccountCustodianService>();
    }
}
