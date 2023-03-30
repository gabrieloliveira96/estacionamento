using Estacionamento.Domain.Data;
using Estacionamento.Domain.DomainObjetcs.Messsages;
using Estacionamento.Domain.Entities;
using Estacionamento.Domain.Entities.Vagas;
using Estacionamento.Domain.Entities.Veiculos;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Estacionamento.Infra.Contexts
{
    public class EstacionamentoContext : DbContext, IUnitOfWork
    {
        private readonly IMediator _mediatorHandler;


        public EstacionamentoContext(DbContextOptions<EstacionamentoContext> options, IMediator mediatorHandler) : base(options)
        {
            _mediatorHandler = mediatorHandler;
        }
        public DbSet<Vaga> Vaga { get; set; }
        public DbSet<RegistroVeiculo> RegistroVeiculo { get; set; }



        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("DataCadastro") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("DataCadastro").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("DataCadastro").IsModified = false;
                }
            }

            var sucesso = await base.SaveChangesAsync() > 0;
            if (sucesso) await _mediatorHandler.PublicarEventos(this);

            return sucesso;
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Ignore<Event>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EstacionamentoContext).Assembly);

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientCascade;

            base.OnModelCreating(modelBuilder);
        }
    }
}
