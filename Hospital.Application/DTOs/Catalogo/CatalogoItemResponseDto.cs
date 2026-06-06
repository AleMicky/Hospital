namespace Hospital.Application.DTOs.Catalogo;

public sealed record CatalogoItemResponseDto(
    int Id,
    int CatalogoGrupoId,
    string Codigo,
    string Nombre,
    string Valor,
    int Orden
);