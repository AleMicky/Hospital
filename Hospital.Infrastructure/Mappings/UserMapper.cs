using Hospital.Application.DTOs.Users;
using Hospital.Infrastructure.Identity;
using Riok.Mapperly.Abstractions;

namespace Hospital.Infrastructure.Mappings;

[Mapper(
    RequiredMappingStrategy = RequiredMappingStrategy.None
)]
public partial class UserMapper
{
    [MapperIgnoreSource(nameof(CreateUserDto.Password))]
    [MapperIgnoreSource(nameof(CreateUserDto.Rol))]
    public partial ApplicationUser ToEntity(CreateUserDto dto);

    [MapperIgnoreTarget(nameof(UserResponseDto.Roles))]
    public partial UserResponseDto ToDto(ApplicationUser user);

    [MapperIgnoreTarget(nameof(ApplicationUser.Id))]
    [MapperIgnoreTarget(nameof(ApplicationUser.UserName))]
    [MapperIgnoreTarget(nameof(ApplicationUser.RefreshToken))]
    [MapperIgnoreTarget(nameof(ApplicationUser.RefreshTokenExpiresAt))]
    public partial void UpdateEntity(UpdateUserDto dto, ApplicationUser user);
}