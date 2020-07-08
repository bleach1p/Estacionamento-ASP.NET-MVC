using Estacionamento.AccessLayer.Entities;
using Estacionamento.Models;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.AccessLayer.Context
{
    public class EstacionamentoContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options) => options.UseSqlite("DataSource=app.db");

        public DbSet<RegistroVeiculo> RegistroVeiculo { get; set; }

        public DbSet<TabelaPreco> TabelaPreco { get; set; }

    }
}