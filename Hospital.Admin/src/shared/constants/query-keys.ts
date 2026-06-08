import type { PagedQuery } from '../types/pagination.types'

export const queryKeys = {
    auth: {
        all: ['auth'] as const,
        me: ['auth', 'me'] as const,
    },
    roles: {
        all: ['roles'] as const,
        list: (query: PagedQuery) => ['roles', 'list', query] as const,
        detail: (id: number) => ['roles', 'detail', id] as const,
    },
    users: {
        all: ['users'] as const,
        list: (query: PagedQuery) => ['users', 'list', query] as const,
        detail: (id: number) => ['users', 'detail', id] as const,
    },
    catalogoGrupos: {
        all: ['catalogo-grupos'] as const,
        list: (query: PagedQuery) => ['catalogo-grupos', 'list', query] as const,
        detail: (id: number) => ['catalogo-grupos', 'detail', id] as const,
        items: (grupoId: number) =>
            ['catalogo-grupos', grupoId, 'items'] as const,
    },
    catalogoItems: {
        all: ['catalogo-items'] as const,
    },
    pacientes: {
        all: ['pacientes'] as const,
        list: (query: PagedQuery) => ['pacientes', 'list', query] as const,
        detail: (id: number) => ['pacientes', 'detail', id] as const,
    },
    catalogoClinico: {
        all: ['catalogo-clinico'] as const,
        areas: {
            all: ['catalogo-clinico', 'areas'] as const,
            list: (query: PagedQuery) =>
                ['catalogo-clinico', 'areas', 'list', query] as const,
            departamentos: (id: number) =>
                ['catalogo-clinico', 'areas', id, 'departamentos'] as const,
        },
        departamentos: {
            all: ['catalogo-clinico', 'departamentos'] as const,
            list: (query: PagedQuery) =>
                ['catalogo-clinico', 'departamentos', 'list', query] as const,
            servicios: (id: number) =>
                ['catalogo-clinico', 'departamentos', id, 'servicios'] as const,
        },
        servicios: {
            all: ['catalogo-clinico', 'servicios'] as const,
            list: (query: PagedQuery) =>
                ['catalogo-clinico', 'servicios', 'list', query] as const,
            prestaciones: (id: number) =>
                ['catalogo-clinico', 'servicios', id, 'prestaciones'] as const,
        },
        prestaciones: {
            all: ['catalogo-clinico', 'prestaciones'] as const,
            list: (query: PagedQuery) =>
                ['catalogo-clinico', 'prestaciones', 'list', query] as const,
        },
        especialidades: {
            all: ['catalogo-clinico', 'especialidades'] as const,
            list: (query: PagedQuery) =>
                ['catalogo-clinico', 'especialidades', 'list', query] as const,
        },
        profesiones: {
            all: ['catalogo-clinico', 'profesiones'] as const,
            list: (query: PagedQuery) =>
                ['catalogo-clinico', 'profesiones', 'list', query] as const,
        },
        cargos: {
            all: ['catalogo-clinico', 'cargos'] as const,
            list: (query: PagedQuery) =>
                ['catalogo-clinico', 'cargos', 'list', query] as const,
        },
        tiposAtencion: {
            all: ['catalogo-clinico', 'tipos-atencion'] as const,
            list: (query: PagedQuery) =>
                ['catalogo-clinico', 'tipos-atencion', 'list', query] as const,
        },
    },
}