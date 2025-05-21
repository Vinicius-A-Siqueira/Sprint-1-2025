using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mottu.Fleet.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Mottu.Fleet.Domain.Entities;


namespace Mottu.Fleet.Application.Repository


{
    public class FilialRepository
    {
        private readonly MottuDbContext _context;

        public FilialRepository(MottuDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Filial> GetAll() =>
            _context.Filiais.AsNoTracking().ToList();

        public Filial GetById(int id) =>
            _context.Filiais.AsNoTracking().FirstOrDefault(f => f.Id == id);

        public void Add(Filial filial)
        {
            _context.Filiais.Add(filial);
            _context.SaveChanges();
        }

        public void Update(Filial filial)
        {
            _context.Filiais.Update(filial);
            _context.SaveChanges();
        }

        public void Delete(Filial filial)
        {
            _context.Filiais.Remove(filial);
            _context.SaveChanges();
        }
    }
}
