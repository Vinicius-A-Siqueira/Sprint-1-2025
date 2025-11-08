using MongoDB.Driver;
using Mottu.Fleet.Domain.Entities;
using Mottu.Fleet.Infrastructure.Data;

namespace Mottu.Fleet.Infrastructure.Repositories
{
    public class PatioMongoRepository
    {
        private readonly IMongoCollection<PatioMongo> _collection;

        public PatioMongoRepository(MongoDbContext context)
        {
            _collection = context.Patios;
        }

        public async Task<PatioMongo> CreateAsync(PatioMongo patio)
        {
            patio.DataCadastro = DateTime.UtcNow;
            patio.VagasOcupadas = 0;
            patio.Ativo = true;
            await _collection.InsertOneAsync(patio);
            return patio;
        }

        public async Task<List<PatioMongo>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        public async Task<PatioMongo?> GetByIdAsync(string id)
        {
            return await _collection.Find(p => p.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<PatioMongo>> GetAtivosAsync()
        {
            return await _collection.Find(p => p.Ativo == true).ToListAsync();
        }

        public async Task<List<PatioMongo>> GetComVagasDisponiveisAsync()
        {
            return await _collection.Find(p => p.Ativo && p.VagasOcupadas < p.Capacidade).ToListAsync();
        }

        public async Task<bool> UpdateAsync(string id, PatioMongo patio)
        {
            patio.DataAtualizacao = DateTime.UtcNow;
            var result = await _collection.ReplaceOneAsync(p => p.Id == id, patio);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> IncrementarVagasOcupadasAsync(string id)
        {
            var update = Builders<PatioMongo>.Update
                .Inc(p => p.VagasOcupadas, 1)
                .Set(p => p.DataAtualizacao, DateTime.UtcNow);

            var result = await _collection.UpdateOneAsync(p => p.Id == id, update);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DecrementarVagasOcupadasAsync(string id)
        {
            var update = Builders<PatioMongo>.Update
                .Inc(p => p.VagasOcupadas, -1)
                .Set(p => p.DataAtualizacao, DateTime.UtcNow);

            var result = await _collection.UpdateOneAsync(p => p.Id == id, update);
            return result.ModifiedCount > 0;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _collection.DeleteOneAsync(p => p.Id == id);
            return result.DeletedCount > 0;
        }

        public async Task<bool> DesativarAsync(string id)
        {
            var update = Builders<PatioMongo>.Update
                .Set(p => p.Ativo, false)
                .Set(p => p.DataAtualizacao, DateTime.UtcNow);

            var result = await _collection.UpdateOneAsync(p => p.Id == id, update);
            return result.ModifiedCount > 0;
        }
    }
}
