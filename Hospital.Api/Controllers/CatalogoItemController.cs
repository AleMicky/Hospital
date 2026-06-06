using Hospital.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Hospital.Api.Controllers.Common;
using Hospital.Application.DTOs.Catalogo;

namespace Hospital.Api.Controllers;

[Route("api/catalogo-items")]
public class CatalogoItemController(
    ICatalogoItemService service) :
    CrudController<
        CreateCatalogoItemDto,
        UpdateCatalogoItemDto,
        CatalogoItemResponseDto>(service)
{
}