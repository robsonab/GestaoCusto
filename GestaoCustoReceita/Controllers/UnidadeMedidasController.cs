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
    public class UnidadeMedidasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public UnidadeMedidasController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: UnidadeMedidas
        public async Task<IActionResult> Index()
        {
            return View(await _context.UnidadeMedida.ToListAsync());
        }

        // GET: UnidadeMedidas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidadeMedida = await _context.UnidadeMedida
                .SingleOrDefaultAsync(m => m.Id == id);
            if (unidadeMedida == null)
            {
                return NotFound();
            }

            return View(unidadeMedida);
        }

        // GET: UnidadeMedidas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UnidadeMedidas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Descricao")] UnidadeMedida unidadeMedida)
        {
            if (ModelState.IsValid)
            {
                _context.Add(unidadeMedida);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(unidadeMedida);
        }

        // GET: UnidadeMedidas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidadeMedida = await _context.UnidadeMedida.SingleOrDefaultAsync(m => m.Id == id);
            if (unidadeMedida == null)
            {
                return NotFound();
            }
            return View(unidadeMedida);
        }

        // POST: UnidadeMedidas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Descricao")] UnidadeMedida unidadeMedida)
        {
            if (id != unidadeMedida.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(unidadeMedida);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UnidadeMedidaExists(unidadeMedida.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(unidadeMedida);
        }

        // GET: UnidadeMedidas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var unidadeMedida = await _context.UnidadeMedida
                .SingleOrDefaultAsync(m => m.Id == id);
            if (unidadeMedida == null)
            {
                return NotFound();
            }

            return View(unidadeMedida);
        }

        // POST: UnidadeMedidas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var unidadeMedida = await _context.UnidadeMedida.SingleOrDefaultAsync(m => m.Id == id);
            _context.UnidadeMedida.Remove(unidadeMedida);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool UnidadeMedidaExists(int id)
        {
            return _context.UnidadeMedida.Any(e => e.Id == id);
        }
    }
}
