using AutoMapper;
using IntegraCTE.Core.UseCases;
using IntegraCTE.Core.ValidationMessages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IntegraCTE.API.Controllers
{
    [Route("api/cte")]
    [ApiController]
    public class CTEController : ControllerBase
    {
        private readonly IMapper _mapper;
        protected readonly IValidationMessage _validationMessage;

        public CTEController(IMapper mapper, IValidationMessage validationMessage)
        {
            _mapper = mapper;
            _validationMessage = validationMessage;
        }

        [HttpGet("ultimos")]
        public async Task<ActionResult> Get([FromServices] BuscarCTEs uc)
        {
            var retorno = await uc.Execute();
            return Ok(retorno);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get([FromRoute]Guid id)
        {
            await Task.Delay(1000);
            return Ok();
        }
    }
}
