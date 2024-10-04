using ETHShop.PassWordLogic;
using Microsoft.EntityFrameworkCore.Query.Internal;
using ETHShop.Entities;
using ETHShop.Controllers;
using ETHShop.Interfaces;
using ETHShop.Repositories;
using ETHShop.Contracts;

namespace ETHShop.Servieces;

public class UserService : IUserService
{
    private readonly IMyPasswordHasher _passwordHasher;
    private readonly IUsersRepository _usersRepository;
    private readonly IJwtProvider _jwtProvider;
    public UserService(IMyPasswordHasher passwordHasher, 
        IUsersRepository usersRepository,
        IJwtProvider jwtProvider)
    {
        _passwordHasher = passwordHasher;
        _usersRepository = usersRepository;
        _jwtProvider = jwtProvider;
    }
    //string userName, string email, string password, string walletaddress
    public async Task<string> Register (Guid id, string userName, string password, string email, string walletAddress)
    {
        var hashedPassword = _passwordHasher.Generate(password);

        var user = User.Create(id, userName, hashedPassword, email, walletAddress);
        try
        {
            await _usersRepository.Add(user);
            var token = _jwtProvider.GenerateUserToken(user);
            return token;
        }
        catch (InvalidOperationException ex)
        {
            throw new InvalidOperationException(ex.Message);
        }
    }

    public async Task<string> Login(LoginUserRequest request)
    {
        var user = await _usersRepository.GetByEmail(request.Email);

        var result = _passwordHasher.Verify(request.Password, user.PasswordHash);

        if (result == false)
        {
            throw new Exception("Failed to Login");
        }

        var token = _jwtProvider.GenerateUserToken(user);
        return token;
    }

    public async Task<bool> AddProductToCart(Guid userID, Guid productID) {

        bool result = await _usersRepository.AddProductToCart(userID, productID);

        return result;
    }

    public async Task<IEnumerable<User>> GetAll()
    {
        return await _usersRepository.GetAll();
    }
    public async Task<User> GetByEmail(string email)
    {
        return await _usersRepository.GetByEmail(email);
    }
    public async Task Update(User user)
    {
        await _usersRepository.Update(user);
    }
    public async Task Delete(string email)
    {
        await _usersRepository.Delete(email);
    }
}
