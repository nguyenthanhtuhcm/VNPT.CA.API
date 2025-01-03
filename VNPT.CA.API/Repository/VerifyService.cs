using VNPTSdkValidatorNetCore.Certificate;
using VNPTSdkValidatorNetCore.Signatures;

namespace VNPT.CA.API.Repository
{
    public class VerifyService : IVerifyService
    {
        private static Cms cms = null;
        public VerifyService() 
        {
            cms = new Cms();
        }
        public bool VerifyCMS(string signeddata)
        {
            var resultList = cms.Verify(Convert.FromBase64String(signeddata), null, null, null, VALIDATE_CERT_OPTION.USE_OCSP);
            Console.WriteLine("Number of Signatures: " + resultList.Count);

            bool validate = true;
            for (int i = 0; i < resultList.Count; i++)
            {
                validate = validate & resultList[i].signatureStatus;
                Console.WriteLine("--------------");
                Console.WriteLine("Signature index " + resultList[i].signatureIndex);
                Console.WriteLine("Signature status (integrity): " + resultList[i].signatureStatus);
                Console.WriteLine("Certificate status: " + resultList[i].certStatus);
                Console.WriteLine("Result code: " + resultList[i].code);
            }
            return false;
        }
    }
}
