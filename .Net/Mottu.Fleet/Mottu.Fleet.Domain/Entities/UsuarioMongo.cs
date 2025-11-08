using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mottu.Fleet.Domain.Entities
{
    public class UsuarioMongo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("username")]
        public string Username { get; set; } = null!;

        [BsonElement("password")]
        public string Password { get; set; } = null!;

        [BsonElement("perfil")]
        public string Perfil { get; set; } = null!;

        [BsonElement("fullName")]
        public string? FullName { get; set; }

        [BsonElement("email")]
        public string? Email { get; set; }

        [BsonElement("phone")]
        public string? Phone { get; set; }

        [BsonElement("status")]
        public int Status { get; set; }

        [BsonElement("ativo")]
        public bool Ativo { get; set; }

        [BsonElement("dataCadastro")]
        public DateTime DataCadastro { get; set; }

        [BsonElement("dataAtualizacao")]
        public DateTime? DataAtualizacao { get; set; }

        [BsonElement("ultimoAcesso")]
        public DateTime? UltimoAcesso { get; set; }
    }
}
