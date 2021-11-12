using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProAgil.Domain;

namespace ProAgil.Repository
{
    public class ProAgilRepository : IProAgilRepository
    {
        public ProAgilContext _context { get; }

        public ProAgilRepository(ProAgilContext context)
        {
            _context = context;
            _context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            // Tracking and No Tracking do EF CORE
            //quando voce poe o item acima, voce informa que nao que no EF a query participem de um ambiente rastreável
            //que irá impedir de realizar mudanças no savechanges.. já que ele esta sendo rastreado, fzendo com que 
            //nosso recurso no EF não seja travado.
        }

       //GERAIS
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Update<T>(T entity) where T : class
        {
            _context.Update(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
           return (await _context.SaveChangesAsync()) > 0;
        }


        //EVENTO
        public async Task<Evento[]> GetAllEventosAsync(bool includePalestrantes = false)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedesSociais);

            if(includePalestrantes)
            {
                query = query
                    .AsNoTracking() //o entity framework, tem um recurso para travar a entidade para voce pode fazer a edição
                                    //para impedir que isso ocorra e voce nao consiga mudar sua entidade, por exemplo
                                    //no metodo put.. voce faz um get(), depois com os dados voce faz um update.. ele 
                    .Include(pe => pe.PalestrantesEventos) //Aqui nesta entidade tenho apenas dois campos idEvento e idPalestrante
                    .ThenInclude(p => p.Palestrante); //ThenInclude é inclua também .. Palestrante, para vir as informaçoes do palestrante
            }

            query = query.OrderByDescending(c => c.DataEvento);

            return await query.ToArrayAsync();
        }

        public async Task<Evento> GetEventoAsyncById(int EventoId, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedesSociais);

            if(includePalestrantes)
            {
                query = query
                    .Include(pe => pe.PalestrantesEventos) //Aqui nesta entidade tenho apenas dois campos idEvento e idPalestrante
                    .ThenInclude(p => p.Palestrante); //ThenInclude é inclua também .. Palestrante, para vir as informaçoes do palestrante
            }

            query = query.OrderByDescending(c => c.DataEvento)
                         .Where(c => c.Id == EventoId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrantes)
        {
            IQueryable<Evento> query = _context.Eventos
            .Include(c => c.Lotes)
            .Include(c => c.RedesSociais);

            if(includePalestrantes)
            {
                query = query
                    .Include(pe => pe.PalestrantesEventos) //Aqui nesta entidade tenho apenas dois campos idEvento e idPalestrante
                    .ThenInclude(p => p.Palestrante); //ThenInclude é inclua também .. Palestrante, para vir as informaçoes do palestrante
            }

            query = query.OrderByDescending(c => c.DataEvento)
                         .Where(c => c.Tema.ToLower().Contains(tema.ToLower()));

            return await query.ToArrayAsync();
        }
 

        //PALESTRANTE
        public async Task<Palestrante> GetPalestranteAsyncById(int PalestranteId, bool includeEventos = false)
        {
             IQueryable<Palestrante> query = _context.Palestrantes
            .Include(p => p.RedesSociais);

            if(includeEventos)
            {
                query = query
                    .Include(pe => pe.PalestrantesEventos) //Aqui nesta entidade tenho apenas dois campos idEvento e idPalestrante
                    .ThenInclude(e => e.Evento); //ThenInclude é inclua também .. Evento, para vir as informaçoes do evento
            }

            query = query.OrderBy(p => p.Nome)
                .Where(p => p.Id == PalestranteId);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<Palestrante[]> GetAllPalestrantesAsyncByName(string name, bool includeEventos = false)
        {
            IQueryable<Palestrante> query = _context.Palestrantes
            .Include(p => p.RedesSociais);

            if(includeEventos)
            {
                query = query
                    .Include(pe => pe.PalestrantesEventos) //Aqui nesta entidade tenho apenas dois campos idEvento e idPalestrante
                    .ThenInclude(e => e.Evento); //ThenInclude é inclua também .. Evento, para vir as informaçoes do evento
            }

            query = query.Where(p => p.Nome.ToLower().Contains(name.ToLower()));

            return await query.ToArrayAsync();
        }
    }
}