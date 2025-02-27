using Riok.Mapperly.Abstractions;
using WebApp.Core.DTOs;
using WebApp.Data.Entities;

namespace WebApp.Services.Mappers;

[Mapper]
public partial class UserMapper
{
    [MapProperty($"{nameof(Role)}.{nameof(Role.Name)}", nameof(LoginDto.Role))]
    [MapProperty($"{nameof(User.Name)}", nameof(LoginDto.Nickname))]
    public partial LoginDto UserToLoginDto(User user);


}