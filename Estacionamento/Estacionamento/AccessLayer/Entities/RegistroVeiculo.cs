using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Estacionamento.AccessLayer.Entities
{
    public class RegistroVeiculo
    {
        [HiddenInput]
        public long Id { get; set; }

        [Display(Name = "Placa"), Required, MaxLength(7)]
        public string PlacaVeiculo { get; set; }

        [Display(Name = "Data de Entrada"), Required]
        public DateTime DataEntrada { get; set; } = DateTime.Now;

        [Display(Name = "Data de Saída")]
        public DateTime? DataSaida { get; set; }

        [NotMapped, Display(Name = "Duração")]
        public string Duracao
        {
            get
            {
                if (DataSaida == null)
                    return "00:00:00";

                var tempo = (TimeSpan)(DataSaida - DataEntrada);
                return string.Format("{0:00}:{1:00}:{2:00}", tempo.TotalHours, tempo.Minutes, tempo.Seconds);
            }
        }

        [Display(Name = "Preço Hora")]
        public decimal PrecoHora { get; set; } = 0;

        [Display(Name = "Valor A Pagar")]
        public decimal ValorPagar { get; set; } = 0;
    }
}
