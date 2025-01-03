using System.Security.Cryptography;
using System.Text;
using VNPT.CA.API.Model;

namespace VNPT.CA.API.Repository
{
    public interface IVerifyService
    {
        public VerifyResultModel VerifyCMS(string data);
       
    }
}
