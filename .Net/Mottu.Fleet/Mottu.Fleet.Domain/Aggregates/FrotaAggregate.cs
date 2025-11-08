using Mottu.Fleet.Domain.Entities;

namespace Mottu.Fleet.Domain.Aggregates
{
    public class FrotaAggregate
    {
        public string Id { get; private set; }
        public string Nome { get; private set; }
        private List<MotoMongo> _motos = new();
        public IReadOnlyCollection<MotoMongo> Motos => _motos.AsReadOnly();

        public FrotaAggregate(string id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public void AdicionarMoto(MotoMongo moto)
        {
            if (_motos.Any(m => m.Placa == moto.Placa))
                throw new InvalidOperationException($"Moto com placa {moto.Placa} já existe na frota.");

            _motos.Add(moto);
        }

        public void RemoverMoto(string motoId)
        {
            var moto = _motos.FirstOrDefault(m => m.Id == motoId);
            if (moto != null)
                _motos.Remove(moto);
        }

        public int TotalMotos() => _motos.Count;

        public int MotosDisponiveis() => _motos.Count(m => m.Status == "Disponivel");
    }
}
