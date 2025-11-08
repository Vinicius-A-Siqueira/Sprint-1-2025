using MongoDB.Driver;
using Mottu.Fleet.Domain.Entities;
using Mottu.Fleet.Infrastructure.Data;

namespace Mottu.Fleet.Infrastructure.Repositories
{
    public class MotoMongoRepository
    {
        private readonly IMongoCollection<MotoMongo> _collection;

        public MotoMongoRepository(MongoDbContext context)
        {
            _collection = context.Motos;
        }

        // CREATE
        public async Task<MotoMongo> CreateAsync(MotoMongo moto)
        {
            moto.DataCadastro = DateTime.UtcNow;
            await _collection.InsertOneAsync(moto);
            return moto;
        }

        // READ (All)
        public async Task<List<MotoMongo>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        // READ (By ID)
        public async Task<MotoMongo?> GetByIdAsync(string id)
        {
            return await _collection.Find(m => m.Id == id).FirstOrDefaultAsync();
        }

        // READ (By Placa) - MÉTODO QUE FALTAVA
        public async Task<MotoMongo?> GetByPlacaAsync(string placa)
        {
            return await _collection.Find(m => m.Placa == placa).FirstOrDefaultAsync();
        }

        // READ (By PatioId) - MÉTODO QUE FALTAVA
        public async Task<List<MotoMongo>> GetByPatioIdAsync(string patioId)
        {
            return await _collection.Find(m => m.PatioId == patioId).ToListAsync();
        }

        // READ (By Status) - MÉTODO QUE FALTAVA
        public async Task<List<MotoMongo>> GetByStatusAsync(string status)
        {
            return await _collection.Find(m => m.Status == status).ToListAsync();
        }

        // UPDATE
        public async Task<bool> UpdateAsync(string id, MotoMongo moto)
        {
            moto.DataAtualizacao = DateTime.UtcNow;
            var result = await _collection.ReplaceOneAsync(m => m.Id == id, moto);
            return result.ModifiedCount > 0;
        }

        // DELETE
        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _collection.DeleteOneAsync(m => m.Id == id);
            return result.DeletedCount > 0;
        }

        // COUNT (By PatioId)
        public async Task<long> CountByPatioIdAsync(string patioId)
        {
            return await _collection.CountDocumentsAsync(m => m.PatioId == patioId);
        }

        // EXISTS (By Placa) - MÉTODO QUE FALTAVA
        public async Task<bool> ExistsByPlacaAsync(string placa)
        {
            var count = await _collection.CountDocumentsAsync(m => m.Placa == placa);
            return count > 0;
        }
    }
}
