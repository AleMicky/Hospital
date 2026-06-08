import { useEffect } from 'react'
import { useForm } from '@tanstack/react-form'
import { Col, Divider, Form, Input, Modal, Row, Select, Typography } from 'antd'

import { useCatalogoGruposGrouped } from '../../catalogos/hooks/catalogo-grupos.hooks'
import {
    createPacienteDefaultValues,
    createPacienteSchema,
    updatePacienteSchema,
    type CreatePacienteFormValues,
    type UpdatePacienteFormValues,
} from '../schemas/paciente.schema'
import type { Paciente } from '../types/paciente.types'
import {
    getCatalogoSelectOptions,
    PACIENTE_CATALOGO,
} from '../utils/catalogo-options'

const { Text } = Typography

type PacienteFormModalProps = {
    open: boolean
    paciente: Paciente | null
    loading: boolean
    onClose: () => void
    onCreate: (values: CreatePacienteFormValues) => Promise<void>
    onUpdate: (values: UpdatePacienteFormValues) => Promise<void>
}

function getFieldError(errors: unknown[]) {
    return errors
        .map((error) =>
            typeof error === 'string'
                ? error
                : (error as { message: string }).message,
        )
        .join(', ')
}

function SectionTitle({ children }: { children: string }) {
    return (
        <Text type="secondary" style={{ fontSize: 12, fontWeight: 600, letterSpacing: '0.04em' }}>
            {children.toUpperCase()}
        </Text>
    )
}

