using Hospital.Application.DTOs.Roles;
using Hospital.Application.Interfaces.Common;

namespace Hospital.Application.Interfaces;

public interface IRoleService
    : ICrudService<CreateRoleDto, UpdateRoleDto, RoleResponseDto>;
