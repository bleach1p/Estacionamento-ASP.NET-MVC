using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Estacionamento.AccessLayer.Context;
using Estacionamento.AccessLayer.Entities;

namespace Estacionamento.Controllers
{
    public class TabelaPrecoController : Controller
    {
        private readonly EstacionamentoContext _context;

        public TabelaPrecoController(EstacionamentoContext context)
        {
            _context = context;
        }

        // GET: TabelaPreco
        public async Task<IActionResult> Index()
        {
            return View(await _context.TabelaPreco.ToListAsync());
        }

        // GET: TabelaPreco/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tabelaPreco = await _context.TabelaPreco
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tabelaPreco == null)
            {
                return NotFound();
            }

            return View(tabelaPreco);
        }

        // GET: TabelaPreco/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TabelaPreco/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ValorHoraInicial,ValorHora,VigenciaInicial,VigenciaFinal")] TabelaPreco tabelaPreco)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tabelaPreco);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tabelaPreco);
        }

        // GET: TabelaPreco/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tabelaPreco = await _context.TabelaPreco.FindAsync(id);
            if (tabelaPreco == null)
            {
                return NotFound();
            }
            return View(tabelaPreco);
        }

        // POST: TabelaPreco/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,ValorHoraInicial,ValorHora")] TabelaPreco tabelaPreco)
        {
            if (id != tabelaPreco.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tabelaPreco);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TabelaPrecoExists(tabelaPreco.Id))
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
            return View(tabelaPreco);
        }

        // GET: TabelaPreco/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tabelaPreco = await _context.TabelaPreco
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tabelaPreco == null)
            {
                return NotFound();
            }

            return View(tabelaPreco);
        }

        // POST: TabelaPreco/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var tabelaPreco = await _context.TabelaPreco.FindAsync(id);
            _context.TabelaPreco.Remove(tabelaPreco);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TabelaPrecoExists(long id)
        {
            return _context.TabelaPreco.Any(e => e.Id == id);
        }
    }
}
