namespace OneAuth.BlazorWebAssemblyDemo.Client.Models;

public class LoginViewModel
{
    public string? UserName { get; set; }
    public string? Password { get; set; }
}

public class UserInfo
{
    public string? UserName { get; set; }
    public IEnumerable<string> Roles { get; set; } = [];
    public DateTime? Birthday { get; set; }
}