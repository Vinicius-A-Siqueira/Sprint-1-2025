using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mottu.Fleet.Application.DTOs;
public class PatioDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Endereco { get; set; } = string.Empty;
    public string? Cidade { get; set; }
    public string? Estado { get; set; }
    public string? Cep { get; set; }
    public int Capacidade { get; set; }
    public string? Telefone { get; set; }
    public string? Observacoes { get; set; }
    public int QuantidadeMotos { get; set; }
    public decimal TaxaOcupacao { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public bool IsActive { get; set; }
    public Dictionary<string, string> Links { get; set; } = new();
}
