using Hospital.Application.DTOs.TiposAtencion;
using Hospital.Application.Interfaces.Common;

namespace Hospital.Application.Interfaces;

public interface ITipoAtencionService
    : ICrudService<CreateTipoAtencionDto, UpdateTipoAtencionDto, TipoAtencionResponseDto>;
