using ApiRestDespliegue.Models.Login;

namespace ApiRestDespliegue.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task CreateAsync(User user);
        Task<IEnumerable<User>> GetAllAsync();
    }
}
