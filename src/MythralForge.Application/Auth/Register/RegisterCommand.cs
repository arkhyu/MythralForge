public class RegisterCommand
{
    public string Email { get; set; }
    public string Password { get; set; }

    public RegisterCommand(string email, string password)
    {
        Email = email;
        Password = password;
    }
}
