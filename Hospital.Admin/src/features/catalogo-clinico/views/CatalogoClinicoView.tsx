import { useState } from 'react'
import { Col, Flex, Grid, Row, Statistic, Typography } from 'antd'
import { DatabaseOutlined } from '@ant-design/icons'

import {
    CatalogoClinicoSidebar,
    getSectionMeta,
} from '../components/CatalogoClinicoSidebar'
import { CatalogoJerarquiaPanel } from '../components/CatalogoJerarquiaPanel'
import { CatalogoSimplePanel } from '../components/CatalogoSimplePanel'
import {
    useCargos,
    useCreateCargo,
    useCreateEspecialidad,
    useCreateProfesion,
    useCreateTipoAtencionCatalogo,
    useDeleteCargo,
    useDeleteEspecialidad,
    useDeleteProfesion,
    useDeleteTipoAtencionCatalogo,
    useEspecialidades,
    useProfesiones,
    useTiposAtencionCatalogo,
    useUpdateCargo,
    useUpdateEspecialidad,
    useUpdateProfesion,
    useUpdateTipoAtencionCatalogo,
} from '../hooks/catalogo-clinico.hooks'
import type { CatalogoBaseFormValues } from '../schemas/catalogo-clinico.schema'
import type { CatalogoClinicoSection } from '../types/catalogo-clinico.types'

const { Title, Text } = Typography
const { useBreakpoint } = Grid
const DEFAULT_PAGE_SIZE = 20

function toPayload(values: CatalogoBaseFormValues) {
    return {
        codigo: values.codigo,
        nombre: values.nombre,
        descripcion: values.descripcion || null,
    }
}

function SimpleCatalogSection({
    section,
}: {
    section: Exclude<CatalogoClinicoSection, 'jerarquia'>
}) {
    const [page, setPage] = useState(1)
    const [pageSize, setPageSize] = useState(DEFAULT_PAGE_SIZE)
    const [search, setSearch] = useState('')

    const query = { page, pageSize, search: search || undefined }
    const meta = getSectionMeta(section)

    const especialidades = useEspecialidades(query)
    const profesiones = useProfesiones(query)
    const cargos = useCargos(query)
    const tiposAtencion = useTiposAtencionCatalogo(query)

    const createEspecialidad = useCreateEspecialidad()
    const updateEspecialidad = useUpdateEspecialidad()
    const deleteEspecialidad = useDeleteEspecialidad()
    const createProfesion = useCreateProfesion()
    const updateProfesion = useUpdateProfesion()
    const deleteProfesion = useDeleteProfesion()
    const createCargo = useCreateCargo()
    const updateCargo = useUpdateCargo()
    const deleteCargo = useDeleteCargo()
    const createTipoAtencion = useCreateTipoAtencionCatalogo()
    const updateTipoAtencion = useUpdateTipoAtencionCatalogo()
    const deleteTipoAtencion = useDeleteTipoAtencionCatalogo()

    const config = {
        especialidades: {
            data: especialidades,
            create: createEspecialidad,
            update: updateEspecialidad,
            delete: deleteEspecialidad,
            entityLabel: 'especialidad',
            newButtonLabel: 'Nueva especialidad',
            searchPlaceholder: 'Buscar especialidad…',
        },
        profesiones: {
            data: profesiones,
            create: createProfesion,
            update: updateProfesion,
            delete: deleteProfesion,
            entityLabel: 'profesión',
            newButtonLabel: 'Nueva profesión',
            searchPlaceholder: 'Buscar profesión…',
        },
        cargos: {
            data: cargos,
            create: createCargo,
            update: updateCargo,
            delete: deleteCargo,
            entityLabel: 'cargo',
            newButtonLabel: 'Nuevo cargo',
            searchPlaceholder: 'Buscar cargo…',
        },
        'tipos-atencion': {
            data: tiposAtencion,
            create: createTipoAtencion,
            update: updateTipoAtencion,
            delete: deleteTipoAtencion,
            entityLabel: 'tipo de atención',
            newButtonLabel: 'Nuevo tipo',
            searchPlaceholder: 'Buscar tipo de atención…',
        },
    }[section]

    const isSaving = config.create.isPending || config.update.isPending

    return (
        <CatalogoSimplePanel
            title={meta.title}
            subtitle={meta.description}
            entityLabel={config.entityLabel}
            newButtonLabel={config.newButtonLabel}
            searchPlaceholder={config.searchPlaceholder}
            items={config.data.data?.items ?? []}
            total={config.data.data?.totalCount ?? 0}
            page={page}
            pageSize={pageSize}
            search={search}
            loading={config.data.isFetching}
            isSaving={isSaving}
            onPageChange={(nextPage, nextPageSize) => {
                setPage(nextPage)
                setPageSize(nextPageSize)
            }}
            onSearch={(value) => {
                setSearch(value)
                setPage(1)
            }}
            onCreate={async (values) => {
                await config.create.mutateAsync(toPayload(values))
            }}
            onUpdate={async (id, values) => {
                await config.update.mutateAsync({ id, data: toPayload(values) })
            }}
            onDelete={async (id) => {
                await config.delete.mutateAsync(id)
            }}
        />
    )
}

export function CatalogoClinicoView() {
    const screens = useBreakpoint()
    const isStacked = !screens.lg
    const [section, setSection] = useState<CatalogoClinicoSection>('jerarquia')
    const meta = getSectionMeta(section)

    return (
        <div className="catalogo-clinico-view">
            <header className="catalogo-clinico-view__header">
                <Flex
                    justify="space-between"
                    align={isStacked ? 'flex-start' : 'center'}
                    gap={16}
                    wrap="wrap"
                >
                    <Flex align="center" gap={16}>
                        <div className="catalogo-clinico-view__header-icon" aria-hidden>
                            <DatabaseOutlined />
                        </div>
                        <div>
                            <Title level={3} className="catalogo-clinico-view__title">
                                Catálogo clínico
                            </Title>
                            <Text type="secondary">
                                Estructura organizacional, prestaciones y catálogos del
                                personal de salud.
                            </Text>
                        </div>
                    </Flex>

                    <div className="catalogo-clinico-view__stat">
                        <Statistic
                            title="Sección activa"
                            value={meta.title}
                            valueStyle={{ fontSize: 16 }}
                        />
                    </div>
                </Flex>
            </header>

            <Row gutter={[20, 20]} className="catalogo-clinico-view__layout">
                <Col xs={24} lg={6} xl={5}>
                    <CatalogoClinicoSidebar
                        activeSection={section}
                        onSectionChange={setSection}
                    />
                </Col>

                <Col xs={24} lg={18} xl={19}>
                    <section className="catalogo-clinico-view__content">
                        {section === 'jerarquia' ? (
                            <CatalogoJerarquiaPanel />
                        ) : (
                            <SimpleCatalogSection section={section} />
                        )}
                    </section>
                </Col>
            </Row>
        </div>
    )
}
