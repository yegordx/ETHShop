using ETHShop.Entities;

namespace ETHShop.Interfaces;

public interface IJwtProvider
{
    string GenerateUserToken(User user);
    string GenerateSellerToken(Seller seller);

}
