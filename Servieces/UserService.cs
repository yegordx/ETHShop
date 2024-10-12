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

    public async Task<string> Login(string Email, string Password)
    {
        var user = await _usersRepository.GetByEmail(Email);

        var result = _passwordHasher.Verify(Password, user.PasswordHash);

        if (result == false)
        {
            throw new Exception("Failed to Login");
        }

        var token = _jwtProvider.GenerateUserToken(user);
        return token;
    }

    public async Task<List<OrderDto>> GetOrders(Guid userID)
    {
        var result = await _usersRepository.GetOrders(userID);
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
    public async Task Update(Guid Id, string UserName, string Email, string Password, string WalletAddress)
    {
        var hashedPassword = _passwordHasher.Generate(Password);
        await _usersRepository.Update(Id, UserName, Email, hashedPassword, WalletAddress);
    }
    public async Task Delete(Guid Id)
    {
        await _usersRepository.Delete(Id);
    }
}
