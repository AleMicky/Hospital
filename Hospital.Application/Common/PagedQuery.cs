namespace Hospital.Application.Common;

public class PagedQuery
{
    private const int MaxPageSize = 100;

    public int Page { get; init; } = 1;
    public int PageSize { get; init; } = 20;
    public string? Search { get; init; }

    public int Skip => (NormalizedPage - 1) * NormalizedPageSize;

    public int NormalizedPage => Page < 1 ? 1 : Page;

    public int NormalizedPageSize =>
        PageSize switch
        {
            < 1 => 20,
            > MaxPageSize => MaxPageSize,
            _ => PageSize
        };
}
