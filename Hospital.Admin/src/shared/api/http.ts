import { api } from './axios'
import type { ApiResponse } from '../types/api-response.types'
import { unwrap } from '../utils/helper'

export async function post<TResponse, TRequest>(
    url: string,
    body: TRequest,
): Promise<TResponse> {
    const { data } = await api.post<
        ApiResponse<TResponse>
    >(
        url,
        body,
    )

    return unwrap(data)
}

export async function get<TResponse>(
    url: string,
): Promise<TResponse> {
    const { data } = await api.get<
        ApiResponse<TResponse>
    >(url)

    return unwrap(data)
}