using Hospital.Application.DTOs.Catalogos;
using Hospital.Application.Interfaces.Common;

namespace Hospital.Application.Interfaces;

public interface IAreaService
    : ICrudService<CreateAreaDto, UpdateAreaDto, AreaResponseDto>
{
    Task<List<DepartamentoResponseDto>> GetDepartamentosByAreaIdAsync(int areaId);
}

public interface IDepartamentoService
    : ICrudService<CreateDepartamentoDto, UpdateDepartamentoDto, DepartamentoResponseDto>
{
    Task<List<ServicioResponseDto>> GetServiciosByDepartamentoIdAsync(int departamentoId);
}

public interface IServicioService
    : ICrudService<CreateServicioDto, UpdateServicioDto, ServicioResponseDto>
{
    Task<List<PrestacionResponseDto>> GetPrestacionesByServicioIdAsync(int servicioId);
}

public interface IPrestacionService
    : ICrudService<CreatePrestacionDto, UpdatePrestacionDto, PrestacionResponseDto>;

public interface IEspecialidadService
    : ICrudService<CreateEspecialidadDto, UpdateEspecialidadDto, EspecialidadResponseDto>;

public interface IProfesionService
    : ICrudService<CreateProfesionDto, UpdateProfesionDto, ProfesionResponseDto>;

public interface ICargoService
    : ICrudService<CreateCargoDto, UpdateCargoDto, CargoResponseDto>;

public interface ITipoAtencionCatalogoService
    : ICrudService<
        CreateTipoAtencionCatalogoDto,
        UpdateTipoAtencionCatalogoDto,
        TipoAtencionCatalogoResponseDto>;
