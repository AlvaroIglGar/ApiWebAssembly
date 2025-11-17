using ApiRestDespliegue.Models.Pajaros;

namespace ApiRestDespliegue.Interfaces.Pajaros
{
    public interface IMongoHistoricoPajaroRepositoryService
    {
        Task CreateAsync(HistoricoPajaro registro);
        Task<HistoricoPajaro?> GetByIdAsync(string id);
        Task<IEnumerable<HistoricoPajaro>> GetAllAsync();
        Task<IEnumerable<HistoricoPajaro>> GetByPajaroIdAsync(string pajaroId); // obtener histórico de un pájaro
        Task UpdateAsync(HistoricoPajaro registro); // actualizar un registro histórico
        Task DeleteAsync(string id); // eliminar un registro histórico
    }
}
