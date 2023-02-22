namespace MP.Lib.Core.SSOCode
{
    public class UserSSOInfo
    {
        public string UserId { get; set; }

        public string Token { get; set; }

        public string FullName { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public string Pwd { get; set; }

        public bool IsAgency { get; set; }

        public string BankAcc { get; set; }

        public string BankId { get; set; }
    }
}