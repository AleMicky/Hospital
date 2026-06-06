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