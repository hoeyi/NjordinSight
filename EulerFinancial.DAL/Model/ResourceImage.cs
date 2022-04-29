﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace EulerFinancial.Model
{
    [Table("ResourceImage", Schema = "EulerApp")]
    public partial class ResourceImage
    {
        [Key]
        [Column("ImageID")]
        public int ImageId { get; set; }
        [StringLength(128)]
        [Unicode(false)]
        public string ImageDescription { get; set; } = null!;
        public byte[] ImageBinary { get; set; } = null!;
        [StringLength(4)]
        [Unicode(false)]
        public string FileExtension { get; set; } = null!;
    }
}
