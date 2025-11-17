using ApiRestDespliegue.Interfaces.Pajaros;
using ApiRestDespliegue.Models.Pajaros;
using MongoDB.Driver;

namespace ApiRestDespliegue.Services.Pajaros
{
    public class MongoPajaroRepositoryService : IMongoPajaroRepositoryService
    {
        private readonly IMongoCollection<Pajaro> _pajaros;

        public MongoPajaroRepositoryService(MongoDbService mongoDb)
        {
            _pajaros = mongoDb.Pajaros;
        }

        public async Task CreateAsync(Pajaro pajaro)
        {
            await _pajaros.InsertOneAsync(pajaro);
        }

        public async Task<Pajaro?> GetByIdAsync(string id)
        {
            return await _pajaros.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Pajaro>> GetAllAsync()
        {
            return await _pajaros.Find(_ => true).ToListAsync();
        }

        // Método para filtrar pájaros por especie
        public async Task<IEnumerable<Pajaro>> GetByEspecieAsync(string especie)
        {
            return await _pajaros.Find(p => p.Especie == especie).ToListAsync();
        }

        // Método para actualizar un pájaro
        public async Task UpdateAsync(Pajaro pajaro)
        {
            var filter = Builders<Pajaro>.Filter.Eq(p => p.Id, pajaro.Id);
            await _pajaros.ReplaceOneAsync(filter, pajaro);
        }

        // Método para eliminar un pájaro por Id
        public async Task DeleteAsync(string id)
        {
            var filter = Builders<Pajaro>.Filter.Eq(p => p.Id, id);
            await _pajaros.DeleteOneAsync(filter);
        }
    }
}
