using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mottu.Fleet.Domain.Entities;

[Table("FILIAL")]
public class Filial
{
    [Column("ID_FILIAL")]
    public int Id { get; set; } 
    [Column("NOME")]
    public string Nome { get; set; } = string.Empty;

    [Column("ENDERECO")]
    public string Endereco { get; set; } = string.Empty;

    public Filial() { }

    public Filial(string nome, string endereco)
    {
        Nome = nome;
        Endereco = endereco;
    }
}