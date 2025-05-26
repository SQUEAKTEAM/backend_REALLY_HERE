namespace BusinessLogic;

public class AuthSettings
{
    public TimeSpan AccessTokenExpires { get; set; }
    public TimeSpan RefreshTokenExpires { get; set; }
    public string SecretKey { get; set; }
}
