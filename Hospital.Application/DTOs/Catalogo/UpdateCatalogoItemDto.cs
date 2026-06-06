namespace Hospital.Application.DTOs.Catalogo;

public sealed record UpdateCatalogoItemDto(
    int CatalogoGrupoId,
    string Codigo,
    string Nombre, 
    string Valor,
    int Orden
);