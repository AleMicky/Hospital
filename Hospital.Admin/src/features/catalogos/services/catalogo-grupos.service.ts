import { del, get, getPaged, post, put } from '../../../shared/api/http'
import { catalogoGrupoEndpoints } from '../../../shared/api/endpoints'
import type { PagedQuery } from '../../../shared/types/pagination.types'
import type {
    CatalogoGrupo,
    CatalogoGrupoConItems,
    CatalogoItem,
    CreateCatalogoGrupoPayload,
    CreateCatalogoResult,
    UpdateCatalogoGrupoPayload,
} from '../types/catalogo.types'

export class CatalogoGruposService {
    getPaged(query: PagedQuery) {
        return getPaged<CatalogoGrupo>(catalogoGrupoEndpoints.root, query)
    }

    getById(id: number) {
        return get<CatalogoGrupo>(catalogoGrupoEndpoints.byId(id))
    }

    getItemsByGrupo(grupoId: number) {
        return get<CatalogoItem[]>(catalogoGrupoEndpoints.itemsByGrupo(grupoId))
    }

    getGroupedItems() {
        return get<CatalogoGrupoConItems[]>(catalogoGrupoEndpoints.groupedItems)
    }

    create(data: CreateCatalogoGrupoPayload) {
        return post<CreateCatalogoResult, CreateCatalogoGrupoPayload>(
            catalogoGrupoEndpoints.root,
            data,
        )
    }

    update(id: number, data: UpdateCatalogoGrupoPayload) {
        return put<void, UpdateCatalogoGrupoPayload>(
            catalogoGrupoEndpoints.byId(id),
            data,
        )
    }

    delete(id: number) {
        return del<void>(catalogoGrupoEndpoints.byId(id))
    }
}

export const catalogoGruposService = new CatalogoGruposService()
