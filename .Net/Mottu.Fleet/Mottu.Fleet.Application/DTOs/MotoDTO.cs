using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.Fleet.Application.DTOs;

public class MotoDTO
{
    public int Id { get; set; }
    public string Placa { get; set; } = null!;
    public DateTime AnoFabricacao { get; set; }
    public int PatioId { get; set; }
    public int PatioFilialIdFilial { get; set; }

}

