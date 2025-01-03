using System.Windows.Input;
using VNPTSdkValidatorNetCore.License;

namespace VNPT.CA.API.Helper
{
    public static class Licenses
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
    }
}
