export type Paciente = {
    id: number
    codigoPaciente: string
    nombres: string
    apellidoPaterno: string
    apellidoMaterno: string
    nombreCompleto: string
    tipoDocumentoId: number
    tipoDocumento: string
    numeroDocumento: string
    complementoDocumento?: string
    extensionDocumentoId?: number
    extensionDocumento?: string
    documentoCompleto: string
    fechaNacimiento: string
    edad: number
    sexoId: number
    sexo: string
    estadoCivilId: number
    estadoCivil: string
    telefono: string
    direccion: string
    ocupacionProfesion?: string
    activo: boolean
}

export type CreatePacientePayload = {
    codigoPaciente: string
    nombres: string
    apellidoPaterno: string
    apellidoMaterno: string
    tipoDocumentoId: number
    numeroDocumento: string
    complementoDocumento?: string
    extensionDocumentoId?: number
    fechaNacimiento: string
    sexoId: number
    estadoCivilId: number
    telefono: string
    direccion: string
    ocupacionProfesion?: string
}

export type UpdatePacientePayload = Omit<CreatePacientePayload, 'codigoPaciente'>

export type CreatePacienteResult = {
    id: number
}
