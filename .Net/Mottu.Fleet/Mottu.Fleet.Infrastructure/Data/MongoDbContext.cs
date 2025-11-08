using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Mottu.Fleet.Domain.Entities;
using Mottu.Fleet.Infrastructure.Configuration;

namespace Mottu.Fleet.Infrastructure.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        private readonly MongoDbSettings _settings;

        public MongoDbContext(IOptions<MongoDbSettings> settings)
        {
            _settings = settings.Value;
            var client = new MongoClient(_settings.ConnectionString);
            _database = client.GetDatabase(_settings.DatabaseName);
            CreateIndexes();
        }

        public IMongoCollection<MotoMongo> Motos =>
            _database.GetCollection<MotoMongo>(_settings.Collections.Motos);

        public IMongoCollection<PatioMongo> Patios =>
            _database.GetCollection<PatioMongo>(_settings.Collections.Patios);

        public IMongoCollection<UsuarioMongo> Usuarios =>
            _database.GetCollection<UsuarioMongo>(_settings.Collections.Usuarios);

        private void CreateIndexes()
        {
            // Índice único para placa
            var motoIndexKeys = Builders<MotoMongo>.IndexKeys.Ascending(m => m.Placa);
            var motoIndexOptions = new CreateIndexOptions { Unique = true };
            Motos.Indexes.CreateOne(new CreateIndexModel<MotoMongo>(motoIndexKeys, motoIndexOptions));

            // Índice único para username
            var usuarioIndexKeys = Builders<UsuarioMongo>.IndexKeys.Ascending(u => u.Username);
            var usuarioIndexOptions = new CreateIndexOptions { Unique = true };
            Usuarios.Indexes.CreateOne(new CreateIndexModel<UsuarioMongo>(usuarioIndexKeys, usuarioIndexOptions));
        }

        public async Task<bool> TestConnectionAsync()
        {
            try
            {
                await _database.RunCommandAsync((Command<MongoDB.Bson.BsonDocument>)"{ping:1}");
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
