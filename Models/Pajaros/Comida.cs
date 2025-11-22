using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiRestDespliegue.Models.Pajaros
{
    public class Comida
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string TipoId { get; set; }

        public string TipoNombre { get; set; } = string.Empty;

        [BsonRepresentation(BsonType.Double)]
        public double Peso { get; set; }
    }
}
