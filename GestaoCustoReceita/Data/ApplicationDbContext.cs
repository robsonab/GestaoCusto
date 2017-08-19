using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using GestaoCustoReceita.Models;
using GestaoCustoBusiness.Model;

namespace GestaoCustoReceita.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }

        public DbSet<GestaoCustoBusiness.Model.UnidadeMedida> UnidadeMedida { get; set; }

        public DbSet<GestaoCustoBusiness.Model.Produto> Produtos { get; set; }

        public DbSet<GestaoCustoBusiness.Model.Receita> Receitas { get; set; }

        public DbSet<GestaoCustoBusiness.Model.Ingrediente> Ingredientes { get; set; }
    }
}
