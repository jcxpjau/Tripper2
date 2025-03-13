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
    public class EstabelecimentosController : Controller
    {
        private readonly Contexto _context;

        public EstabelecimentosController(Contexto context)
        {
            _context = context;
        }

        // GET: Estabelecimentos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Estabelecimentos.ToListAsync());
        }

        // GET: Estabelecimentos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estabelecimentos = await _context.Estabelecimentos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estabelecimentos == null)
            {
                return NotFound();
            }

            return View(estabelecimentos);
        }

        // GET: Estabelecimentos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Estabelecimentos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RazaoSocial,CNPJ")] Estabelecimentos estabelecimentos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(estabelecimentos);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(estabelecimentos);
        }

        // GET: Estabelecimentos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estabelecimentos = await _context.Estabelecimentos.FindAsync(id);
            if (estabelecimentos == null)
            {
                return NotFound();
            }
            return View(estabelecimentos);
        }

        // POST: Estabelecimentos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RazaoSocial,CNPJ")] Estabelecimentos estabelecimentos)
        {
            if (id != estabelecimentos.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(estabelecimentos);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstabelecimentosExists(estabelecimentos.Id))
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
            return View(estabelecimentos);
        }

        // GET: Estabelecimentos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estabelecimentos = await _context.Estabelecimentos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estabelecimentos == null)
            {
                return NotFound();
            }

            return View(estabelecimentos);
        }

        // POST: Estabelecimentos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estabelecimentos = await _context.Estabelecimentos.FindAsync(id);
            if (estabelecimentos != null)
            {
                _context.Estabelecimentos.Remove(estabelecimentos);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstabelecimentosExists(int id)
        {
            return _context.Estabelecimentos.Any(e => e.Id == id);
        }
    }
}
