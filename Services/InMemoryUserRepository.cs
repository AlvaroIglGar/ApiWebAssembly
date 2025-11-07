using ApiRestDespliegue.Interfaces;
using ApiRestDespliegue.Models.Login;
using System.Collections.Concurrent;

namespace ApiRestDespliegue.Services
{
    public class InMemoryUserRepository : IUserRepository
    {
        // ConcurrentDictionary para seguridad en concurrencia simple
        private readonly ConcurrentDictionary<string, User> _users = new();

        public InMemoryUserRepository()
        {
            // Seed: crear un usuario de prueba con contraseña hasheada
            var admin = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = "admin@ejemplo.com",
                Nombre = "Administrador",
                Rol = "Admin",
                FechaRegistro = DateTime.UtcNow,
                Activo = true
            };

            // Genera hash seguro en tiempo de ejecución (BCrypt)
            admin.PasswordHash = BCrypt.Net.BCrypt.HashPassword("1234");

            _users[admin.Email.ToLowerInvariant()] = admin;

            // Otro usuario de prueba
            var user = new User
            {
                Id = Guid.NewGuid().ToString(),
                Email = "ale@gmail.com",
                Nombre = "Alejandra",
                Rol = "User",
                FechaRegistro = DateTime.UtcNow,
                Activo = true
            };
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword("1234");
            _users[user.Email.ToLowerInvariant()] = user;
        }

        public Task CreateAsync(User user)
        {
            // Asegurarse de que el email es la clave
            _users[user.Email.ToLowerInvariant()] = user;
            return Task.CompletedTask;
        }

        public Task<User?> GetByEmailAsync(string email)
        {
            if (email == null) return Task.FromResult<User?>(null);
            _users.TryGetValue(email.ToLowerInvariant(), out var user);
            return Task.FromResult<User?>(user);
        }

        public Task<IEnumerable<User>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<User>>(_users.Values.ToList());
        }
    }
}
