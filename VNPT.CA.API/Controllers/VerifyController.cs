using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VNPT.CA.API.Helper;
using VNPT.CA.API.Model;
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
        public ActionResult CmsVerify(CmsVerifyRequest verifyRequest)
        {

            if (!Licenses.CheckLicense(verifyRequest.licenseKey))
            {
                return Unauthorized(new {
                    Status = false,
                    Message = "License key is invalid"
                });
            }
            return Ok(new
            {
                Status = true,
                Data = _verifyService.VerifyCMS(verifyRequest.signeddata),
                Message = "Verify success"
            });
        }

    }
}
