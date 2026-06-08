import { z } from 'zod'

const catalogIdField = (message: string) =>
    z.number().int().min(1, message)

const fechaNacimientoField = z
    .string()
    .min(1, 'La fecha de nacimiento es obligatoria.')
    .refine((value) => {
        const date = new Date(`${value}T00:00:00`)
        return !Number.isNaN(date.getTime())
    }, 'La fecha de nacimiento no es válida.')
    .refine((value) => {
        const date = new Date(`${value}T00:00:00`)
        const today = new Date()
        today.setHours(0, 0, 0, 0)
        return date < today
    }, 'La fecha de nacimiento debe ser anterior a hoy.')

const pacienteBaseSchema = z.object({
    nombres: z
        .string()
        .trim()
        .min(1, 'Los nombres son obligatorios.')
        .max(150, 'Los nombres no pueden superar los 150 caracteres.'),
    apellidoPaterno: z
        .string()
        .trim()
        .min(1, 'El apellido paterno es obligatorio.')
        .max(100, 'El apellido paterno no puede superar los 100 caracteres.'),
    apellidoMaterno: z
        .string()
        .trim()
        .max(100, 'El apellido materno no puede superar los 100 caracteres.'),
    tipoDocumentoId: catalogIdField('Debe seleccionar un tipo de documento.'),
    numeroDocumento: z
        .string()
        .trim()
        .min(1, 'El número de documento es obligatorio.')
        .max(20, 'El número de documento no puede superar los 20 caracteres.'),
    complementoDocumento: z
        .string()
        .trim()
        .max(10, 'El complemento no puede superar los 10 caracteres.'),
    extensionDocumentoId: z.number().int().min(1).optional(),
    fechaNacimiento: fechaNacimientoField,
    sexoId: catalogIdField('Debe seleccionar un sexo.'),
    estadoCivilId: catalogIdField('Debe seleccionar un estado civil.'),
    telefono: z.string().trim().max(30, 'El teléfono no puede superar los 30 caracteres.'),
    direccion: z
        .string()
        .trim()
        .max(250, 'La dirección no puede superar los 250 caracteres.'),
    ocupacionProfesion: z
        .string()
        .trim()
        .max(150, 'La ocupación no puede superar los 150 caracteres.'),
})

export const createPacienteSchema = pacienteBaseSchema.extend({
    codigoPaciente: z
        .string()
        .trim()
        .min(1, 'El código de paciente es obligatorio.')
        .max(30, 'El código no puede superar los 30 caracteres.'),
})

export const updatePacienteSchema = pacienteBaseSchema

export type CreatePacienteFormInput = z.infer<typeof createPacienteSchema>
export type CreatePacienteFormValues = z.output<typeof createPacienteSchema>

export type UpdatePacienteFormInput = z.infer<typeof updatePacienteSchema>
export type UpdatePacienteFormValues = z.output<typeof updatePacienteSchema>

export const createPacienteDefaultValues: CreatePacienteFormInput = {
    codigoPaciente: '',
    nombres: '',
    apellidoPaterno: '',
    apellidoMaterno: '',
    tipoDocumentoId: 0,
    numeroDocumento: '',
    complementoDocumento: '',
    extensionDocumentoId: undefined,
    fechaNacimiento: '',
    sexoId: 0,
    estadoCivilId: 0,
    telefono: '',
    direccion: '',
    ocupacionProfesion: '',
}

export const updatePacienteDefaultValues: UpdatePacienteFormInput = {
    nombres: '',
    apellidoPaterno: '',
    apellidoMaterno: '',
    tipoDocumentoId: 0,
    numeroDocumento: '',
    complementoDocumento: '',
    extensionDocumentoId: undefined,
    fechaNacimiento: '',
    sexoId: 0,
    estadoCivilId: 0,
    telefono: '',
    direccion: '',
    ocupacionProfesion: '',
}

function toOptionalPayloadText(value: string) {
    const trimmed = value.trim()
    return trimmed.length > 0 ? trimmed : undefined
}

export function toCreatePacientePayload(
    values: CreatePacienteFormValues,
): import('../types/paciente.types').CreatePacientePayload {
    return {
        codigoPaciente: values.codigoPaciente.trim(),
        nombres: values.nombres.trim(),
        apellidoPaterno: values.apellidoPaterno.trim(),
        apellidoMaterno: values.apellidoMaterno.trim(),
        tipoDocumentoId: values.tipoDocumentoId,
        numeroDocumento: values.numeroDocumento.trim(),
        complementoDocumento: toOptionalPayloadText(values.complementoDocumento),
        extensionDocumentoId: values.extensionDocumentoId,
        fechaNacimiento: values.fechaNacimiento,
        sexoId: values.sexoId,
        estadoCivilId: values.estadoCivilId,
        telefono: values.telefono.trim(),
        direccion: values.direccion.trim(),
        ocupacionProfesion: toOptionalPayloadText(values.ocupacionProfesion),
    }
}

export function toUpdatePacientePayload(
    values: UpdatePacienteFormValues,
): import('../types/paciente.types').UpdatePacientePayload {
    const { codigoPaciente: _codigo, ...rest } = toCreatePacientePayload({
        ...values,
        codigoPaciente: 'unused',
    })

    return rest
}
