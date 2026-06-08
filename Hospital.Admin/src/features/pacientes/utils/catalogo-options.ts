import type { CatalogoGrupoConItems } from '../../catalogos/types/catalogo.types'

export const PACIENTE_CATALOGO = {
    tipoDocumento: 'TIPO_DOCUMENTO',
    extensionDocumento: 'EXTENSION_DOCUMENTO',
    sexo: 'SEXO',
    estadoCivil: 'ESTADO_CIVIL',
} as const

export function getCatalogoSelectOptions(
    grupos: CatalogoGrupoConItems[] | undefined,
    codigo: string,
) {
    return (grupos?.find((grupo) => grupo.codigo === codigo)?.items ?? []).map(
        (item) => ({
            label: item.nombre,
            value: item.id,
        }),
    )
}
