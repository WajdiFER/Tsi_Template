namespace Tsi.Template.ViewModels.Common
{
    public class UserRegisterModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; } 
        public string Email { get; set; } 
        public string PhoneNumber { get; set; }
        public string Password { get; set; } 
        public string ConfirmPassword { get; set; } 
        public bool UserNameEnabled { get; set; }
    }
}
