﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NjordFinance.Model
{
    [Table("AccountAttributeMemberEntry", Schema = "FinanceApp")]
    [Index(nameof(AccountObjectId), Name = "IX_AccountAttributeMemberEntry_AccountObjectID")]
    [Index(nameof(AttributeMemberId), Name = "IX_AccountAttributeMemberEntry_AttributeMemberID")]
    public partial class AccountAttributeMemberEntry
    {
        [Key]
        [Column("AttributeMemberID", Order = 0)]
        public int AttributeMemberId { get; set; }
        [Key]
        [Column("AccountObjectID", Order = 1)]
        public int AccountObjectId { get; set; }
        [Key]
        [Column(TypeName = "date", Order = 2)]
        public DateTime EffectiveDate { get; set; }
        [Column(TypeName = "decimal(5, 4)")]
        public decimal Weight { get; set; }

        [ForeignKey(nameof(AccountObjectId))]
        [InverseProperty("AccountAttributeMemberEntries")]
        public virtual AccountObject AccountObject { get; set; }
        [ForeignKey(nameof(AttributeMemberId))]
        [InverseProperty(nameof(ModelAttributeMember.AccountAttributeMemberEntries))]
        public virtual ModelAttributeMember AttributeMember { get; set; }
    }
}
