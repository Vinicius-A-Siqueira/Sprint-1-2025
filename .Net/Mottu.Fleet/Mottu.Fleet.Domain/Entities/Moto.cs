using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mottu.Fleet.Domain.Exceptions;


namespace Mottu.Fleet.Domain.Entities
{
    [Table("MOTO")]
    public class Moto
    {
        [Column("ID_MOTO")]
        public int Id { get; private set; }

        [Column("PLACA")]
        public string Placa { get; private set; }

        [Column("ANO_FABRICACAO")]
        public DateTime AnoFabricacao { get; private set; }

        [Column("PATIO_ID_PATIO")]
        public int PatioId { get; private set; }

        [ForeignKey("PatioId")]
        public Patio Patio { get; private set; } = null!;

        [Column("PATIO_FILIAL_ID_FILIAL")]
        public int PatioFilialIdFilial { get; private set; }

        public Moto(string placa, int patioId, int patioFilialIdFilial, DateTime anoFabricacao)
        {
            if (string.IsNullOrWhiteSpace(placa))
                throw new DomainException("Placa inválida.");

            if (anoFabricacao == default)
                throw new DomainException("Ano de fabricação inválido.");

            Placa = placa;
            PatioId = patioId;
            PatioFilialIdFilial = patioFilialIdFilial;
            AnoFabricacao = anoFabricacao;
        }

        public void AtualizarPlaca(string novaPlaca)
        {
            if (string.IsNullOrWhiteSpace(novaPlaca))
                throw new DomainException("Placa inválida.");

            Placa = novaPlaca;
        }

        public void AtualizarAnoFabricacao(DateTime novoAno)
        {
            if (novoAno == default)
                throw new DomainException("Ano de fabricação inválido.");

            AnoFabricacao = novoAno;
        }

        public void AtualizarPatio(int novoPatioId, int novoPatioFilialIdFilial)
        {
            PatioId = novoPatioId;
            PatioFilialIdFilial = novoPatioFilialIdFilial;
        }
    }
}
