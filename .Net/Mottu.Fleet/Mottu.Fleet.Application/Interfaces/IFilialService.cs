using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mottu.Fleet.Application.DTOs;

namespace Mottu.Fleet.Application.Interfaces
{
    public interface IFilialService
    {
        IEnumerable<FilialDTO> GetAll();
        FilialDTO GetById(int id);
        void Create(FilialDTO dto);
        void Update(int id, FilialDTO dto);
        void Delete(int id);
    }
}
