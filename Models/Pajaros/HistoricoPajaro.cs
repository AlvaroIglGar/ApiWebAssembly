namespace ApiRestDespliegue.Models.Pajaros
{
    using MongoDB.Bson;
    using MongoDB.Bson.Serialization.Attributes;
    using System;

    public class HistoricoPajaro
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        // Referencia al pájaro
        [BsonRepresentation(BsonType.ObjectId)]
        public string PajaroId { get; set; }

        [BsonRepresentation(BsonType.Double)]
        public double Peso { get; set; }

        public string MomentoDelDia { get; set; }

        [BsonRepresentation(BsonType.Double)]
        public double CantidadComida { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime FechaRegistro { get; set; }
    }

}
