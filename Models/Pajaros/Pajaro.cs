namespace ApiRestDespliegue.Models.Pajaros
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System;

    public class Pajaro
    {
        [BsonId] // Marca este campo como el _id de MongoDB
        [BsonRepresentation(BsonType.ObjectId)] // Permite trabajar con string como ObjectId
        public string Id { get; set; }

        public string Nombre { get; set; }
        public string Especie { get; set; }
        public string Alimentacion { get; set; }
        public string Descripcion { get; set; }

        [BsonRepresentation(BsonType.Double)]
        public double CantidadComida { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime FechaCreacion { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime FechaNacimiento { get; set; }

        public string RutaImagen { get; set; }
    }

}
