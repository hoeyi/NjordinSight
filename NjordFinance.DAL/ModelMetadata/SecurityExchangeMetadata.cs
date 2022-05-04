﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NjordFinance.ModelMetadata.Resources;
using System.ComponentModel.DataAnnotations;
using Ichosoft.DataModel.Annotations;

namespace NjordFinance.Model
{
    /// <summary>
    /// Defines the metadata for <see cref="SecurityExchange"/>.
    /// </summary>
    [Noun(
        Plural = nameof(ModelNoun.SecurityExchange_Plural),
        PluralArticle = nameof(ModelNoun.SecurityExchange_PluralArticle),
        Singular = nameof(ModelNoun.SecurityExchange_Singular),
        SingularArticle = nameof(ModelNoun.SecurityExchange_SingularArticle),
        ResourceType = typeof(ModelNoun)
        )]
    public class SecurityExchangeMetadata
    {

        [Display(
            Name = nameof(ModelDisplay.SecurityExchange_ExchangeCode_Name),
            Description = nameof(ModelDisplay.SecurityExchange_ExchangeCode_Description),
            ResourceType = typeof(ModelDisplay))]
        public string ExchangeCode { get; set; }


        [Display(
            Name = nameof(ModelDisplay.SecurityExchange_ExchangeDescription_Name),
            Description = nameof(ModelDisplay.SecurityExchange_ExchangeDescription_Description),
            ResourceType = typeof(ModelDisplay))]
        public string ExchangeDescription { get; set; }
    }

    [MetadataType(typeof(SecurityExchangeMetadata))]
    public partial class SecurityExchange
    {
    }
}
