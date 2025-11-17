using ApiRestDespliegue.Models.Login;
using ApiRestDespliegue.Models.Pajaros;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace ApiRestDespliegue.Services
{
    public class MongoDbService
    {
        private readonly IMongoDatabase _database;

        public MongoDbService(IOptions<MongoDbSettings> mongoSettings)
        {
            try
            {
            var client = new MongoClient(mongoSettings.Value.ConnectionString);
            _database = client.GetDatabase(mongoSettings.Value.DatabaseName);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

        }

        public IMongoCollection<User> Users =>
            _database.GetCollection<User>("Users");
        public IMongoCollection<Pajaro> Pajaros => _database.GetCollection<Pajaro>("Pajaros");
        public IMongoCollection<HistoricoPajaro> HistoricoPajaros => _database.GetCollection<HistoricoPajaro>("Pajaros_Historico");
    }

    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } = string.Empty;
        public string DatabaseName { get; set; } = string.Empty;
        public string UsersCollectionName { get; set; } = string.Empty;
    }
}
