using MongoDB.Driver;
using Mottu.Fleet.Domain.Entities;
using Mottu.Fleet.Infrastructure.Data;

namespace Mottu.Fleet.Infrastructure.Repositories
{
    public class UsuarioMongoRepository
    {
        private readonly IMongoCollection<UsuarioMongo> _collection;

        public UsuarioMongoRepository(MongoDbContext context)
        {
            _collection = context.Usuarios;
        }

        // CREATE
        public async Task<UsuarioMongo> CreateAsync(UsuarioMongo usuario)
        {
            usuario.DataCadastro = DateTime.UtcNow;
            usuario.Ativo = true;
            usuario.Status = 1;
            await _collection.InsertOneAsync(usuario);
            return usuario;
        }

        // READ (All)
        public async Task<List<UsuarioMongo>> GetAllAsync()
        {
            return await _collection.Find(_ => true).ToListAsync();
        }

        // READ (By ID)
        public async Task<UsuarioMongo?> GetByIdAsync(string id)
        {
            return await _collection.Find(u => u.Id == id).FirstOrDefaultAsync();
        }

        // READ (By Username)
        public async Task<UsuarioMongo?> GetByUsernameAsync(string username)
        {
            return await _collection.Find(u => u.Username == username).FirstOrDefaultAsync();
        }

        // READ (By Email)
        public async Task<UsuarioMongo?> GetByEmailAsync(string email)
        {
            return await _collection.Find(u => u.Email == email).FirstOrDefaultAsync();
        }

        // READ (By Perfil)
        public async Task<List<UsuarioMongo>> GetByPerfilAsync(string perfil)
        {
            return await _collection.Find(u => u.Perfil == perfil).ToListAsync();
        }

        // READ (Ativos)
        public async Task<List<UsuarioMongo>> GetAtivosAsync()
        {
            return await _collection.Find(u => u.Ativo == true).ToListAsync();
        }

        // UPDATE
        public async Task<bool> UpdateAsync(string id, UsuarioMongo usuario)
        {
            usuario.DataAtualizacao = DateTime.UtcNow;
            var result = await _collection.ReplaceOneAsync(u => u.Id == id, usuario);
            return result.ModifiedCount > 0;
        }

        // UPDATE Last Login
        public async Task<bool> AtualizarUltimoAcessoAsync(string id)
        {
            var update = Builders<UsuarioMongo>.Update
                .Set(u => u.UltimoAcesso, DateTime.UtcNow);

            var result = await _collection.UpdateOneAsync(u => u.Id == id, update);
            return result.ModifiedCount > 0;
        }

        // SOFT DELETE (Desativar)
        public async Task<bool> DesativarAsync(string id)
        {
            var update = Builders<UsuarioMongo>.Update
                .Set(u => u.Ativo, false)
                .Set(u => u.Status, 0)
                .Set(u => u.DataAtualizacao, DateTime.UtcNow);

            var result = await _collection.UpdateOneAsync(u => u.Id == id, update);
            return result.ModifiedCount > 0;
        }

        // DELETE
        public async Task<bool> DeleteAsync(string id)
        {
            var result = await _collection.DeleteOneAsync(u => u.Id == id);
            return result.DeletedCount > 0;
        }

        // EXISTS (By Username)
        public async Task<bool> ExistsByUsernameAsync(string username)
        {
            var count = await _collection.CountDocumentsAsync(u => u.Username == username);
            return count > 0;
        }

        // EXISTS (By Email)
        public async Task<bool> ExistsByEmailAsync(string email)
        {
            var count = await _collection.CountDocumentsAsync(u => u.Email == email);
            return count > 0;
        }
    }
}
