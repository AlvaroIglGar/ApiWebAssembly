using ApiRestDespliegue.Interfaces.Pajaros;
using ApiRestDespliegue.Models.Pajaros;
using MongoDB.Driver;

namespace ApiRestDespliegue.Services.Pajaros
{
    public class MongoActividadRepositoryService : IMongoActividadRepositoryService
    {
        private readonly IMongoCollection<Actividad> _Actividad;

        public MongoActividadRepositoryService(MongoDbService mongoDb)
        {
            _Actividad = mongoDb.Actividad; // Asegúrate de exponer la colección en MongoDbService
        }

        public async Task CreateAsync(Actividad Actividad)
        {
            await _Actividad.InsertOneAsync(Actividad);
        }

        public async Task<Actividad?> GetByIdAsync(string id)
        {
            return await _Actividad.Find(t => t.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<Actividad>> GetAllAsync()
        {
            return await _Actividad.Find(_ => true).ToListAsync();
        }

        public async Task UpdateAsync(Actividad Actividad)
        {
            var filter = Builders<Actividad>.Filter.Eq(t => t.Id, Actividad.Id);
            await _Actividad.ReplaceOneAsync(filter, Actividad);
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<Actividad>.Filter.Eq(t => t.Id, id);
            await _Actividad.DeleteOneAsync(filter);
        }
    }
}
