using Birth_Certificate_Request.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Buffers.Text;

namespace Birth_Certificate_Request.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirthCertificateController : ControllerBase
    {

        [HttpPost]
        public ActionResult<BirthCertificate> PostBirthCertificate([FromBody] BirthCertificate data)
        {

            if(Base64.IsValid(data.ImageData))
            {
                return Ok(true);
            }
            
                return Ok(data);

        }
    }
}
