using LIberacaoCredito.API.Core.Enums;
using System;


namespace LiberacaoCredito.API.Models
{
    public class LiberacaoCreditoModel
    {
        public double ValorCredito { get; set; }

        public TipoCreditoEnum TipoCredito { get; set; }

        public int QuantidadeParcelas { get; set; }

        public DateTime DataPrimVencimento { get; set; }
    }
}
