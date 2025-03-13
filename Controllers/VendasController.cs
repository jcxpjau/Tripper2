using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tripper.Models;

namespace Tripper.Controllers
{
    public class VendasController : Controller
    {
        private readonly Contexto _context;

        public VendasController(Contexto context)
        {
            _context = context;
        }

        // GET: Vendas
        public async Task<IActionResult> Index()
        {
            var contexto = _context.Vendas.Include(v => v.Estabelecimentos).Include(v => v.Produtos).Include(v => v.Vendedores);
            return View(await contexto.ToListAsync());
        }

        // GET: Vendas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendas = await _context.Vendas
                .Include(v => v.Estabelecimentos)
                .Include(v => v.Produtos)
                .Include(v => v.Vendedores)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendas == null)
            {
                return NotFound();
            }

            return View(vendas);
        }

        // GET: Vendas/Create
        public IActionResult Create()
        {
            ViewData["EstabelecimentosId"] = new SelectList(_context.Estabelecimentos, "Id", "RazaoSocial");
            ViewData["ProdutosId"] = new SelectList(_context.Produtos, "Id", "Descricao");
            ViewData["VendedoresId"] = new SelectList(_context.Vendedores, "Id", "Nome");
            return View();
        }

        // POST: Vendas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProdutosId,EstabelecimentosId,VendedoresId,Data")] Vendas vendas)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vendas);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstabelecimentosId"] = new SelectList(_context.Estabelecimentos, "Id", "RazaoSocial", vendas.EstabelecimentosId);
            ViewData["ProdutosId"] = new SelectList(_context.Produtos, "Id", "Descricao", vendas.ProdutosId);
            ViewData["VendedoresId"] = new SelectList(_context.Vendedores, "Id", "Nome", vendas.VendedoresId);
            return View(vendas);
        }

        // GET: Vendas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendas = await _context.Vendas.FindAsync(id);
            if (vendas == null)
            {
                return NotFound();
            }
            ViewData["EstabelecimentosId"] = new SelectList(_context.Estabelecimentos, "Id", "RazaoSocial", vendas.EstabelecimentosId);
            ViewData["ProdutosId"] = new SelectList(_context.Produtos, "Id", "Descricao", vendas.ProdutosId);
            ViewData["VendedoresId"] = new SelectList(_context.Vendedores, "Id", "Nome", vendas.VendedoresId);
            return View(vendas);
        }

        // POST: Vendas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProdutosId,EstabelecimentosId,VendedoresId,Data")] Vendas vendas)
        {
            if (id != vendas.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vendas);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendasExists(vendas.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["EstabelecimentosId"] = new SelectList(_context.Estabelecimentos, "Id", "RazaoSocial", vendas.EstabelecimentosId);
            ViewData["ProdutosId"] = new SelectList(_context.Produtos, "Id", "Descricao", vendas.ProdutosId);
            ViewData["VendedoresId"] = new SelectList(_context.Vendedores, "Id", "Nome", vendas.VendedoresId);
            return View(vendas);
        }

        // GET: Vendas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendas = await _context.Vendas
                .Include(v => v.Estabelecimentos)
                .Include(v => v.Produtos)
                .Include(v => v.Vendedores)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vendas == null)
            {
                return NotFound();
            }

            return View(vendas);
        }

        // POST: Vendas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendas = await _context.Vendas.FindAsync(id);
            if (vendas != null)
            {
                _context.Vendas.Remove(vendas);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VendasExists(int id)
        {
            return _context.Vendas.Any(e => e.Id == id);
        }
    }
}
