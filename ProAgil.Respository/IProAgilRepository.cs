using System.Threading.Tasks;
using ProAgil.Domain;

namespace ProAgil.Respository
{
    public interface IProAgilRepository
    {
        //Geral
        void Add<T>(T entity) where T: class;
        void Update<T>(T entity) where T: class;
        void Delete<T>(T entity) where T: class;
        Task<bool> SaveChangesAsync();
        
        //Eventos
        Task<Evento[]> GetAllEventoAsyncByTema(string tema, bool includePalestrantes);
        Task<Evento[]> GetAllEventoAsync(bool includePalestrantes);
        Task<Evento> GetEventoAsyncById(int eventoId, bool includePalestrantes);

        Task<Palestrante[]> GetAllPalestrantesAsync(bool includeEventos);
        Task<Palestrante> GetPalestranteAsyncById(int palestranteId, bool includeEventos);
        Task<Palestrante[]> GetPalestrantesAsyncByName(string palestranteNome, bool includeEventos);
    }
}