namespace Mottu.Fleet.Domain.ValueObjects
{
    public class Placa
    {
        public string Valor { get; private set; }

        public Placa(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new ArgumentException("Placa não pode ser vazia.");

            // Validação formato placa brasileira (ABC1234 ou ABC1D23)
            if (!System.Text.RegularExpressions.Regex.IsMatch(valor, @"^[A-Z]{3}[0-9][A-Z0-9][0-9]{2}$"))
                throw new ArgumentException("Formato de placa inválido. Use formato ABC1234 ou ABC1D23.");

            Valor = valor.ToUpper();
        }

        public override bool Equals(object? obj)
        {
            return obj is Placa placa && Valor == placa.Valor;
        }

        public override int GetHashCode() => Valor.GetHashCode();

        public override string ToString() => Valor;
    }
}
