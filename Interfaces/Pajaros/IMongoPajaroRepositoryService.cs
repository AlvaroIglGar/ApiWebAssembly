using ApiRestDespliegue.Models.Pajaros;

namespace ApiRestDespliegue.Interfaces.Pajaros
{
    public interface IMongoPajaroRepositoryService
    {
        Task CreateAsync(Pajaro pajaro);
        Task<Pajaro?> GetByIdAsync(string id);
        Task<IEnumerable<Pajaro>> GetAllAsync();
        Task<IEnumerable<Pajaro>> GetByEspecieAsync(string especie); // método adicional útil
        Task UpdateAsync(Pajaro pajaro); // actualizar un pájaro
        Task DeleteAsync(string id); // eliminar un pájaro
    }
}
