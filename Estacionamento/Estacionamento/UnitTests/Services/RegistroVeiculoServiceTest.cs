using Estacionamento.Services;
using System;
using Xunit;

namespace Estacionamento.UnitTests.Services
{
    public class RegistroVeiculoServiceTest
    {
        private readonly RegistroVeiculoService _registroVeiculoService;
        private readonly TabelaPrecoService _tabelaPrecoService;
        
        public RegistroVeiculoServiceTest()
        {
            _tabelaPrecoService = new TabelaPrecoService(null);
            _registroVeiculoService = new RegistroVeiculoService(_tabelaPrecoService);
        }

        [Fact]
        public void ValidaDataSaida_DataInicialMaiorQueFinal()
        {
            var dataSaida = DateTime.Now.AddDays(-1);
            var dataEntrada = DateTime.Now;

            Assert.True(!_registroVeiculoService.ValidaDataSaida(dataSaida, dataEntrada));
        }

        [Fact]
        public void ValidaDataSaida_DataEntradaMaiorDataAtual()
        {
            var dataEntrada = DateTime.Now.AddDays(1);

            Assert.True(!_registroVeiculoService.ValidaDataSaida(null, dataEntrada));
        }

        [Fact]
        public void ValidaDataSaida_Ok()
        {
            var dataEntrada = DateTime.Now;
            var dataSaida = DateTime.Now.AddDays(2);
            
            Assert.True(_registroVeiculoService.ValidaDataSaida(dataSaida, dataEntrada));
        }

    }
}
