using System.Windows.Input;
using VNPTSdkValidatorNetCore.License;
using VNPTSdkValidatorNetCore.Signatures;

namespace VNPT.CA.API.Helper
{
    public static class Utilities
    {
        public static bool CheckLicense(string lic)
        {
            string mes = string.Empty;
            bool result = false;
            try
            {
                if (!string.IsNullOrEmpty(lic))
                {
                    result = VNPTSdkValidatorNetCoreLicense.SetLicenseKey(lic, out mes);
                }
                return result;
            }
            catch (Exception)
            {
                mes = "Lỗi không xác định";
                return false;
            }
           
        }

        public static string ShowMessage(VERIFY_RESULT code)
        {
            switch (code)
            {
                case VERIFY_RESULT.vefBadInput:
                    return "Dữ liệu không hợp lệ";
                    break;
                case VERIFY_RESULT.vefCantGetRef:
                    return  "Không thể lấy tham chiếu tới dữ liệu xml";
                    break;
                case VERIFY_RESULT.vefCertNotGood:
                    return "Chứng thư dùng để ký không được tin tưởng";
                    break;
                case VERIFY_RESULT.vefCheckCertFailed:
                    return  "Không kiểm tra được hiệu lực chứng thư";
                    break;
                case VERIFY_RESULT.vefIgnore:
                    return  "Bỏ qua kiểm tra hiệu lực chứng thư";
                    break;
                case VERIFY_RESULT.vefNotFoundBase64CertCorrespond:
                    return  "Xml: không lấy được base64 của chứng thư số";
                    break;
                case VERIFY_RESULT.vefNotFoundCertSigning:
                    return  "Không lấy được thông tin chứng thư số sử dụng để ký";
                    break;
                case VERIFY_RESULT.vefSigInValid:
                    return  "Chữ ký không hợp lệ";
                    break;
                case VERIFY_RESULT.vefSigSucess:
                    return "Dữ liệu hợp lệ";
                    break;
                case VERIFY_RESULT.vefValidateFailed:
                    return "Validate dữ liệu không thành công";               
            }
            return "Lỗi không xác định";
        }
    }

}
