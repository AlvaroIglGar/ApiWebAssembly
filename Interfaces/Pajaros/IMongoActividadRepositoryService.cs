using ApiRestDespliegue.Models.Pajaros;

namespace ApiRestDespliegue.Interfaces.Pajaros
{
    public interface IMongoActividadRepositoryService
    {
        Task<List<Actividad>> GetAllAsync();
        Task<Actividad?> GetByIdAsync(string id);
        Task CreateAsync(Actividad tipoComida);
        Task UpdateAsync(Actividad tipoComida);
        Task DeleteAsync(string id);
    }
}
