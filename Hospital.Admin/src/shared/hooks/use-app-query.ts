import {
    useQuery,
    type UseQueryOptions,
    type QueryKey,
} from '@tanstack/react-query'

import { notify } from '../utils/notify'

export function useAppQuery<
    TQueryFnData,
    TError = Error,
    TData = TQueryFnData,
    TQueryKey extends QueryKey = QueryKey,
>(
    options: UseQueryOptions<
        TQueryFnData,
        TError,
        TData,
        TQueryKey
    >,
) {
    return useQuery({
        retry: false,
        ...options,

        throwOnError: (error) => {
            const message =
                error instanceof Error
                    ? error.message
                    : 'Ocurrió un error inesperado.'

            notify.error('Error', message)

            return false
        },
    })
}