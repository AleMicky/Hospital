import { del, get, getPaged, post, put } from '../../../shared/api/http'
import { userEndpoints } from '../../../shared/api/endpoints'
import type { PagedQuery } from '../../../shared/types/pagination.types'
import type {
    CreateUserPayload,
    CreateUserResult,
    UpdateUserPayload,
    User,
} from '../types/user.types'

export class UsersService {
    getPaged(query: PagedQuery) {
        return getPaged<User>(userEndpoints.root, query)
    }

    getById(id: number) {
        return get<User>(userEndpoints.byId(id))
    }

    create(data: CreateUserPayload) {
        return post<CreateUserResult, CreateUserPayload>(userEndpoints.root, data)
    }

    update(id: number, data: UpdateUserPayload) {
        return put<void, UpdateUserPayload>(userEndpoints.byId(id), data)
    }

    delete(id: number) {
        return del<void>(userEndpoints.byId(id))
    }
}

export const usersService = new UsersService()
