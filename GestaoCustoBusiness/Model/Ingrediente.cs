using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GestaoCustoBusiness.Model
{
    [Table("Ingredientes")]
    public class Ingrediente
    {
        [Key]
        public int Id { get; set; }

        [Display(Name ="Ingrediente")]
        public int ProdutoId { get; set; }

        public Produto Produto { get; set; }
        public decimal Quantidade { get; set; }
        [NotMapped]
        public decimal Custo { get; set; }
        [Display(Name ="Receita")]
        public int ReceitaId { get; set; }
        public Receita Receita { get; set; }
    }
}
