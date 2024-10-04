namespace ETHShop.PasswordLogic;

public interface IPasswordHasher
{
    string Generate(string password);
    bool Verufy(string password, string hashedPassword);
}
