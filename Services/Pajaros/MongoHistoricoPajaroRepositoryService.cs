using ApiRestDespliegue.Interfaces.Pajaros;
using ApiRestDespliegue.Models.Pajaros;
using MongoDB.Driver;

namespace ApiRestDespliegue.Services.Pajaros
{
    public class MongoHistoricoPajaroRepositoryService : IMongoHistoricoPajaroRepositoryService
    {
        private readonly IMongoCollection<HistoricoPajaro> _historico;

        public MongoHistoricoPajaroRepositoryService(MongoDbService mongoDb)
        {
            _historico = mongoDb.HistoricoPajaros;
        }

        public async Task CreateAsync(HistoricoPajaro registro)
        {
            await _historico.InsertOneAsync(registro);
        }

        public async Task<HistoricoPajaro?> GetByIdAsync(string id)
        {
            return await _historico.Find(h => h.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<HistoricoPajaro>> GetByPajaroIdAsync(string pajaroId)
        {
            return await _historico
                .Find(h => h.PajaroId == pajaroId)
                .SortByDescending(h => h.FechaRegistro)
                .ToListAsync();
        }

        public async Task<IEnumerable<HistoricoPajaro>> GetByPajaroIdAndDateRangeAsync(string pajaroId, DateTime fechaInicio, DateTime fechaFin)
        {
            var filterBuilder = Builders<HistoricoPajaro>.Filter;

            var filter = filterBuilder.Eq(h => h.PajaroId, pajaroId) &
                         filterBuilder.Gte(h => h.FechaRegistro, fechaInicio) &
                         filterBuilder.Lte(h => h.FechaRegistro, fechaFin);

            return await _historico
                .Find(filter)
                .SortBy(h => h.FechaRegistro)
                .ThenBy(h => h.MomentoDelDia)
                .ToListAsync();
        }

        public async Task<IEnumerable<HistoricoPajaro>> GetAllAsync()
        {
            return await _historico
                .Find(_ => true)
                .SortByDescending(h => h.FechaRegistro)
                .ToListAsync();
        }

        public async Task UpdateAsync(HistoricoPajaro registro)
        {
            var filter = Builders<HistoricoPajaro>.Filter.Eq(h => h.Id, registro.Id);
            await _historico.ReplaceOneAsync(filter, registro);
        }

        public async Task DeleteAsync(string id)
        {
            var filter = Builders<HistoricoPajaro>.Filter.Eq(h => h.Id, id);
            await _historico.DeleteOneAsync(filter);
        }
    }
}