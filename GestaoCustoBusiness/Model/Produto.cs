
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestaoCustoBusiness
{
    namespace Model
    {

        [Table("Produtos")]
        public class Produto
        {
            [Key]
            public int Id { get; set; }

            [Display(Name = "Produto")]
            [Required(ErrorMessage ="Informe o nome do produto")]
            [StringLength(100,ErrorMessage = "Tamanho máximo de 100 caracteres")]
            public string Descricao { get; set; }

            [Display(Prompt = "Digite a quantidade", Name ="Quantidade") ]
            [Required(ErrorMessage ="Informe a quantidade")]
            [Range(1, int.MaxValue, ErrorMessage = "Quantidade inválida")]
            public int Qtd { get; set; }

            [Display(Name ="Preço")]
            [Required(ErrorMessage ="Informe o preço")]
            [Range(0.01, double.MaxValue, ErrorMessage = "Preço inválido")]
            public decimal Preco { get; set; }         
            
        }
    }
}