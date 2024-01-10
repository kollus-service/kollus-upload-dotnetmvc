using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KollusSampleMVC.Models;

namespace KollusSampleMVC.Data
{
    public class KollusSampleMVCContext : DbContext
    {
        public KollusSampleMVCContext (DbContextOptions<KollusSampleMVCContext> options)
            : base(options)
        {
        }

        public DbSet<KollusSampleMVC.Models.Content> Content { get; set; } = default!;
    }
}
