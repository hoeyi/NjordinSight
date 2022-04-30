﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace NjordFinance.Model
{
    [Table("AuditEvent", Schema = "FinanceApp")]
    public partial class AuditEvent
    {
        [Key]
        [Column("EventID")]
        public int EventId { get; set; }
        [Column("EventTimeUTC", TypeName = "datetime")]
        public DateTime EventTimeUtc { get; set; }
        [Column("AuditUserID")]
        public int AuditUserId { get; set; }
    }
}
