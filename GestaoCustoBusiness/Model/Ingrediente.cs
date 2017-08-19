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
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }
        public decimal Quantidade { get; set; }
        public decimal PrecoTotal { get; set; }
        public int ReceitaId { get; set; }
        public Receita Receita { get; set; }
    }
}
