import { del, post, put } from '../../../shared/api/http'
import { catalogoItemEndpoints } from '../../../shared/api/endpoints'
import type {
    CatalogoItem,
    CreateCatalogoItemPayload,
    CreateCatalogoResult,
    UpdateCatalogoItemPayload,
} from '../types/catalogo.types'

export class CatalogoItemsService {
    create(data: CreateCatalogoItemPayload) {
        return post<CreateCatalogoResult, CreateCatalogoItemPayload>(
            catalogoItemEndpoints.root,
            data,
        )
    }

    update(id: number, data: UpdateCatalogoItemPayload) {
        return put<void, UpdateCatalogoItemPayload>(
            catalogoItemEndpoints.byId(id),
            data,
        )
    }

    delete(id: number) {
        return del<void>(catalogoItemEndpoints.byId(id))
    }
}

export const catalogoItemsService = new CatalogoItemsService()

// Re-export type for hooks convenience
export type { CatalogoItem }
