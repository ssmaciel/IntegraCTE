using IntegraCTE.Core.DTO;
using IntegraCTE.Core.UseCases;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace IntegraCTE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntegracaoController : ControllerBase
    {

        //[HttpPost, DisableRequestSizeLimit]
        // POST api/<XMLController>
        [HttpPost("{id}")]
        public async Task<ActionResult> Post([FromServices] IntegrarCTE ucIntegrarCTE, [FromRoute] Guid id)
        {
            await ucIntegrarCTE.Execute(id);
            return Ok();
        }
    }
}
