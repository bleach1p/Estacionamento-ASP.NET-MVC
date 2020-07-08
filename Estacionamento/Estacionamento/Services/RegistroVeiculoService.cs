using Estacionamento.AccessLayer.Context;
using Estacionamento.AccessLayer.Entities;
using System;

namespace Estacionamento.Services
{
    public class RegistroVeiculoService
    {
        private readonly TabelaPrecoService _tabelaPrecoService;

        public RegistroVeiculoService(TabelaPrecoService tabelaPrecoService)
        {
            _tabelaPrecoService = tabelaPrecoService;
        }

        public RegistroVeiculo CalculaValores(RegistroVeiculo veiculo)
        {
                var tabela = _tabelaPrecoService.RetornaTabelaValida(veiculo);

                if (tabela != null)
                {
                    veiculo.PrecoHora = tabela.ValorHora;
                    veiculo.ValorPagar = RetornaValorPagar(tabela, (TimeSpan)(veiculo.DataSaida - veiculo.DataEntrada));
                }

                return veiculo;
        }

        private decimal RetornaValorPagar(TabelaPreco tabela, TimeSpan duracao)
        {
            var totalHoras = (long)duracao.TotalHours;
            var minutos = duracao.Minutes;
            var segundos = duracao.Seconds;

            if ((minutos >= 10 && segundos > 0) || minutos > 10)
            {
                totalHoras++;
            }

            return (totalHoras * tabela.ValorHora) + tabela.ValorHoraInicial;

        }

        public bool ValidaDataSaida(DateTime? dataSaida, DateTime dataEntrada)
        {
            if (dataSaida == null)
                return true;
            else
                if (dataSaida < dataEntrada)
                return false;
            else
                return true;
        }

        public bool ValidaDataEntrada(DateTime dataEntrada)
        {
            if (dataEntrada > DateTime.Now)
                return false;
            return true;
        }
    }
}
