using GestaoCustoBusiness.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestaoCustoReceita.Models
{
    public class ViewProduto
    {
        public List<Produto> ProdutosList { get; set; }
        public Produto Produto { get; set; }
    }
}
