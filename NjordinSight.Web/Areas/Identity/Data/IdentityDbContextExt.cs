﻿using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using NjordinSight.Web.Areas.Identity.Data;

namespace NjordinSight.Web.Data
{
    /// <summary>
    /// <see cref="DbContext"/> child for working with <see cref="WebAppUser"/> records.
    /// </summary>
    public partial class IdentityDbContext : IdentityDbContext<WebAppUser>
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseInMemoryDatabase("NjordIdentity");
            }
        }
    }
}
