using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.Fleet.Application.DTOs
{
    public class PatioDTO
    {
        public int Id { get; set; }
        public string Nome { get; private set; } = null!;
        public int FilialId { get; set; }
    }
}
