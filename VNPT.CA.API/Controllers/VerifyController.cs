using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VNPT.CA.API.Repository;

namespace VNPT.CA.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VerifyController : ControllerBase
    {
        private readonly IVerifyService _verifyService;
        public VerifyController(IVerifyService verifyService) 
        {
            _verifyService = verifyService;
        }
        [HttpPost]
        public ActionResult CmsVerify(string signeddata)
        {
                        
            return Ok(_verifyService.VerifyCMS(signeddata));
        }

    }
}
