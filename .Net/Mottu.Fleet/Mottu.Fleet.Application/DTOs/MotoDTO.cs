using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.Fleet.Application.DTOs;
public class MotoDto
{
    public int Id { get; set; }
    public string Placa { get; set; } = string.Empty;
    public string Modelo { get; set; } = string.Empty;
    public int PatioId { get; set; }
    public string PatioNome { get; set; } = string.Empty;
    public int Ano { get; set; }
    public string? Cor { get; set; }
    public int Quilometragem { get; set; }
    public MotoStatus Status { get; set; }
    public string StatusDescricao { get; set; } = string.Empty;
    public DateTime? UltimaManutencao { get; set; }
    public DateTime? ProximaManutencao { get; set; }
    public string? Chassi { get; set; }
    public string? NumeroMotor { get; set; }
    public string? Observacoes { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public Dictionary<string, string> Links { get; set; } = new();
}

