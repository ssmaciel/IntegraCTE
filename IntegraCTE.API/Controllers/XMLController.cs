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
        public async Task<ActionResult> Post([FromBody] string xml, [FromServices] UploadCTE uc)
        {
            //var valueBytes = System.Convert.FromBase64String(xml);

            //var ret = Encoding.UTF8.GetString(valueBytes);
            ArquivoDTO dto = new(xml);
            await uc.Execute(dto);
            return Ok(xml);
            //return Ok(ret);
        }
    }
}
