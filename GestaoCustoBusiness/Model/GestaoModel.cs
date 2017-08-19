using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestaoCustoBusiness.Model
{
    public class GestaoModel : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<UnidadeMedida> UnidadesMedida { get; set; }
        public DbSet<Receita> Receitas { get; set; }
        public DbSet<Ingrediente> Ingredientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.Entity<Receita>()
                            .HasMany(e => e.Ingredientes)
                            .WithOne(e => e.Receita)
                            .HasForeignKey(e => e.ReceitaId);

            
        }
    }
}
