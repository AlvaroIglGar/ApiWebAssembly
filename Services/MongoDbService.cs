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
        public IMongoCollection<TipoComida> TipoComida => _database.GetCollection<TipoComida>("TipoComida");
        public IMongoCollection<Actividad> Actividad => _database.GetCollection<Actividad>("Actividad");
    }

    public class MongoDbSettings
    {
        public string ConnectionString { get; set; } =
         Environment.GetEnvironmentVariable("MONGO_URI") ?? "";

        public string DatabaseName { get; set; } =
            Environment.GetEnvironmentVariable("MONGO_DB_NAME") ?? "AppPruebasDB";

        public string UsersCollectionName { get; set; } =
            Environment.GetEnvironmentVariable("MONGO_USERS_COLLECTION") ?? "Users";
    }
}
