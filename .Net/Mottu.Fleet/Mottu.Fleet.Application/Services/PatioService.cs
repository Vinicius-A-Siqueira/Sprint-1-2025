using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Mottu.Fleet.Application.DTOs;
using Mottu.Fleet.Application.Interfaces;
using Mottu.Fleet.Domain.Entities;
using Mottu.Fleet.Infrastructure.Data;

namespace Mottu.Fleet.Application.Services
{
    public class PatioService : IPatioService
    {
        private readonly MottuDbContext _context;
        private readonly IMapper _mapper;

        public PatioService(MottuDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<PatioDTO> GetAll() =>
            _mapper.Map<List<PatioDTO>>(_context.Patios.ToList());

        public PatioDTO GetById(int id)
        {
            var entity = _context.Patios.Find(id);
            return entity == null ? null : _mapper.Map<PatioDTO>(entity);
        }

        public void Create(PatioDTO dto)
        {
            var entity = _mapper.Map<Patio>(dto);
            _context.Patios.Add(entity);
            _context.SaveChanges();
        }

        public void Update(int id, PatioDTO dto)
        {
            var entity = _context.Patios.Find(id);
            if (entity == null) return;
            _mapper.Map(dto, entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.Patios.Find(id);
            if (entity == null) return;
            _context.Patios.Remove(entity);
            _context.SaveChanges();
        }
    }
}
