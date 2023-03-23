using IntegraCTE.Core.Context;
using IntegraCTE.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntegraCTE.Infra.Context
{
    public class IntegraCTEContext : DbContext, IIntegraCTEContext
    {
        public IntegraCTEContext(DbContextOptions<IntegraCTEContext> options) : base(options)
        {
            this.Database.Migrate();
        }
        public IQueryable<ArquivoModel> ArquivoCTE => ArquivoCTEDb;

        public IQueryable<CTEModel> CTE => CTEDb;

        public IQueryable<TransportadoraModel> Transportadora => TransportadoraDb;

        
        public DbSet<ArquivoModel> ArquivoCTEDb { get; set; }
        public DbSet<CTEModel> CTEDb { get; set; }
        public DbSet<TransportadoraModel> TransportadoraDb { get; set; }

        public async Task Adicionar<T>(T t)
        {
            this.Entry(t).State = EntityState.Added;
            await Task.CompletedTask;
        }

        public async Task Atualizar<T>(T t)
        {
            this.Entry(t).State = EntityState.Modified;
            await Task.CompletedTask;
        }

        public async Task Remover<T>(T t)
        {
            this.Entry(t).State = EntityState.Deleted;
            await Task.CompletedTask;
        }   



        public async Task<int> SaveChangesAsync()
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

            return await base.SaveChangesAsync();
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=localhost;Database=IntegraCteDB;user id=sa;password=Password_01", p =>
        //    {
        //        p.EnableRetryOnFailure(
        //             maxRetryCount: 2,
        //             maxRetryDelay: TimeSpan.FromSeconds(5),
        //             errorNumbersToAdd: null)
        //         .MigrationsHistoryTable("EFHistory_IntegraCTE");

        //    });
        //    optionsBuilder.EnableDetailedErrors();
        //    optionsBuilder.EnableSensitiveDataLogging();
        //}
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IntegraCTEContext).Assembly);
            MapearPropriedadesEsquecidas(modelBuilder);
            //IgnoraPropriedades(modelBuilder);
        }

        private void MapearPropriedadesEsquecidas(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entity.GetProperties().Where(p => p.ClrType == typeof(string));

                foreach (var property in properties)
                {
                    if (string.IsNullOrEmpty(property.GetColumnType())
                        && !property.GetMaxLength().HasValue)
                    {
                        //property.SetMaxLength(100);
                        property.SetColumnType("VARCHAR(100)");
                    }
                }
            }
        }
    }
}
