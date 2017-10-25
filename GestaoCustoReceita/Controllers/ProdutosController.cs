using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestaoCustoBusiness.Model;
using GestaoCustoReceita.Data;
using GestaoCustoReceita.Models;

namespace GestaoCustoReceita.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly ApplicationDbContext _context;

        [HttpPost]
        public JsonResult Update([Bind("Id,Descricao,Qtd,Preco")] Produto produto)
        {
            //_context.Update(produto);
            return Json(produto);
        }

        public JsonResult GetbyId(int id)  
        {   

            var produto = _context.Produtos
                .SingleOrDefault(m => m.Id == id);
             
            return Json(produto);    
        } 


        public ProdutosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Produtos
        public async Task<IActionResult> Index()
        {
            var viewProduto = new ViewProduto();
            viewProduto.ProdutosList = await _context.Produtos.OrderBy(p => p.Descricao).ToListAsync();
            viewProduto.Produto = new Produto();
            return View(viewProduto);
        }

        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .SingleOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Produtos/Create
        public IActionResult Create()
        {
            if (!string.IsNullOrEmpty(Request.Query["receita"]))
            {
                ViewBag.ReceitaId = int.Parse(Request.Query["receita"]);
            }
            else
            {
                ViewBag.ReceitaId = 0;
            }
            return View();
        }
 
        // POST: Produtos/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao,Qtd,Preco")] Produto produto, int receitaId)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produto);
                await _context.SaveChangesAsync();
                if (receitaId == 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Create", "Ingredientes", new { id = receitaId });
                }
            }
            var viewProduto = new ViewProduto();
            viewProduto.ProdutosList = await _context.Produtos.OrderBy(p => p.Descricao).ToListAsync();
            viewProduto.Produto = produto;
            return View("Index",  viewProduto);
        }

        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(Request.Query["receita"]))
            {
                ViewBag.ReceitaId = int.Parse(Request.Query["receita"]);
            }
            else
            {
                ViewBag.ReceitaId = 0;
            }
            var produto = await _context.Produtos.SingleOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao,Qtd,Preco")] Produto produto, int receitaId)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                if (receitaId == 0)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return RedirectToAction("Index", "Ingredientes", new { receita = receitaId });
                } 
            }
            return View(produto);
        }

        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .SingleOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.Produtos.SingleOrDefaultAsync(m => m.Id == id);
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }
    }
}
