using System;
using System.ComponentModel.DataAnnotations;

namespace Estacionamento.AccessLayer.Entities
{
    public class TabelaPreco
    {
        [Microsoft.AspNetCore.Mvc.HiddenInput]
        public long Id { get; set; }

        [Display(Name = "Valor Da Hora Inicial")]
        public decimal ValorHoraInicial { get; set; }

        [Display(Name = "Valor Hora")]
        public decimal ValorHora { get; set; }

        [Display(Name = "Vigência Inicial")]
        public DateTime VigenciaInicial { get; set; }

        [Display(Name = "Vigência Final")]
        public DateTime VigenciaFinal { get; set; }
    }
}
