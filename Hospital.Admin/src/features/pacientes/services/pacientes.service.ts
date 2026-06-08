import { del, get, getPaged, post, put } from '../../../shared/api/http'
import { pacienteEndpoints } from '../../../shared/api/endpoints'
import type { PagedQuery } from '../../../shared/types/pagination.types'
import type {
    CreatePacientePayload,
    CreatePacienteResult,
    Paciente,
    UpdatePacientePayload,
} from '../types/paciente.types'

export class PacientesService {
    getPaged(query: PagedQuery) {
        return getPaged<Paciente>(pacienteEndpoints.root, query)
    }

    getById(id: number) {
        return get<Paciente>(pacienteEndpoints.byId(id))
    }

    create(data: CreatePacientePayload) {
        return post<CreatePacienteResult, CreatePacientePayload>(
            pacienteEndpoints.root,
            data,
        )
    }

    update(id: number, data: UpdatePacientePayload) {
        return put<void, UpdatePacientePayload>(pacienteEndpoints.byId(id), data)
    }

    delete(id: number) {
        return del<void>(pacienteEndpoints.byId(id))
    }
}

export const pacientesService = new PacientesService()
