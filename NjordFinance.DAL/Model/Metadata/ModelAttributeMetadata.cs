﻿using System.ComponentModel.DataAnnotations;
using NjordFinance.Model.Metadata;
using Ichosys.DataModel.Annotations;

namespace NjordFinance.Model
{
    /// <summary>
    /// Defines the metadata for <see cref="ModelAttribute"/>.
    /// </summary>
    [Noun(
        Plural = nameof(ModelNoun.ModelAttribute_Plural),
        PluralArticle = nameof(ModelNoun.ModelAttribute_PluralArticle),
        Singular = nameof(ModelNoun.ModelAttribute_Singular),
        SingularArticle = nameof(ModelNoun.ModelAttribute_SingularArticle),
        ResourceType = typeof(ModelNoun)
        )]
    public class ModelAttributeMetadata
    {

        [Display(
            Name = nameof(ModelDisplay.ModelAttribute_DisplayName_Name),
            Description = nameof(ModelDisplay.ModelAttribute_DisplayName_Description),
            ResourceType = typeof(ModelDisplay))]
        public string DisplayName { get; set; }
    }

    [MetadataType(typeof(ModelAttributeMetadata))]
    public partial class ModelAttribute
    {
    }
}