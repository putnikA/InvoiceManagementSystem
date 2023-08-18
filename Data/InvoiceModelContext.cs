using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using InvoiceManagementWebApp.Models;

namespace InvoiceManagementWebApp.Data
{
    public class InvoiceModelContext : DbContext
    {
        public InvoiceModelContext (DbContextOptions<InvoiceModelContext> options)
            : base(options)
        {
        }

        public DbSet<InvoiceManagementWebApp.Models.InvoiceModel> InvoiceModel { get; set; }

        public DbSet<InvoiceManagementWebApp.Models.SupplierModel> SupplierModel { get; set; }
    }
}
