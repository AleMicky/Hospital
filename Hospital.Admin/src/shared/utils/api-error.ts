import { AxiosError } from 'axios'
import type { ApiResponse } from '../types/api-response.types'

export function getApiErrorMessage(error: unknown): string {
  if (error instanceof AxiosError) {
    const response = error.response?.data as ApiResponse<unknown> | undefined

    return (
      response?.message ||
      'Ocurrió un error al procesar la solicitud.'
    )
  }

  if (error instanceof Error) {
    return error.message
  }

  return 'Ocurrió un error inesperado.'
}