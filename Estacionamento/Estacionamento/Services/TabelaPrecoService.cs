using Estacionamento.AccessLayer.Context;
using Estacionamento.AccessLayer.Entities;
using System;
using System.Linq;

namespace Estacionamento.Services
{
    public class TabelaPrecoService
    {
        private readonly EstacionamentoContext _context;

        public TabelaPrecoService(EstacionamentoContext context)
        {
            _context = context;
        }

        public TabelaPreco RetornaTabelaValida(RegistroVeiculo veiculo)
        {
            var tabela = _context.TabelaPreco.Where(x => x.VigenciaInicial <= veiculo.DataEntrada && x.VigenciaFinal >= veiculo.DataSaida).FirstOrDefault();

            return tabela;
        }
    }
}
