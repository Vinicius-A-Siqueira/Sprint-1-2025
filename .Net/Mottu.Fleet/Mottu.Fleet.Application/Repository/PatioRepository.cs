using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Mottu.Fleet.Domain.Entities;
using Mottu.Fleet.Infrastructure.Data;

namespace Mottu.Fleet.Application.Repository
{
    public class PatioRepository
    {
        private readonly MottuDbContext _context;

        public PatioRepository(MottuDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Patio> GetAll() =>
            _context.Patios.AsNoTracking().ToList();

        public Patio? GetById(int idPatio, int filialId) =>
            _context.Patios.AsNoTracking()
                .FirstOrDefault(p => p.Id == idPatio && p.FilialId == filialId);

        public IEnumerable<Patio> GetByFilialId(int filialId) =>
            _context.Patios.AsNoTracking()
                .Where(p => p.FilialId == filialId)
                .ToList();

        public void Add(Patio patio)
        {
            _context.Patios.Add(patio);
            _context.SaveChanges();
        }

        public void Update(Patio patio)
        {
            _context.Patios.Attach(patio);
            _context.Entry(patio).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Delete(Patio patio)
        {
            _context.Patios.Attach(patio);
            _context.Patios.Remove(patio);
            _context.SaveChanges();
        }

        public void Delete(int idPatio, int filialId, int id)
        {
            var patio = new Patio("placeholder", "placeholder", 0, 0, 0, filialId)
            {
                Id = idPatio
            };

            _context.Patios.Attach(patio);
            _context.Patios.Remove(patio);
            _context.SaveChanges();
        }
    }
}
