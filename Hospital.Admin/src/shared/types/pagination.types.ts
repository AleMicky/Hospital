export type PagedQuery = {
    page?: number
    pageSize?: number
    search?: string
}

export type PagedResult<T> = {
    items: T[]
    totalCount: number
    page: number
    pageSize: number
    totalPages: number
}
