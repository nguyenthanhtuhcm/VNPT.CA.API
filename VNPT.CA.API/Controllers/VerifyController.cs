using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;
        public VerifyController(IVerifyService verifyService, IConfiguration configuration)
        {
            _verifyService = verifyService;
            _configuration = configuration;
        }
        [HttpPost("/CmsVerify")]
        public ActionResult CmsVerify(CmsVerifyRequest verifyRequest)
        {         
            if (String.IsNullOrEmpty(_configuration["LicenseKey"]) || !Utilities.CheckLicense(_configuration["LicenseKey"]))
            {
                return Unauthorized(new {
                    Status = false,
                    Message = "License key không hợp lệ"
                });
            }
            return Ok(new
            {
                Status = true,
                Data = _verifyService.VerifyCMS(verifyRequest.signeddata),
                Message = "Đã thực hiện xác thực"
            });
        }
        [HttpPost("/XmlVerify")]
        public ActionResult XmlVerify(XmlVerifyRequest verifyRequest)
        {

            if (String.IsNullOrEmpty(_configuration["LicenseKey"]) || !Utilities.CheckLicense(_configuration["LicenseKey"]))
            {
                return Unauthorized(new
                {
                    Status = false,
                    Message = "License key không hợp lệ"
                });
            }
            return Ok(new
            {
                Status = true,
                Data = _verifyService.VerifyXml(verifyRequest.signeddata),
                Message = "Đã thực hiện xác thực"
            });
        }

        [HttpPost("/OfficeVerify")]
        public ActionResult OfficeVerify(OfficeVerifyRequest verifyRequest)
        {

            if (String.IsNullOrEmpty(_configuration["LicenseKey"]) || !Utilities.CheckLicense(_configuration["LicenseKey"]))
            {
                return Unauthorized(new
                {
                    Status = false,
                    Message = "License key không hợp lệ"
                });
            }
            return Ok(new
            {
                Status = true,
                Data = _verifyService.VerifyOffice(verifyRequest.signeddata),
                Message = "Đã thực hiện xác thực"
            });
        }

        [HttpPost("/PdfVerify")]
        public ActionResult PdfVerify(PdfVerifyRequest verifyRequest)
        {

            if (String.IsNullOrEmpty(_configuration["LicenseKey"]) || !Utilities.CheckLicense(_configuration["LicenseKey"]))
            {
                return Unauthorized(new
                {
                    Status = false,
                    Message = "License key không hợp lệ"
                });
            }
            return Ok(new
            {
                Status = true,
                Data = _verifyService.VerifyPdf(verifyRequest.signeddata),
                Message = "Đã thực hiện xác thực"
            });
        }

    }
}
