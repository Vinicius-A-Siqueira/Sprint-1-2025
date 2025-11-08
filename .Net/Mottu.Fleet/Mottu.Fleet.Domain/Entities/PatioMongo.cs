using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mottu.Fleet.Domain.Entities
{
    public class PatioMongo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("nome")]
        public string Nome { get; set; } = null!;

        [BsonElement("endereco")]
        public EnderecoMongo Endereco { get; set; } = null!;

        [BsonElement("capacidade")]
        public int Capacidade { get; set; }

        [BsonElement("vagasOcupadas")]
        public int VagasOcupadas { get; set; }

        [BsonElement("ativo")]
        public bool Ativo { get; set; }

        [BsonElement("dataCadastro")]
        public DateTime DataCadastro { get; set; }

        [BsonElement("dataAtualizacao")]
        public DateTime? DataAtualizacao { get; set; }
    }

    public class EnderecoMongo
    {
        [BsonElement("rua")]
        public string Rua { get; set; } = null!;

        [BsonElement("numero")]
        public string Numero { get; set; } = null!;

        [BsonElement("bairro")]
        public string Bairro { get; set; } = null!;

        [BsonElement("cidade")]
        public string Cidade { get; set; } = null!;

        [BsonElement("estado")]
        public string Estado { get; set; } = null!;

        [BsonElement("cep")]
        public string Cep { get; set; } = null!;
    }
}
