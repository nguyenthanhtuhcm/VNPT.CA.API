using Microsoft.OpenApi.Extensions;
using VNPT.CA.API.Model;
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
        public VerifyResultModel VerifyCMS(string signeddata)
        {
            VerifyResultModel verifyResultModel = new VerifyResultModel();
            verifyResultModel.TranID = Guid.NewGuid().ToString();
            var resultList = cms.Verify(Convert.FromBase64String(signeddata), null, null, null, VALIDATE_CERT_OPTION.USE_OCSP);
            Console.WriteLine("Number of Signatures: " + resultList.Count);
            
            if (resultList.Count > 0)
            {
                verifyResultModel.signatures = new List<SignServerVerifyResultModel>();
            }      
            bool validate = true;
            for (int i = 0; i < resultList.Count; i++)
            {
                validate = validate & resultList[i].signatureStatus;
                SignServerVerifyResultModel item = new SignServerVerifyResultModel();             
                item.signatureIndex = resultList[i].signatureIndex;
                item.signatureStatus = resultList[i].signatureStatus;
                item.certStatus = resultList[i].certStatus.ToString();
                item.code = resultList[i].code.ToString();
                item.certificate = resultList[i].certificate;
                item.signingTime = resultList[i].signingTime;
                verifyResultModel.signatures.Add(item);
            }
            verifyResultModel.status = validate;
            if (validate)
            {
                verifyResultModel.message = "Verify success";
            }
            else
            {
                verifyResultModel.message = "Verify fail";
            }
            return verifyResultModel;
        }
    }
}
