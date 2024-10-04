namespace ETHShop.Jwt;

public class JwtOptions
{
    public string SecretKey { get; set; }
    public int ExpiersHours { get; set; }
}
