export type EntityId = number | string

export type BaseEndpoints = {
    root: string
    byId: (id: EntityId) => string
}

export const createEndpoints = <
    T extends Record<string, unknown> = Record<string, never>,
>(
    root: string,
    extra?: (root: string) => T,
): BaseEndpoints & T => {
    const base: BaseEndpoints = {
        root,
        byId: (id: EntityId) => `${root}/${id}`,
    }
    return {
        ...base,
        ...(extra?.(root) ?? {}),
    } as BaseEndpoints & T
}

export const authEndpoints = createEndpoints('/auth', (root) => ({
    login: `${root}/login`,
    refresh: `${root}/refresh`,
    me: `${root}/me`,
    logout: `${root}/logout`,
}))

export const roleEndpoints = createEndpoints('/roles')

export const userEndpoints = createEndpoints('/users')

export const catalogoGrupoEndpoints = createEndpoints(
    '/catalogo-grupos',
    (root) => ({
        groupedItems: `${root}/items`,
        itemsByGrupo: (id: EntityId) => `${root}/${id}/catalogo-items`,
    }),
)

export const catalogoItemEndpoints = createEndpoints('/catalogo-items')

export const pacienteEndpoints = createEndpoints('/pacientes')

export const catalogoClinicoEndpoints = {
    areas: createEndpoints('/catalogos/areas', (root) => ({
        departamentos: (id: EntityId) => `${root}/${id}/departamentos`,
    })),
    departamentos: createEndpoints('/catalogos/departamentos', (root) => ({
        servicios: (id: EntityId) => `${root}/${id}/servicios`,
    })),
    servicios: createEndpoints('/catalogos/servicios', (root) => ({
        prestaciones: (id: EntityId) => `${root}/${id}/prestaciones`,
    })),
    prestaciones: createEndpoints('/catalogos/prestaciones'),
    especialidades: createEndpoints('/catalogos/especialidades'),
    profesiones: createEndpoints('/catalogos/profesiones'),
    cargos: createEndpoints('/catalogos/cargos'),
    tiposAtencion: createEndpoints('/catalogos/tipos-atencion'),
} as const