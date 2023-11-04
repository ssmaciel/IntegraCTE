using AutoMapper;
using IntegraCTE.Core.DTO;
using IntegraCTE.Core.Model;
using IntegraCTE.Core.Repository;
using IntegraCTE.Core.UseCases;
using IntegraCTE.Core.ValidationMessages;
using Microsoft.AspNetCore.Mvc;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IntegraCTE.API.Controllers
{
    [Route("api/xml")]
    [ApiController]
    public class XMLController : ControllerBase
    {
        private readonly IMapper _mapper;
        protected readonly IValidationMessage _validationMessage;
        public XMLController(IMapper mapper, IValidationMessage validationMessage)
        {
            this._mapper = mapper;
            _validationMessage = validationMessage;
        }
        //[HttpPost, DisableRequestSizeLimit]
        // POST api/<XMLController>
        [HttpPost("upload")]
        public async Task<ActionResult> Post([FromBody] UploadRequest request, [FromServices] UploadCTE ucUploadXML, [FromServices] ProcessarXMLCTE ucProcessarXML)
        {
            try
            {
                byte[] bytes = Convert.FromBase64String(request.Xmlbase64);
                string xml = Encoding.UTF8.GetString(bytes);
                ArquivoDTO dto = new(xml);
                await ucUploadXML.Execute(dto);
                await ucProcessarXML.Execute(dto.Id);
                if (_validationMessage.HasValidation())
                {
                    return BadRequest(_validationMessage.GetValidations());

                }
                return Ok(dto.Id);
            }
            catch (Exception ex)
            {
                _validationMessage.AddMessage($"Erro não definido: {ex.Message}", ValidationType.Geral);
                return BadRequest(_validationMessage.GetValidations());
            }
        }

        [HttpGet]
        public async Task<CTEModel> Get([FromServices] IIntegraCTERepository repository, Guid id) => await repository.BuscarCTE(id);
    }

    public class UploadRequest
    {
        public string? Fatura { get; set; }
        public string Xmlbase64 { get; set; }
    }
}
