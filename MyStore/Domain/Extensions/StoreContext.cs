using Microsoft.EntityFrameworkCore;
using MyStore.Domain.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyStore.Domain.Entities
{
    public partial class StoreContext
    {
        public virtual DbSet<CustomerContact> CustomerContacts { get; set; }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomerContact>().HasNoKey();
        }
    }

}
