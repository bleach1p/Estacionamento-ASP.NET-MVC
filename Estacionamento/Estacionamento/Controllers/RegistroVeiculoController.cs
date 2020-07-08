using Estacionamento.AccessLayer.Context;
using Estacionamento.AccessLayer.Entities;
using Estacionamento.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Estacionamento.Controllers
{
    public class RegistroVeiculoController : Controller
    {
        private readonly EstacionamentoContext _context;
        private readonly TabelaPrecoService _tabelaPrecoService;
        private readonly RegistroVeiculoService _registroVeiculoService;

        public RegistroVeiculoController(EstacionamentoContext context,
            TabelaPrecoService tabelaPrecoService,
            RegistroVeiculoService registroVeiculoService)
        {
            _context = context;
            _tabelaPrecoService = tabelaPrecoService;
            _registroVeiculoService = registroVeiculoService;

        }

        public async Task<IActionResult> Index()
        {
            var veiculos = await _context.RegistroVeiculo.ToListAsync();

            return View(veiculos);
        }

        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroVeiculo = await _context.RegistroVeiculo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroVeiculo == null)
            {
                return NotFound();
            }

            return View(registroVeiculo);
        }

        public async Task<IActionResult> MarcarSaida(long? id)
        {
            var registroVeiculo = await _context.RegistroVeiculo.FirstOrDefaultAsync(m => m.Id == id);

            registroVeiculo.DataSaida = DateTime.Now;

            registroVeiculo = _registroVeiculoService.CalculaValores(registroVeiculo);

            _context.RegistroVeiculo.Update(registroVeiculo);
            _context.SaveChanges();

            return RedirectToAction("Index", await _context.RegistroVeiculo.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PlacaVeiculo,DataEntrada,DataSaida,PrecoHora,ValorPagar")] RegistroVeiculo registroVeiculo)
        {
            if (ModelState.IsValid)
            {
                if (!ValidarForm(registroVeiculo))
                    return View(registroVeiculo);

                if (registroVeiculo.DataSaida != null)
                {
                    registroVeiculo = _registroVeiculoService.CalculaValores(registroVeiculo);
                }

                _context.Add(registroVeiculo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(registroVeiculo);
        }

        private bool ValidarForm(RegistroVeiculo registroVeiculo)
        {

            if (!_registroVeiculoService.ValidaDataSaida(registroVeiculo.DataSaida, registroVeiculo.DataEntrada))
                ModelState.AddModelError(string.Empty, "A Data de Saída deve Ser maior que a data de Entrada");

            if (!_registroVeiculoService.ValidaDataEntrada(registroVeiculo.DataEntrada))
                ModelState.AddModelError(string.Empty, "A Data de Entrada deve Ser Menor ou Igual a Data Atual");

            if (ModelState.ErrorCount > 0)
                return false;
            return true;
        }

        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroVeiculo = await _context.RegistroVeiculo.FindAsync(id);
            if (registroVeiculo == null)
            {
                return NotFound();
            }
            return View(registroVeiculo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,PlacaVeiculo,DataEntrada,DataSaida,PrecoHora,ValorPagar")] RegistroVeiculo registroVeiculo)
        {
            if (id != registroVeiculo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (!ValidarForm(registroVeiculo))
                        return View(registroVeiculo);

                    registroVeiculo = _registroVeiculoService.CalculaValores(registroVeiculo);

                    _context.Update(registroVeiculo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegistroVeiculoExists(registroVeiculo.Id))
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
            return View(registroVeiculo);
        }

        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var registroVeiculo = await _context.RegistroVeiculo
                .FirstOrDefaultAsync(m => m.Id == id);
            if (registroVeiculo == null)
            {
                return NotFound();
            }

            return View(registroVeiculo);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var registroVeiculo = await _context.RegistroVeiculo.FindAsync(id);
            _context.RegistroVeiculo.Remove(registroVeiculo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegistroVeiculoExists(long id)
        {
            return _context.RegistroVeiculo.Any(e => e.Id == id);
        }
    }
}
