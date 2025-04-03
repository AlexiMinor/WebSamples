using System.Security.Cryptography;
using System.Text;
using MediatR;
using WebApp.Core.DTOs;
using WebApp.Data.CQS.Commands;
using WebApp.Data.CQS.Queries;
using WebApp.Services.Abstract;
using WebApp.Services.Mappers;

namespace WebApp.Services.Implementations;

public class AccountService : IAccountService
{
    private readonly IMediator _mediator;
    private readonly UserMapper _mapper;

    public AccountService(IMediator mediator, UserMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<LoginDto> TryToLoginAsync(string modelEmail, string modelPassword)
    {
        var passwordHash = GetHash(modelPassword);
        var result = await _mediator.Send(new TryLoginQuery()
        {
            Email = modelEmail,
            PasswordHash = passwordHash
        });
        return _mapper.UserToLoginDto(result);
    }

    public async Task<LoginDto?> TryToRegister(string modelEmail, string modelPassword)
    {
        if (await _mediator.Send(new CheckUserWithEmailExistsQuery()
        {
            Email = modelEmail
        }))
            return null;

        var passwordHash = GetHash(modelPassword);

        await _mediator.Send(new TryToRegisterUserCommand()
        {
            Email = modelEmail,
            PasswordHash = passwordHash
        });

        var userDto =  _mapper.UserToLoginDto(await _mediator.Send(new GetUserByEmailQuery()));
        return userDto;
    }

    //todo: move to another service & add salt
    public string GetHash(string password)
    {
        using var sha256 = SHA256.Create();
        var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
    }
}