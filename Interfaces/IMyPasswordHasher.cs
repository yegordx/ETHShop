namespace ETHShop.Interfaces;

public interface IMyPasswordHasher
{
     string Generate(string password);

     bool Verify(string password, string hashedPassword);
}
