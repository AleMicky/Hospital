import { del, get, getPaged, post, put } from '../../../shared/api/http'
import { roleEndpoints } from '../../../shared/api/endpoints'
import type { PagedQuery } from '../../../shared/types/pagination.types'
import type {
    CreateRolePayload,
    CreateRoleResult,
    Role,
    UpdateRolePayload,
} from '../types/role.types'

export class RolesService {
    getPaged(query: PagedQuery) {
        return getPaged<Role>(roleEndpoints.root, query)
    }

    getById(id: number) {
        return get<Role>(roleEndpoints.byId(id))
    }

    create(data: CreateRolePayload) {
        return post<CreateRoleResult, CreateRolePayload>(roleEndpoints.root, data)
    }

    update(id: number, data: UpdateRolePayload) {
        return put<void, UpdateRolePayload>(roleEndpoints.byId(id), data)
    }

    delete(id: number) {
        return del<void>(roleEndpoints.byId(id))
    }
}

export const rolesService = new RolesService()
