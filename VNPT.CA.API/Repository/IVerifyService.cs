using System.Security.Cryptography;
using System.Text;
using VNPT.CA.API.Model;

namespace VNPT.CA.API.Repository
{
    public interface IVerifyService
    {
        public VerifyResultModel VerifyCMS(string data);
        public VerifyResultModel VerifyXml(string data);
        public VerifyResultModel VerifyPdf(string data);
        public VerifyResultModel VerifyOffice(string data);

    }
}
