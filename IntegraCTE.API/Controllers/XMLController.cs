using AutoMapper;
using IntegraCTE.Core.DTO;
using IntegraCTE.Core.UseCases;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IntegraCTE.API.Controllers
{
    [Route("api/xml")]
    [ApiController]
    public class XMLController : ControllerBase
    {
        private readonly IMapper _mapper;
        public XMLController(IMapper mapper)
        {
            this._mapper = mapper;
        }
        //[HttpPost, DisableRequestSizeLimit]
        // POST api/<XMLController>
        [HttpPost("upload")]
        public async Task<ActionResult> Post([FromBody] string xml, [FromServices] UploadCTE ucUploadXML, [FromServices] ProcessarXMLCTE ucProcessarXML)
        {
            ArquivoDTO dto = new(xml);
            await ucUploadXML.Execute(dto);
            await ucProcessarXML.Execute(dto.Id);
            return Ok(xml);
        }
    }
}
