using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestaoCustoBusiness.Model;
using GestaoCustoReceita.Data;

namespace GestaoCustoReceita.Controllers
{
    public class IngredientesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public IngredientesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Ingredientes        
        public async Task<IActionResult> Index(int? receita)
        {
            var ingredientes = await _context.Ingredientes
                                            .Where(i => i.ReceitaId == receita)
                                            .Include(i => i.Produto)
                                            .Include(i => i.Receita)
                                            .OrderBy(i => i.Produto.Descricao)
                                            .ToListAsync();

            AtualizaPreco(ingredientes);
            ViewBag.ReceitaId = receita;

            if (ingredientes.Count > 0)
            {
                ingredientes.First().Receita.CustoUnitario = ingredientes.Sum(i => i.Custo) / ingredientes.First().Receita.Qtd;
            }            

            ViewData["ProdutoId"] = new SelectList(_context.Produtos.OrderBy(p => p.Descricao), "Id", "Descricao");

            if (ingredientes.Count == 0)
            {
                ViewBag.Receita = _context.Receitas.Where(r => r.Id == receita).FirstOrDefault().Descricao;
            }
            else
            {
                ViewBag.Receita = ingredientes.FirstOrDefault().Receita.Descricao;
            }
            return View(ingredientes);
        }

        public void AtualizaPreco(List<Ingrediente> ingredientes)
        {
            foreach (var item in ingredientes)
            {
                item.Custo = item.Quantidade * (item.Produto.Preco / item.Produto.Qtd);                
            }
        }
        // GET: Ingredientes/Create
        public IActionResult Create(int id)
        {
            ViewData["ProdutoId"] = new SelectList(_context.Produtos.OrderBy(p => p.Descricao), "Id", "Descricao");
            ViewData["ReceitaId"] = id;
            ViewBag.Receita = _context.Receitas.Where(r => r.Id == id).FirstOrDefault().Descricao;

            return View();
        }

        // POST: Ingredientes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id, ProdutoId,Quantidade,ReceitaId")] Ingrediente ingrediente)
        {
            ingrediente.Id = 0;
            if (ModelState.IsValid)
            {
                _context.Add(ingrediente);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new { receita = ingrediente.ReceitaId });
            }
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "Id", "Id", ingrediente.ProdutoId);
            ViewData["ReceitaId"] = new SelectList(_context.Receitas, "Id", "Id", ingrediente.ReceitaId);
            return View(ingrediente);
        }


        // GET: Ingredientes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingrediente = await _context.Ingredientes
                .Include(i => i.Produto)
                .Include(i => i.Receita)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (ingrediente == null)
            {
                return NotFound();
            }

            return View(ingrediente);
        }


        // GET: Ingredientes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingrediente = await _context.Ingredientes.SingleOrDefaultAsync(m => m.Id == id);
            if (ingrediente == null)
            {
                return NotFound();
            }

            ViewData["ProdutoId"] = new SelectList(_context.Produtos.OrderBy(p => p.Descricao), "Id", "Descricao");
            ViewData["ReceitaId"] = new SelectList(_context.Receitas, "Id", "Descricao", ingrediente.ReceitaId);
            return View(ingrediente);
        }

        // POST: Ingredientes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProdutoId,Quantidade,ReceitaId")] Ingrediente ingrediente)
        {
            if (id != ingrediente.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ingrediente);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!IngredienteExists(ingrediente.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index", new { receita = ingrediente.ReceitaId });
            }
            ViewData["ProdutoId"] = new SelectList(_context.Produtos, "Id", "Id", ingrediente.ProdutoId);
            ViewData["ReceitaId"] = new SelectList(_context.Receitas, "Id", "Id", ingrediente.ReceitaId);
            return View(ingrediente);
        }

        // GET: Ingredientes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ingrediente = await _context.Ingredientes
                .Include(i => i.Produto)
                .Include(i => i.Receita)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (ingrediente == null)
            {
                return NotFound();
            }

            return View(ingrediente);
        }

        // POST: Ingredientes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ingrediente = await _context.Ingredientes.SingleOrDefaultAsync(m => m.Id == id);
            _context.Ingredientes.Remove(ingrediente);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", new { receita = ingrediente.ReceitaId });
        }

        private bool IngredienteExists(int id)
        {
            return _context.Ingredientes.Any(e => e.Id == id);
        }
    }
}
