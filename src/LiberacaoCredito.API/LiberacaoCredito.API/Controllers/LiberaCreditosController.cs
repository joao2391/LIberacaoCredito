using System;
using Microsoft.AspNetCore.Mvc;
using LiberacaoCredito.API.Models;
using LIberacaoCredito.API.Core.Interfaces;

namespace LiberacaoCredito.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class LiberaCreditosController : ControllerBase
    {
        private ILiberacaoCredito _liberacaoCredito;

        public LiberaCreditosController(ILiberacaoCredito liberacaoCredito)
        {
            _liberacaoCredito = liberacaoCredito;
        }
        
        [HttpGet("getLiberacaoCredito")]
        [Produces("application/json")]
        public IActionResult GetLiberacaoCreditoAsync([FromBody] LiberacaoCreditoModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var objLiberCred = _liberacaoCredito.ValidaLiberacaoCredito(model.ValorCredito, model.TipoCredito,
                                                        model.QuantidadeParcelas, model.DataPrimVencimento);


                    return Ok(objLiberCred);
                }

                return NotFound();
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
