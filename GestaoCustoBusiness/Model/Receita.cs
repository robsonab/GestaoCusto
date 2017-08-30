using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GestaoCustoBusiness.Model
{
    [Table("Receitas")]
    public class Receita
    {
        
        [Key]
        public int Id { get; set; }

        [Display(Name = "Receita")]
        public string Descricao { get; set; }

        public IEnumerable<Ingrediente> Ingredientes { get; set; }

        [NotMapped]
        [Display(Name = "Custo")]
        public decimal CustoTotal { get; set; }

        [Display(Name = "Porções")]
        public short Qtd { get; set; }

        [NotMapped]
        [Display(Name ="Custo Unit.")]
        public decimal CustoUnitario { get; set; }
    }
}
