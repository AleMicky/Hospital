export type Role = {
    id: number
    name: string
}

export type CreateRolePayload = {
    name: string
}

export type UpdateRolePayload = {
    name: string
}

export type CreateRoleResult = {
    id: number
}
