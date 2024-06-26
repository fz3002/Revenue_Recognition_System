using Revenue_Recognition_System.Enums;

namespace Revenue_Recognition_System.Models;

public class User
{
    public int IdUser { get; set; }
    public string Login { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
    public string Salt { get; set; }
    public string RefreshToken { get; set; }
    public DateTime? RefreshTokenExp { get; set; }
}