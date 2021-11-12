using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilContext : DbContext
    {
        public ProAgilContext(DbContextOptions<ProAgilContext> options) : base (options){}

        public DbSet<Evento> Eventos{get; set;}
        public DbSet<Palestrante> Palestrantes{get; set;}
        public DbSet<PalestranteEvento> PalestranteEventos{get; set;}
        public DbSet<Lote> Lotes{get; set;}
        public DbSet<RedeSocial> RedeSociais{get; set;}
        
        //como aqui dentro eu tenho uma relação de N para N.. eu preciso especificar aqui dentro essa relação.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PalestranteEvento>()
            //só especificar que esses dois campos contidos na tabela PalestranteEvento
            //, ele já vai entender que esta tabela esta amarrando outras duas por essas chaves
            .HasKey(PE => new {PE.EventoId, PE.PalestranteId});
        }
    }
}