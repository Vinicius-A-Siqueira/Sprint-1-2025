using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mottu.Fleet.Domain.Entities
{
    public class MotoMongo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("placa")]
        public string Placa { get; set; } = null!;

        [BsonElement("modelo")]
        public string Modelo { get; set; } = null!;

        [BsonElement("ano")]
        public int Ano { get; set; }

        [BsonElement("status")]
        public string Status { get; set; } = null!; // "Disponivel", "EmUso", "Manutencao"

        [BsonElement("patioId")]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? PatioId { get; set; }

        [BsonElement("dataCadastro")]
        public DateTime DataCadastro { get; set; }

        [BsonElement("dataAtualizacao")]
        public DateTime? DataAtualizacao { get; set; }
    }
}
