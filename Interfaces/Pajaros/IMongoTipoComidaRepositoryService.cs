using ApiRestDespliegue.Models.Pajaros;

namespace ApiRestDespliegue.Interfaces.Pajaros
{
    public interface IMongoTipoComidaRepositoryService
    {
        Task<List<TipoComida>> GetAllAsync();
        Task<TipoComida?> GetByIdAsync(string id);
        Task CreateAsync(TipoComida tipoComida);
        Task UpdateAsync(TipoComida tipoComida);
        Task DeleteAsync(string id);
    }
}
