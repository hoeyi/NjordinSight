﻿using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NjordinSight.DataTransfer.Profiles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NjordinSight.Test.DataTransfer.Mapping
{
    [TestClass]
    [TestCategory("Unit")]
    public class CountryProfileTest : ProfileBaseTest
    {
        public CountryProfileTest() 
            : base(configuration: new(
                x =>
                {
                    x.AddProfile<ModelAttributeProfile>();
                    x.AddProfile<CountryProfile>();
                }))
        {
        }

        [TestMethod]
        public void Configuration_WithProfile_IsValid()
        {
            // Assert
            Configuration.AssertConfigurationIsValid();
        }
    }
}
