using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace ApiCdb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CdbController : ControllerBase
    {       
        private readonly ILogger<CdbController> _logger;

        public CdbController(ILogger<CdbController> logger)
        {
            _logger = logger;
        }

        [HttpGet("/cdb")]
        public IActionResult ObterTaxaCDB()
        {
            _logger.LogInformation($"{nameof(ObterTaxaCDB)} Invocado");

            /// TODO: Criar caso de uso e obter valores do banco de dados
            var result = new Cdb
            {
                Cdi = 0.009M,
                TaxaBancaria = 1.08M
            };

            _logger.LogInformation($"Retornando taxa CDI: {result.Cdi} e Bancaria:{result.TaxaBancaria}");

            return Ok(result);
        }
    }
}
