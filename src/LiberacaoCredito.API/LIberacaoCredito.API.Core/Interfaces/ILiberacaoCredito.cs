using LIberacaoCredito.API.Core.Enums;
using LIberacaoCredito.API.Core.Entities;
using System;

namespace LIberacaoCredito.API.Core.Interfaces
{
    public interface ILiberacaoCredito
    {
        ResultadoLiberacaoCredito ValidaLiberacaoCredito(double valorCredito,
                                    TipoCreditoEnum tipoCredito,
                                    int quantParcelas, DateTime dataPrimeiroVencimento);


    }
}
