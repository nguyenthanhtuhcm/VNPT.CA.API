using Microsoft.OpenApi.Extensions;
using System.Security.Cryptography;
using System.Security;
using VNPT.CA.API.Model;
using VNPTSdkValidatorNetCore.Certificate;
using VNPTSdkValidatorNetCore.Common;
using VNPTSdkValidatorNetCore.Signatures;
using VNPT.CA.API.Helper;

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
            try
            {
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
                    item.code = resultList[i].code;
                    item.certificate = resultList[i].certificate;
                    item.signingTime = resultList[i].signingTime;
                    verifyResultModel.signatures.Add(item);
                }
                verifyResultModel.status = validate;
                verifyResultModel.message = Utilities.ShowMessage(resultList[0].code);

            }
            catch (Exception ex)
            {
                verifyResultModel.status = false;
                verifyResultModel.message = ex.Message;
            }

            return verifyResultModel;
        }

        public VerifyResultModel VerifyXml(string signeddata)
        {

            VerifyResultModel verifyResultModel = new VerifyResultModel();
            verifyResultModel.TranID = Guid.NewGuid().ToString();
            try
            {
                CryptoConfig.AddAlgorithm(typeof(RSAPKCS1SHA256SignatureDescription),
                     "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256"
                 );

                CryptoConfig.AddAlgorithm(typeof(RSAPKCS1SHA1SignatureDescription),
                    "http://www.w3.org/2000/09/xmldsig#rsa-sha1"
                );
                var xml = new Xml();
                byte[] dataBytes = FileUtils.ReadFileToByte(@"D:\Project\smartCA\test_signed.xml");
                var res = xml.Verify(dataBytes, null, null, null, VALIDATE_CERT_OPTION.USE_OCSP);
                Console.WriteLine("Number of Signatures: " + res.Count);
                if (res.Count > 0)
                {
                    verifyResultModel.signatures = new List<SignServerVerifyResultModel>();
                }
                bool validate = true;

                for (int i = 0; i < res.Count; i++)
                {
                    validate = validate & res[i].signatureStatus;
                    SignServerVerifyResultModel item = new SignServerVerifyResultModel();
                    item.signatureIndex = res[i].signatureIndex;
                    item.signatureStatus = res[i].signatureStatus;
                    item.certStatus = res[i].certStatus.ToString();
                    item.code = res[i].code;
                    item.certificate = res[i].certificate;
                    item.signingTime = res[i].signingTime;
                    verifyResultModel.signatures.Add(item);
                }
                verifyResultModel.status = validate;
                verifyResultModel.message = Utilities.ShowMessage(res[0].code);

            }
            catch (Exception ex)
            {
                verifyResultModel.status = false;
                verifyResultModel.message = ex.Message;
            }

            return verifyResultModel;
        }

        public VerifyResultModel VerifyPdf(string signeddataBase64)
        {

            VerifyResultModel verifyResultModel = new VerifyResultModel();
            verifyResultModel.TranID = Guid.NewGuid().ToString();
            try
            {

                //byte[] signeddata = Convert.FromBase64String(signeddataBase64);
                byte[] signeddata = FileUtils.ReadFileToByte(@"D:\Project\smartCA\test\pdf\Travel Reservation August 15 for MR THANH TU NGUYEN_signed.pdf");
                CertificateHandle certHandle = new CertificateHandle();
                //certHandle.SetCertIssuer(rootCA);
                //certHandle.SetCertIssuer(vnptCert);
                var pdf = new Pdf(certHandle);
                var res = pdf.Verify(signeddata, null, null, null, VALIDATE_CERT_OPTION.USE_OCSP);
                Console.WriteLine("Number of Signatures: " + res.Count);
                if (res.Count > 0)
                {
                    verifyResultModel.signatures = new List<SignServerVerifyResultModel>();
                }
                bool validate = true;

                for (int i = 0; i < res.Count; i++)
                {
                    validate = validate & res[i].signatureStatus;
                    SignServerVerifyResultModel item = new SignServerVerifyResultModel();
                    item.signatureIndex = res[i].signatureIndex;
                    item.signatureStatus = res[i].signatureStatus;
                    item.certStatus = res[i].certStatus.ToString();
                    item.code = res[i].code;
                    item.certificate = res[i].certificate;
                    item.signingTime = res[i].signingTime;
                    verifyResultModel.signatures.Add(item);
                }
                verifyResultModel.status = validate;
                verifyResultModel.message = Utilities.ShowMessage(res[0].code);

            }
            catch (Exception ex)
            {
                verifyResultModel.status = false;
                verifyResultModel.message = ex.Message;
            }

            return verifyResultModel;
        }
    }
}
