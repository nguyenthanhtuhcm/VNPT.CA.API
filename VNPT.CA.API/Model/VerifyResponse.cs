namespace VNPT.CA.API.Model
{
       // Verify response -----------------------------------------------
    public class VerifyResultModel
    {
        public string TranID { get; set; }
        public bool status { get; set; }
        public string message { get; set; }

        public List<SignServerVerifyResultModel> signatures { get; set; }
    }
    public class SignServerVerifyResultModel
    {
        public string signingTime { get; set; }
        public bool signatureStatus { get; set; }
        public string certStatus { get; set; }
        public string certificate { get; set; }
        public int signatureIndex { get; set; }
        public string code { get; set; }
    }
    // ---------------------------------------------------------------
}
