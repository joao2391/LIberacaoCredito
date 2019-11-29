namespace LIberacaoCredito.API.Core.Entities
{
    public class ResultadoLiberacaoCredito
    {
        public string StatusCredito { get; set; }

        public double ValorTotal { get; set; }

        public double ValorJuros { get; set; }
    }
}
