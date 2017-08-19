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

        public string Descricao { get; set; }

        public IEnumerable<Ingrediente> Ingredientes { get; set; }
    }
}
