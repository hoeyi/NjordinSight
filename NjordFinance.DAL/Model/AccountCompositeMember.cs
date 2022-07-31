﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using NjordFinance.Model.Metadata;

namespace NjordFinance.Model
{
    [Table("AccountCompositeMember", Schema = "FinanceApp")]
    [Index(nameof(AccountCompositeId), Name = "IX_AccountCompositeMember_AccountCompositeID")]
    [Index(nameof(AccountId), Name = "IX_AccountCompositeMember_AccountID")]
    public partial class AccountCompositeMember
    {
        [Key]
        [Column("AccountCompositeID", Order = 0)]
        public int AccountCompositeId { get; set; }
        [Key]
        [Column("AccountID", Order = 1)]
        public int AccountId { get; set; }
        [Key]
        [Column(TypeName = "date", Order = 2)]
        public DateTime EntryDate { get; set; }
        [Column(TypeName = "date")]
        public DateTime? ExitDate { get; set; }
        public int DisplayOrder { get; set; }
        [StringLength(72,
            ErrorMessageResourceName = nameof(ModelValidation.StringLengthAttribute_ValidationError),
            ErrorMessageResourceType = typeof(ModelValidation))]
        public string Comment { get; set; }

        [ForeignKey(nameof(AccountId))]
        [InverseProperty("AccountCompositeMembers")]
        public virtual Account Account { get; set; }
        [ForeignKey(nameof(AccountCompositeId))]
        [InverseProperty("AccountCompositeMembers")]
        public virtual AccountComposite AccountComposite { get; set; }
    }
}
