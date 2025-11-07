using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiRestDespliegue.Models.Login
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Email")]
        public string Email { get; set; } = string.Empty;

        [BsonElement("PasswordHash")]
        public string PasswordHash { get; set; } = string.Empty;

        [BsonElement("Nombre")]
        public string Nombre { get; set; } = string.Empty;

        [BsonElement("Rol")]
        public string Rol { get; set; } = "User";

        [BsonElement("FechaRegistro")]
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        [BsonElement("Activo")]
        public bool Activo { get; set; } = true;
    }
}
