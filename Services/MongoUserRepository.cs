using ApiRestDespliegue.Interfaces;
using ApiRestDespliegue.Models.Login;
using MongoDB.Driver;

namespace ApiRestDespliegue.Services
{
    public class MongoUserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;

        public MongoUserRepository(MongoDbService mongoDb)
        {
            _users = mongoDb.Users; // suponiendo que MongoDbService expone IMongoCollection<User> Users
        }

        public async Task CreateAsync(User user)
        {
            await _users.InsertOneAsync(user);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _users.Find(_ => true).ToListAsync();
        }
    }
}
