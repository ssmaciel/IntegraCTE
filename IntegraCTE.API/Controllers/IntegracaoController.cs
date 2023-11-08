using IntegraCTE.Core.DTO;
using IntegraCTE.Core.UseCases;
using IntegraCTE.Core.ValidationMessages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace IntegraCTE.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IntegracaoController : ControllerBase
    {
        protected readonly IValidationMessage _validationMessage;

        public IntegracaoController(IValidationMessage validationMessage)
        {
            _validationMessage = validationMessage;
        }

        //[HttpPost, DisableRequestSizeLimit]
        // POST api/<XMLController>
        [HttpPost("{id}")]
        public async Task<ActionResult> Post([FromServices] IntegrarCTE ucIntegrarCTE, [FromRoute] Guid id)
        {
            await ucIntegrarCTE.Execute(id);
            if (_validationMessage.HasValidation())
            {
                return BadRequest(_validationMessage.GetValidations());

            }
            return Ok();
        }
    }
}
