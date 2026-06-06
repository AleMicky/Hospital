using Hospital.Application.DTOs.Users;
using Hospital.Application.Interfaces.Common;

namespace Hospital.Application.Interfaces;

public interface IUserService
    : ICrudService<CreateUserDto, UpdateUserDto, UserResponseDto>;
