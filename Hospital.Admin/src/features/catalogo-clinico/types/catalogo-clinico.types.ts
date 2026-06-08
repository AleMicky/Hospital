export type CatalogoBase = {
    id: number
    codigo: string
    nombre: string
    descripcion: string | null
}

export type Area = CatalogoBase

export type Departamento = CatalogoBase & {
    areaId: number
    areaNombre: string
}

export type Servicio = CatalogoBase & {
    departamentoId: number
    departamentoNombre: string
}

export type Prestacion = CatalogoBase & {
    servicioId: number
    servicioNombre: string
    precio: number
    requiereOrdenMedica: boolean
    requiereMedico: boolean
}

export type Especialidad = CatalogoBase
export type Profesion = CatalogoBase
export type Cargo = CatalogoBase
export type TipoAtencionCatalogo = CatalogoBase

export type CreateCatalogoBasePayload = {
    codigo: string
    nombre: string
    descripcion?: string | null
}

export type UpdateCatalogoBasePayload = {
    codigo: string
    nombre: string
    descripcion?: string | null
}

export type CreateDepartamentoPayload = CreateCatalogoBasePayload & {
    areaId: number
}

export type UpdateDepartamentoPayload = UpdateCatalogoBasePayload & {
    areaId: number
}

export type CreateServicioPayload = CreateCatalogoBasePayload & {
    departamentoId: number
}

export type UpdateServicioPayload = UpdateCatalogoBasePayload & {
    departamentoId: number
}

export type CreatePrestacionPayload = CreateCatalogoBasePayload & {
    servicioId: number
    precio: number
    requiereOrdenMedica: boolean
    requiereMedico: boolean
}

export type UpdatePrestacionPayload = UpdateCatalogoBasePayload & {
    servicioId: number
    precio: number
    requiereOrdenMedica: boolean
    requiereMedico: boolean
}

export type CreateCatalogoResult = { id: number }

export type CatalogoClinicoSection =
    | 'jerarquia'
    | 'especialidades'
    | 'profesiones'
    | 'cargos'
    | 'tipos-atencion'
