using ApiRestDespliegue.Interfaces.Pajaros;
using ApiRestDespliegue.Models.Pajaros;
using MongoDB.Driver;

namespace ApiRestDespliegue.Services.Pajaros
{
    public class MongoTipoComidaRepositoryService : IMongoTipoComidaRepositoryService
    {
        private readonly IMongoCollection<TipoComida> _tiposComida;

        public MongoTipoComidaRepositoryService(MongoDbService mongoDb)
        {
            _tiposComida = mongoDb.TipoComida; // Asegúrate de exponer la colección en MongoDbService
        }

        public async Task CreateAsync(TipoComida tipoComida)
        {
            await _tiposComida.InsertOneAsync(tipoComida);
        }

        public async Task<TipoComida?> GetByIdAsync(string id)
        {
            return await _tiposComida.Find(t => t.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<TipoComida>> GetAllAsync()
        {
            return await _tiposComida.Find(_ => true).ToListAsync();
        }

        public async Task UpdateAsync(TipoComida tipoComida)
        {
            var filter = Builders<TipoComida>.Filter.Eq(t => t.Id, tipoComida.Id);
            await _tiposComida.ReplaceOneAsync(filter, tipoComida);
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<TipoComida>.Filter.Eq(t => t.Id, id);
            await _tiposComida.DeleteOneAsync(filter);
        }
    }
}
