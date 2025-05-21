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
    public class FilialService : IFilialService
    {
        private readonly MottuDbContext _context;
        private readonly IMapper _mapper;

        public FilialService(MottuDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<FilialDTO> GetAll() =>
            _mapper.Map<List<FilialDTO>>(_context.Filiais.ToList());

        public FilialDTO GetById(int id)
        {
            var entity = _context.Filiais.Find(id);
            return entity == null ? null : _mapper.Map<FilialDTO>(entity);
        }

        public void Create(FilialDTO dto)
        {
            var entity = _mapper.Map<Filial>(dto);
            _context.Filiais.Add(entity);
            _context.SaveChanges();
        }

        public void Update(int id, FilialDTO dto)
        {
            var entity = _context.Filiais.Find(id);
            if (entity == null) return;
            _mapper.Map(dto, entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.Filiais.Find(id);
            if (entity == null) return;
            _context.Filiais.Remove(entity);
            _context.SaveChanges();
        }
    }
}
