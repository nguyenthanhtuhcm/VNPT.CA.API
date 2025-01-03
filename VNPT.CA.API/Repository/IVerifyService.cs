using System.Security.Cryptography;
using System.Text;

namespace VNPT.CA.API.Repository
{
    public interface IVerifyService
    {
        public bool VerifyCMS(string data);
       
    }
}
