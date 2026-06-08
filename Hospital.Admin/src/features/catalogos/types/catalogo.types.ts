export type CatalogoGrupo = {
    id: number
    codigo: string
    nombre: string
    descripcion: string
}

export type CatalogoItem = {
    id: number
    catalogoGrupoId: number
    codigo: string
    nombre: string
    valor: string
    orden: number
}

export type CreateCatalogoGrupoPayload = {
    codigo: string
    nombre: string
    descripcion: string
}

export type UpdateCatalogoGrupoPayload = {
    nombre: string
    descripcion: string
}

export type CreateCatalogoItemPayload = {
    catalogoGrupoId: number
    codigo: string
    nombre: string
    valor: string
    orden: number
}

export type UpdateCatalogoItemPayload = CreateCatalogoItemPayload

export type CreateCatalogoResult = {
    id: number
}

export type CatalogoItemOption = {
    id: number
    codigo: string
    nombre: string
    valor: string
    orden: number
    activo: boolean
}

export type CatalogoGrupoConItems = {
    id: number
    codigo: string
    nombre: string
    items: CatalogoItemOption[]
}
