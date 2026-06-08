export type User = {
    id: number
    userName: string
    nombreCompleto: string
    activo: boolean
    roles: string[]
}

export type CreateUserPayload = {
    userName: string
    nombreCompleto: string
    password: string
    rol: string
}

export type UpdateUserPayload = {
    nombreCompleto: string
    rol: string
    password?: string
    activo: boolean
}

export type CreateUserResult = {
    id: number
}
