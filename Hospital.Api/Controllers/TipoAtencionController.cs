using Hospital.Application.Interfaces;
using Hospital.Api.Controllers.Common;
using Hospital.Application.DTOs.TiposAtencion;
using Microsoft.AspNetCore.Mvc;

namespace Hospital.Api.Controllers;

[Route("api/tipos-atencion")]
public class TipoAtencionController(ITipoAtencionService service)
    : CrudController<CreateTipoAtencionDto, UpdateTipoAtencionDto, TipoAtencionResponseDto>(service);
