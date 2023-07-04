﻿using AutoMapper;
using NjordinSight.DataTransfer.Profiles;
using System.Threading.Tasks;

namespace NjordinSight.Test.DataTransfer.Mapping
{
    [TestClass]
    [TestCategory("Unit")]
    public class SecurityProfileTest : IProfileTest, IProfileWithDependencyTest
    {
        [TestMethod]
        public void Configuration_IsValid()
        {
            // Arrange
            var config = new MapperConfiguration(x =>
            {
                x.AddProfile<ModelAttributeProfile>();
                x.AddProfile<SecurityProfile>();
            });

            // Act

            // Assert
            config.AssertConfigurationIsValid();
        }

        public void Configuration_WithoutProfileDependencies_IsInvalid()
        {
            throw new System.NotImplementedException();
        }

        public Task Dto_MapFrom_Entity_MappedProperties_AreEqual()
        {
            throw new System.NotImplementedException();
        }

        public Task Entity_MapFrom_Dto_MappedProperties_AreEqual()
        {
            throw new System.NotImplementedException();
        }
    }
}
