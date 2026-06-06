namespace Hospital.Application.DTOs.Catalogo;

public sealed record CreateCatalogoItemDto(
    int CatalogoGrupoId,
    string Codigo,
    string Nombre, 
    string Valor,
    int Orden
);