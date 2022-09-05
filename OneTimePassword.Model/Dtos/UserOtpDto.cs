namespace OneTimePassword.Model.Dtos
{
    public class UserOtpDto
    {
        public string Code { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
