using Hospital.Application.Interfaces;
using Hospital.Api.Controllers.Common;
using Hospital.Application.DTOs.Catalogos;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers.Catalogos;

[Route("api/catalogos/prestaciones")]
public class PrestacionController(IPrestacionService service)
    : CrudController<CreatePrestacionDto, UpdatePrestacionDto, PrestacionResponseDto>(service);

[Route("api/catalogos/especialidades")]
public class EspecialidadController(IEspecialidadService service)
    : CrudController<CreateEspecialidadDto, UpdateEspecialidadDto, EspecialidadResponseDto>(service);

[Route("api/catalogos/profesiones")]
public class ProfesionController(IProfesionService service)
    : CrudController<CreateProfesionDto, UpdateProfesionDto, ProfesionResponseDto>(service);

[Route("api/catalogos/cargos")]
public class CargoController(ICargoService service)
    : CrudController<CreateCargoDto, UpdateCargoDto, CargoResponseDto>(service);

[Route("api/catalogos/tipos-atencion")]
public class TipoAtencionCatalogoController(ITipoAtencionCatalogoService service)
    : CrudController<
        CreateTipoAtencionCatalogoDto,
        UpdateTipoAtencionCatalogoDto,
        TipoAtencionCatalogoResponseDto>(service);
