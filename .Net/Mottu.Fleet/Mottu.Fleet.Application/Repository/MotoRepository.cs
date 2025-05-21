using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mottu.Fleet.Domain.Entities;
using Mottu.Fleet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Mottu.Fleet.Application.Repository
{
    public class MotoRepository
    {
        private readonly MottuDbContext _context;

        public MotoRepository(MottuDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Moto> GetAll() =>
            _context.Motos.AsNoTracking().ToList();

        public Moto GetById(int id) =>
            _context.Motos.AsNoTracking().FirstOrDefault(m => m.Id == id);

        public void Add(Moto moto)
        {
            _context.Motos.Add(moto);
            _context.SaveChanges();
        }

        public void Update(Moto moto)
        {
            _context.Motos.Update(moto);
            _context.SaveChanges();
        }

        public void Delete(Moto moto)
        {
            _context.Motos.Remove(moto);
            _context.SaveChanges();
        }
    }
}
