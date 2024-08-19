namespace AeroclubeManager.Web.Models.Account
{
    public class ConfirmationAccountModelView
    {
        public ConfirmationAccountModelView(string userName, string email, bool isSucceed) {
        UserName = userName;
        Email = email;
        IsSucceed = isSucceed;
        }

        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsSucceed { get; set; } = false;
        public DateTime DateConfirmated { get; set; } = DateTime.Now;
    }
}
