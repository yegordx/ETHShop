using ETHShop.Interfaces;
namespace ETHShop.PassWordLogic;

public class PasswordHasher : IMyPasswordHasher
{ 
    public string Generate(string password) =>
        BCrypt.Net.BCrypt.EnhancedHashPassword(password);

    public bool Verify (string password, string hashedPassword) =>
        BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
}
