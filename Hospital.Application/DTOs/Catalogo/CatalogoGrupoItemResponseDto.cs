namespace Hospital.Application.DTOs.Catalogo;

public sealed record CatalogoGrupoItemResponseDto(
    int Id,
    string Codigo,
    string Nombre,
    List<ItemDto> Items
);

public sealed record ItemDto(
    int Id,
    string Codigo,
    string Nombre,
    string Valor,
    int Orden,
    bool Activo
);