using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Mottu.Fleet.Domain.Entities;
using System.ComponentModel.DataAnnotations;


namespace Mottu.Fleet.Domain.Entities
{
    [Table("PATIO")]
    public class Patio
    {
        [Key]
        [Column("ID_PATIO")]
        public int Id { get; set; }

        [Column("NOME")]
        public string Nome { get; set; } = string.Empty;

        [Column("LOCALIZACAO")]
        public string Localizacao { get; set; } = string.Empty;

        [Column("LARGURA")]
        public decimal Largura { get; set; }

        [Column("COMPRIMENTO")]
        public decimal Comprimento { get; set; }

        [Column("AREA_TOTAL")]
        public decimal AreaTotal { get; set; }

        [Column("FILIAL_ID_FILIAL")]
        public int FilialId { get; set; }

        [ForeignKey("FilialId")]
        public Filial Filial { get; set; } = null!;

        public ICollection<Moto> Motos { get; set; } = new List<Moto>();

        public Patio(string nome, string localizacao, decimal largura, decimal comprimento, decimal areaTotal, int filialId)
        {
            Nome = nome;
            Localizacao = localizacao;
            Largura = largura;
            Comprimento = comprimento;
            AreaTotal = areaTotal;
            FilialId = filialId;
        }

        protected Patio() { }
    }
}