using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.Fleet.Application.DTOs;
public class CreatePatioDto
{
    [Required]
    [StringLength(100)]
    public string Nome { get; set; } = string.Empty;

    [Required]
    [StringLength(255)]
    public string Endereco { get; set; } = string.Empty;

    [StringLength(100)]
    public string? Cidade { get; set; }

    [StringLength(2)]
    public string? Estado { get; set; }

    [StringLength(10)]
    public string? Cep { get; set; }

    [Range(1, int.MaxValue)]
    public int Capacidade { get; set; } = 100;

    [Phone]
    [StringLength(20)]
    public string? Telefone { get; set; }

    [StringLength(500)]
    public string? Observacoes { get; set; }
}