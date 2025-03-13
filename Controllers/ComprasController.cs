using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.EntityFrameworkCore;
using Tripper.Models;

namespace Tripper.Controllers
{
    public class ComprasController : Controller
    {
        private readonly Contexto _context;

        public ComprasController(Contexto context)
        {
            _context = context;
        }

        // GET: Compras
        public async Task<IActionResult> Index()
        {
            var search = HttpContext.Request.Query["search"];
            var compras = _context.Compras
                .Include( c => c.Fornecedores)
                .Include( c => c.Produtos )
                .AsQueryable();
            if (!string.IsNullOrEmpty(search))
            {
                search = search.ToString().ToLower();

                compras = compras.Where(compra =>
                    compra.Produtos.Descricao.ToLower().Contains(search) ||
                    compra.Fornecedores.RazaoSocial.ToLower().Contains(search)
                    );
            }
            return compras != null ?
                View(await compras.ToListAsync()) :
                Problem("Entity set 'Contexto.Alunos' is null.");
        }

        // GET: Compras/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compras = await _context.Compras
                .Include(c => c.Fornecedores)
                .Include(c => c.Produtos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (compras == null)
            {
                return NotFound();
            }

            return View(compras);
        }

        // GET: Compras/Create
        public IActionResult Create()
        {
            ViewData["FornecedoresId"] = new SelectList(_context.Fornecedores, "Id", "RazaoSocial");
            ViewData["ProdutosId"] = new SelectList(_context.Produtos, "Id", "Descricao");
            return View();
        }

        // POST: Compras/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProdutosId,FornecedoresId,Data")] Compras compras, int quantidade )
        {
            if (ModelState.IsValid)
            {
                if( quantidade <= 0 )
                {
                    TempData["Erro"] = "A quantidade deve ser maior que zero.";
                    return RedirectToAction(nameof(Create));
                }
            
                var produto = await _context.Produtos
                    .FirstAsync( p => p.Id == compras.ProdutosId );

                if( produto != null )
                {
                    produto.QtdeEstoque += quantidade;
                }
                _context.Add(compras);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FornecedoresId"] = new SelectList(_context.Fornecedores, "Id", "RazaoSocial", compras.FornecedoresId);
            ViewData["ProdutosId"] = new SelectList(_context.Produtos, "Id", "Descricao", compras.ProdutosId);
            return View(compras);
        }

        // GET: Compras/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compras = await _context.Compras.FindAsync(id);
            if (compras == null)
            {
                return NotFound();
            }
            ViewData["FornecedoresId"] = new SelectList(_context.Fornecedores, "Id", "RazaoSocial", compras.FornecedoresId);
            ViewData["ProdutosId"] = new SelectList(_context.Produtos, "Id", "Descricao", compras.ProdutosId);
            return View(compras);
        }

        // POST: Compras/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProdutosId,FornecedoresId,Data")] Compras compras)
        {
            if (id != compras.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(compras);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ComprasExists(compras.Id))
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
            ViewData["FornecedoresId"] = new SelectList(_context.Fornecedores, "Id", "RazaoSocial", compras.FornecedoresId);
            ViewData["ProdutosId"] = new SelectList(_context.Produtos, "Id", "Descricao", compras.ProdutosId);
            return View(compras);
        }

        // GET: Compras/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var compras = await _context.Compras
                .Include(c => c.Fornecedores)
                .Include(c => c.Produtos)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (compras == null)
            {
                return NotFound();
            }

            return View(compras);
        }

        // POST: Compras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var compras = await _context.Compras.FindAsync(id);
            if (compras != null)
            {
                _context.Compras.Remove(compras);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ComprasExists(int id)
        {
            return _context.Compras.Any(e => e.Id == id);
        }
    }
}
