using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mottu.Fleet.Application.DTOs;

namespace Mottu.Fleet.Application.Interfaces
{
    public interface IPatioService
    {
        IEnumerable<PatioDTO> GetAll();
        PatioDTO GetById(int id);
        void Create(PatioDTO dto);
        void Update(int id, PatioDTO dto);
        void Delete(int id);
    }
}
