using ApiRestDespliegue.Models.Pajaros;

namespace ApiRestDespliegue.Interfaces.Pajaros
{
    public interface IMongoHistoricoPajaroRepositoryService
    {
        Task CreateAsync(HistoricoPajaro registro);
        Task<HistoricoPajaro?> GetByIdAsync(string id);
        Task<IEnumerable<HistoricoPajaro>> GetAllAsync();
        Task<IEnumerable<HistoricoPajaro>> GetByPajaroIdAsync(string pajaroId);
        Task<IEnumerable<HistoricoPajaro>> GetByPajaroIdAndDateRangeAsync(string pajaroId, DateTime fechaInicio, DateTime fechaFin);
        Task UpdateAsync(HistoricoPajaro registro);
        Task DeleteAsync(string id);
    }
}