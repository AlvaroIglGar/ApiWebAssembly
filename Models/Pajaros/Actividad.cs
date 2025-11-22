using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiRestDespliegue.Models.Pajaros
{
    public class Actividad
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Nombre { get; set; } = string.Empty;
    }
}
