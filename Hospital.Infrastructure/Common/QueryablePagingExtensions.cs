using Hospital.Application.Common;
using Microsoft.EntityFrameworkCore;

namespace Hospital.Infrastructure.Common;

public static class QueryablePagingExtensions
{
    public static async Task<PagedResult<T>> ToPagedResultAsync<T>(
        this IQueryable<T> query,
        PagedQuery pagedQuery,
        CancellationToken cancellationToken = default)
    {
        var totalCount = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip(pagedQuery.Skip)
            .Take(pagedQuery.NormalizedPageSize)
            .ToListAsync(cancellationToken);

        return PagedResult<T>.Create(items, totalCount, pagedQuery);
    }
}
