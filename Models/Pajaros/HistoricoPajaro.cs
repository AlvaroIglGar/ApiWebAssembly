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

        // Referencia a la actividad
        [BsonRepresentation(BsonType.ObjectId)]
        public string ActividadId { get; set; }

        public string MomentoDelDia { get; set; }

        [BsonRepresentation(BsonType.Double)]
        public double PesoAntesVuelo { get; set; }

        [BsonRepresentation(BsonType.Double)]
        public double PesoDespuesVuelo { get; set; }

        [BsonRepresentation(BsonType.Double)]
        public double CantidadComida { get; set; }

        // NUEVO: Lista de comidas usadas ese día
        public List<Comida> AlimentacionUsada { get; set; } = new();

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime FechaRegistro { get; set; }
    }


    //public class HistoricoPajaro
    //{
    //    [BsonId]
    //    [BsonRepresentation(BsonType.ObjectId)]
    //    public string Id { get; set; }

    //    // Referencia al pájaro
    //    [BsonRepresentation(BsonType.ObjectId)]
    //    public string PajaroId { get; set; }

    //    [BsonRepresentation(BsonType.Double)]
    //    public double PesoAntesVuelo { get; set; }

    //    [BsonRepresentation(BsonType.Double)]
    //    public double PesoDespuesVuelo { get; set; }

    //    public string MomentoDelDia { get; set; }

    //    [BsonRepresentation(BsonType.Double)]
    //    public double CantidadComida { get; set; }

    //    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
    //    public DateTime FechaRegistro { get; set; }
    //}

}
