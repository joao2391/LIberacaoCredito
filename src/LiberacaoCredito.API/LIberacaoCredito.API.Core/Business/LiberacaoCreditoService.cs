using LIberacaoCredito.API.Core.Entities;
using LIberacaoCredito.API.Core.Enums;
using LIberacaoCredito.API.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using LIberacaoCredito.API.Core.Utils;

namespace LIberacaoCredito.API.Core.Business
{
    public class LiberacaoCreditoService : ILiberacaoCredito
    {
        
        private IEnumerable<int> qtdParcelasMaximas;
        private double valorTotal;
        private double valorJuros;

        public LiberacaoCreditoService()
        {
            qtdParcelasMaximas = Enumerable.Range(5, 68);            
        }

        public ResultadoLiberacaoCredito ValidaLiberacaoCredito(double valorCredito,
                                                                TipoCreditoEnum tipoCredito,
                                                                int quantParcelas,
                                                                DateTime dataPrimeiroVencimento)
        {
            if (IsDataPrimVencValid(dataPrimeiroVencimento)
                && IsQuantParcValid(quantParcelas)
                && IsValorCreditoValid(valorCredito))
            {
                switch (tipoCredito)
                {
                    case TipoCreditoEnum.CreditoDireto:

                        CalculaCreditoDireto(valorCredito, quantParcelas);
                        break;

                    case TipoCreditoEnum.CreditoConsignado:

                        CalculaCreditoConsignado(valorCredito, quantParcelas);
                        break;

                    case TipoCreditoEnum.CreditoPJ:

                        if (HasPJValorMin(valorCredito))
                        {
                            CalculaCreditoPJ(valorCredito, quantParcelas);
                        }
                         break;

                    case TipoCreditoEnum.CreditoPF:

                        CalculaCreditoPF(valorCredito, quantParcelas);
                        break;

                    case TipoCreditoEnum.CreditoImob:

                        CalculaCreditoImob(valorCredito, quantParcelas);
                        break;
                    default:
                        break;
                }

                return new ResultadoLiberacaoCredito {StatusCredito = StatusCreditoEnum.Aprovado.ToString(),
                                                      ValorJuros = valorJuros, ValorTotal = valorTotal};
            }
            else
            {
                return new ResultadoLiberacaoCredito {StatusCredito = StatusCreditoEnum.Recusado.ToString(),
                                                      ValorJuros = 0, ValorTotal = 0 };
            }            
        }

        #region Private Methods

        private bool IsDataPrimVencValid(DateTime data)
        {
            DateTime dataAtual = DateTime.Now;
            bool isOK = data >= dataAtual.AddDays(15) && data < dataAtual.AddDays(40);

            return isOK;
        }

        private bool IsValorCreditoValid(double valorCredito)
        {
            bool isOk = valorCredito <= Constants.VALOR_MAXIMO
                        && !double.IsNegative(valorCredito) ? true : false;

            return isOk;
        }

        private bool IsQuantParcValid(int quantParcelas)
        {            
            bool isOk = qtdParcelasMaximas.Contains(quantParcelas) ? true : false;

            return isOk;
        }        

        private bool HasPJValorMin(double valorCredito)
        {
            bool isOK = valorCredito >= Constants.VALOR_MIN_PJ ? true : false;

            return isOK;
        }

        private void CalculaCreditoDireto(double valorCredito, int qtdParcelas)
        {
            valorJuros += ( Constants.DOIS_PORCENTO * valorCredito) * qtdParcelas;

            valorTotal += (valorCredito + valorJuros);
        }

        private void CalculaCreditoConsignado(double valorCredito, int qtdParcelas)
        {            
            valorJuros += (Constants.UM_PORCENTO * valorCredito) * qtdParcelas;

            valorTotal += (valorCredito + valorJuros);
        }

        private void CalculaCreditoPJ(double valorCredito, int qtdParcelas)
        {
            valorJuros += (Constants.CINCO_PORCENTO * valorCredito) * qtdParcelas;

            valorTotal += (valorCredito + valorJuros);
            
        }

        private void CalculaCreditoPF(double valorCredito, int qtdParcelas)
        {
            valorJuros += (Constants.TRES_PORCENTO * valorCredito) * qtdParcelas;

            valorTotal += (valorCredito + valorJuros);
        }

        private void CalculaCreditoImob(double valorCredito, int qtdParcelas)
        {  
            
            valorJuros += (Constants.NOVE_PCT_ANO * valorCredito) * qtdParcelas;

            valorTotal += (valorCredito + valorJuros);
        }


        #endregion
    }
}
