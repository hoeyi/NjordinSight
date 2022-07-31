﻿using NjordFinance.Model.Metadata;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NjordFinance.Model
{
    [Table("ReportStyleSheet", Schema = "FinanceApp")]
    public partial class ReportStyleSheet
    {
        [Key]
        [Column("StyleSheetID")]
        public int StyleSheetId { get; set; }
        [Required(
            ErrorMessageResourceName = nameof(ModelValidation.RequiredAttribute_ValidationError),
            ErrorMessageResourceType = typeof(ModelValidation))]
        [StringLength(32,
            ErrorMessageResourceName = nameof(ModelValidation.StringLengthAttribute_ValidationError),
            ErrorMessageResourceType = typeof(ModelValidation))]
        public string StyleSheetCode { get; set; }
        [Required(
            ErrorMessageResourceName = nameof(ModelValidation.RequiredAttribute_ValidationError),
            ErrorMessageResourceType = typeof(ModelValidation))]
        [StringLength(128,
            ErrorMessageResourceName = nameof(ModelValidation.StringLengthAttribute_ValidationError),
            ErrorMessageResourceType = typeof(ModelValidation))]
        public string StyleSheetDescription { get; set; }
        [Required(
            ErrorMessageResourceName = nameof(ModelValidation.RequiredAttribute_ValidationError),
            ErrorMessageResourceType = typeof(ModelValidation))]
        [Column(TypeName = "xml")]
        public string XmlDefinition { get; set; }
    }
}
