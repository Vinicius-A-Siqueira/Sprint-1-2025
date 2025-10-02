using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.Fleet.Domain.Entities;
public class Moto : BaseEntity
{
    [Required]
    [StringLength(20)]
    public string Placa { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    public string Modelo { get; set; } = string.Empty;

    [Required]
    public int PatioId { get; set; }

    [Range(2000, 2030)]
    public int Ano { get; set; } = DateTime.Now.Year;

    [StringLength(50)]
    public string? Cor { get; set; }

    [Range(0, int.MaxValue)]
    public int Quilometragem { get; set; } = 0;

    public MotoStatus Status { get; set; } = MotoStatus.Disponivel;

    public DateTime? UltimaManutencao { get; set; }

    public DateTime? ProximaManutencao { get; set; }

    [StringLength(500)]
    public string? Observacoes { get; set; }

    [StringLength(50)]
    public string? Chassi { get; set; }

    [StringLength(50)]
    public string? NumeroMotor { get; set; }
    public virtual Patio Patio { get; set; } = null!;
}