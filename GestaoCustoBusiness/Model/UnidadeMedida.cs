using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace GestaoCustoBusiness.Model
{
    [Table("UnidadeMedida")]
    public class UnidadeMedida
    {
        [Key]
        public int Id { get; set; }
        public string Descricao { get; set; }
         
    }
}