export function PacienteFormModal({
    open,
    paciente,
    loading,
    onClose,
    onCreate,
    onUpdate,
}: PacienteFormModalProps) {
    const isEditing = paciente !== null
    const { data: catalogos, isFetching: loadingCatalogos } = useCatalogoGruposGrouped()

    const form = useForm({
        defaultValues: createPacienteDefaultValues,
        validators: {
            onSubmit: ({ value }) => {
                const schema = isEditing ? updatePacienteSchema : createPacienteSchema
                const result = schema.safeParse(value)

                if (!result.success) {
                    return result.error.issues.map((issue) => issue.message).join(', ')
                }
            },
        },
        onSubmit: async ({ value }) => {
            if (isEditing) {
                const { codigoPaciente: _codigo, ...updateValues } = value
                await onUpdate(updateValues)
                return
            }

            await onCreate(value)
        },
    })

    useEffect(() => {
        if (!open) return

        if (paciente) {
            form.reset()
            form.setFieldValue('codigoPaciente', paciente.codigoPaciente)
            form.setFieldValue('nombres', paciente.nombres)
            form.setFieldValue('apellidoPaterno', paciente.apellidoPaterno)
            form.setFieldValue('apellidoMaterno', paciente.apellidoMaterno)
            form.setFieldValue('tipoDocumentoId', paciente.tipoDocumentoId)
            form.setFieldValue('numeroDocumento', paciente.numeroDocumento)
            form.setFieldValue('complementoDocumento', paciente.complementoDocumento ?? '')
            form.setFieldValue('extensionDocumentoId', paciente.extensionDocumentoId)
            form.setFieldValue('fechaNacimiento', paciente.fechaNacimiento)
            form.setFieldValue('sexoId', paciente.sexoId)
            form.setFieldValue('estadoCivilId', paciente.estadoCivilId)
            form.setFieldValue('telefono', paciente.telefono)
            form.setFieldValue('direccion', paciente.direccion)
            form.setFieldValue('ocupacionProfesion', paciente.ocupacionProfesion ?? '')
            return
        }

        form.reset()
    }, [open, paciente, form])

    const handleClose = () => {
        if (loading) return
        onClose()
    }

    const tipoDocumentoOptions = getCatalogoSelectOptions(
        catalogos,
        PACIENTE_CATALOGO.tipoDocumento,
    )
    const extensionDocumentoOptions = getCatalogoSelectOptions(
        catalogos,
        PACIENTE_CATALOGO.extensionDocumento,
    )
    const sexoOptions = getCatalogoSelectOptions(catalogos, PACIENTE_CATALOGO.sexo)
    const estadoCivilOptions = getCatalogoSelectOptions(
        catalogos,
        PACIENTE_CATALOGO.estadoCivil,
    )

    const formDisabled = loading || loadingCatalogos

    return (
        <Modal
            title={isEditing ? 'Editar paciente' : 'Nuevo paciente'}
            open={open}
            onCancel={handleClose}
            onOk={() => void form.handleSubmit()}
            okText={isEditing ? 'Guardar' : 'Crear'}
            cancelText="Cancelar"
            confirmLoading={loading}
            destroyOnHidden
            width={760}
            styles={{ body: { maxHeight: '70vh', overflowY: 'auto' } }}
        >
            <Form layout="vertical" requiredMark={false}>
                <form.Field name="codigoPaciente">
                    {(field) => {
                        const error = getFieldError(field.state.meta.errors)

                        return (
                            <Form.Item
                                label="Código de paciente"
                                validateStatus={error ? 'error' : undefined}
                                help={error || undefined}
                            >
                                <Input
                                    placeholder="PAC-00001"
                                    value={field.state.value}
                                    onChange={(event) =>
                                        field.handleChange(event.target.value)
                                    }
                                    onBlur={field.handleBlur}
                                    disabled={formDisabled || isEditing}
                                    autoFocus={!isEditing}
                                />
                            </Form.Item>
                        )
                    }}
                </form.Field>

                <SectionTitle>Identificación</SectionTitle>
                <Divider style={{ margin: '8px 0 16px' }} />

                <Row gutter={16}>
                    <Col xs={24} md={12}>
                        <form.Field name="nombres">
                            {(field) => {
                                const error = getFieldError(field.state.meta.errors)

                                return (
                                    <Form.Item
                                        label="Nombres"
                                        validateStatus={error ? 'error' : undefined}
                                        help={error || undefined}
                                    >
                                        <Input
                                            placeholder="Nombres"
                                            value={field.state.value}
                                            onChange={(event) =>
                                                field.handleChange(event.target.value)
                                            }
                                            onBlur={field.handleBlur}
                                            disabled={formDisabled}
                                            autoFocus={isEditing}
                                        />
                                    </Form.Item>
                                )
                            }}
                        </form.Field>
                    </Col>
                    <Col xs={24} md={6}>
                        <form.Field name="apellidoPaterno">
                            {(field) => {
                                const error = getFieldError(field.state.meta.errors)

                                return (
                                    <Form.Item
                                        label="Apellido paterno"
                                        validateStatus={error ? 'error' : undefined}
                                        help={error || undefined}
                                    >
                                        <Input
                                            value={field.state.value}
                                            onChange={(event) =>
                                                field.handleChange(event.target.value)
                                            }
                                            onBlur={field.handleBlur}
                                            disabled={formDisabled}
                                        />
                                    </Form.Item>
                                )
                            }}
                        </form.Field>
                    </Col>
                    <Col xs={24} md={6}>
                        <form.Field name="apellidoMaterno">
                            {(field) => {
                                const error = getFieldError(field.state.meta.errors)

                                return (
                                    <Form.Item
                                        label="Apellido materno"
                                        validateStatus={error ? 'error' : undefined}
                                        help={error || undefined}
                                    >
                                        <Input
                                            value={field.state.value}
                                            onChange={(event) =>
                                                field.handleChange(event.target.value)
                                            }
                                            onBlur={field.handleBlur}
                                            disabled={formDisabled}
                                        />
                                    </Form.Item>
                                )
                            }}
                        </form.Field>
                    </Col>
                </Row>

                <Row gutter={16}>
                    <Col xs={24} md={8}>
                        <form.Field name="tipoDocumentoId">
                            {(field) => {
                                const error = getFieldError(field.state.meta.errors)

                                return (
                                    <Form.Item
                                        label="Tipo de documento"
                                        validateStatus={error ? 'error' : undefined}
                                        help={error || undefined}
                                    >
                                        <Select
                                            placeholder="Seleccione"
                                            options={tipoDocumentoOptions}
                                            value={field.state.value || undefined}
                                            onChange={(value) => field.handleChange(value)}
                                            onBlur={field.handleBlur}
                                            disabled={formDisabled}
                                            loading={loadingCatalogos}
                                        />
                                    </Form.Item>
                                )
                            }}
                        </form.Field>
                    </Col>
                    <Col xs={24} md={6}>
                        <form.Field name="numeroDocumento">
                            {(field) => {
                                const error = getFieldError(field.state.meta.errors)

                                return (
                                    <Form.Item
                                        label="Número"
                                        validateStatus={error ? 'error' : undefined}
                                        help={error || undefined}
                                    >
                                        <Input
                                            value={field.state.value}
                                            onChange={(event) =>
                                                field.handleChange(event.target.value)
                                            }
                                            onBlur={field.handleBlur}
                                            disabled={formDisabled}
                                        />
                                    </Form.Item>
                                )
                            }}
                        </form.Field>
                    </Col>
                    <Col xs={24} md={4}>
                        <form.Field name="complementoDocumento">
                            {(field) => {
                                const error = getFieldError(field.state.meta.errors)

                                return (
                                    <Form.Item
                                        label="Complemento"
                                        validateStatus={error ? 'error' : undefined}
                                        help={error || undefined}
                                    >
                                        <Input
                                            placeholder="Opcional"
                                            value={field.state.value}
                                            onChange={(event) =>
                                                field.handleChange(event.target.value)
                                            }
                                            onBlur={field.handleBlur}
                                            disabled={formDisabled}
                                        />
                                    </Form.Item>
                                )
                            }}
                        </form.Field>
                    </Col>
                    <Col xs={24} md={6}>
                        <form.Field name="extensionDocumentoId">
                            {(field) => {
                                const error = getFieldError(field.state.meta.errors)

                                return (
                                    <Form.Item
                                        label="Extensión"
                                        validateStatus={error ? 'error' : undefined}
                                        help={error || undefined}
                                    >
                                        <Select
                                            allowClear
                                            placeholder="Opcional"
                                            options={extensionDocumentoOptions}
                                            value={field.state.value}
                                            onChange={(value) =>
                                                field.handleChange(value ?? undefined)
                                            }
                                            onBlur={field.handleBlur}
                                            disabled={formDisabled}
                                            loading={loadingCatalogos}
                                        />
                                    </Form.Item>
                                )
                            }}
                        </form.Field>
                    </Col>
                </Row>

                <SectionTitle>Datos personales</SectionTitle>
                <Divider style={{ margin: '8px 0 16px' }} />

                <Row gutter={16}>
                    <Col xs={24} md={8}>
                        <form.Field name="fechaNacimiento">
                            {(field) => {
                                const error = getFieldError(field.state.meta.errors)

                                return (
                                    <Form.Item
                                        label="Fecha de nacimiento"
                                        validateStatus={error ? 'error' : undefined}
                                        help={error || undefined}
                                    >
                                        <Input
                                            type="date"
                                            value={field.state.value}
                                            onChange={(event) =>
                                                field.handleChange(event.target.value)
                                            }
                                            onBlur={field.handleBlur}
                                            disabled={formDisabled}
                                        />
                                    </Form.Item>
                                )
                            }}
                        </form.Field>
                    </Col>
                    <Col xs={24} md={8}>
                        <form.Field name="sexoId">
                            {(field) => {
                                const error = getFieldError(field.state.meta.errors)

                                return (
                                    <Form.Item
                                        label="Sexo"
                                        validateStatus={error ? 'error' : undefined}
                                        help={error || undefined}
                                    >
                                        <Select
                                            placeholder="Seleccione"
                                            options={sexoOptions}
                                            value={field.state.value || undefined}
                                            onChange={(value) => field.handleChange(value)}
                                            onBlur={field.handleBlur}
                                            disabled={formDisabled}
                                            loading={loadingCatalogos}
                                        />
                                    </Form.Item>
                                )
                            }}
                        </form.Field>
                    </Col>
                    <Col xs={24} md={8}>
                        <form.Field name="estadoCivilId">
                            {(field) => {
                                const error = getFieldError(field.state.meta.errors)

                                return (
                                    <Form.Item
                                        label="Estado civil"
                                        validateStatus={error ? 'error' : undefined}
                                        help={error || undefined}
                                    >
                                        <Select
                                            placeholder="Seleccione"
                                            options={estadoCivilOptions}
                                            value={field.state.value || undefined}
                                            onChange={(value) => field.handleChange(value)}
                                            onBlur={field.handleBlur}
                                            disabled={formDisabled}
                                            loading={loadingCatalogos}
                                        />
                                    </Form.Item>
                                )
                            }}
                        </form.Field>
                    </Col>
                </Row>

                <Row gutter={16}>
                    <Col xs={24} md={12}>
                        <form.Field name="ocupacionProfesion">
                            {(field) => {
                                const error = getFieldError(field.state.meta.errors)

                                return (
                                    <Form.Item
                                        label="Ocupación / profesión"
                                        validateStatus={error ? 'error' : undefined}
                                        help={error || undefined}
                                    >
                                        <Input
                                            placeholder="Opcional"
                                            value={field.state.value}
                                            onChange={(event) =>
                                                field.handleChange(event.target.value)
                                            }
                                            onBlur={field.handleBlur}
                                            disabled={formDisabled}
                                        />
                                    </Form.Item>
                                )
                            }}
                        </form.Field>
                    </Col>
                </Row>

                <SectionTitle>Contacto</SectionTitle>
                <Divider style={{ margin: '8px 0 16px' }} />

                <Row gutter={16}>
                    <Col xs={24} md={8}>
                        <form.Field name="telefono">
                            {(field) => {
                                const error = getFieldError(field.state.meta.errors)

                                return (
                                    <Form.Item
                                        label="Teléfono"
                                        validateStatus={error ? 'error' : undefined}
                                        help={error || undefined}
                                    >
                                        <Input
                                            value={field.state.value}
                                            onChange={(event) =>
                                                field.handleChange(event.target.value)
                                            }
                                            onBlur={field.handleBlur}
                                            disabled={formDisabled}
                                        />
                                    </Form.Item>
                                )
                            }}
                        </form.Field>
                    </Col>
                    <Col xs={24} md={16}>
                        <form.Field name="direccion">
                            {(field) => {
                                const error = getFieldError(field.state.meta.errors)

                                return (
                                    <Form.Item
                                        label="Dirección"
                                        validateStatus={error ? 'error' : undefined}
                                        help={error || undefined}
                                    >
                                        <Input
                                            value={field.state.value}
                                            onChange={(event) =>
                                                field.handleChange(event.target.value)
                                            }
                                            onBlur={field.handleBlur}
                                            disabled={formDisabled}
                                        />
                                    </Form.Item>
                                )
                            }}
                        </form.Field>
                    </Col>
                </Row>
            </Form>
        </Modal>
    )
}
