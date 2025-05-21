using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.Fleet.Application.DTOs;

public class CreateMotoDTO
{
    public string Placa { get; set; }
    public int PatioId { get; set; }
    public int PatioFilialIdFilial { get; set; }
    public DateTime AnoFabricacao { get; set; }
}

