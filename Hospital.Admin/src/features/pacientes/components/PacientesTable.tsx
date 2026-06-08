import { useMemo } from 'react'
import {
    createColumnHelper,
    type ColumnDef,
} from '@tanstack/react-table'
import { Button, Popconfirm, Space, Tag, Typography } from 'antd'
import { DeleteOutlined, EditOutlined } from '@ant-design/icons'

import { AppDataTable } from '../../../shared/components/ui/data-table/AppDataTable'
import type { Paciente } from '../types/paciente.types'

const { Text } = Typography

type PacientesTableProps = {
    pacientes: Paciente[]
    loading: boolean
    total: number
    page: number
    pageSize: number
    onPageChange: (page: number, pageSize: number) => void
    onEdit: (paciente: Paciente) => void
    onDelete: (paciente: Paciente) => void
    deletingId: number | null
}

const columnHelper = createColumnHelper<Paciente>()

export function PacientesTable({
    pacientes,
    loading,
    total,
    page,
    pageSize,
    onPageChange,
    onEdit,
    onDelete,
    deletingId,
}: PacientesTableProps) {
    const columns = useMemo(
        () => [
            columnHelper.accessor('codigoPaciente', {
                header: 'Código',
                size: 110,
                cell: ({ getValue }) => (
                    <Text code style={{ fontSize: 12 }}>
                        {getValue()}
                    </Text>
                ),
            }),
            columnHelper.accessor('nombreCompleto', {
                header: 'Paciente',
                cell: ({ row }) => (
                    <div>
                        <Text strong>{row.original.nombreCompleto}</Text>
                        <Text type="secondary" style={{ display: 'block', fontSize: 12 }}>
                            {row.original.tipoDocumento}: {row.original.documentoCompleto}
                        </Text>
                    </div>
                ),
            }),
            columnHelper.accessor('edad', {
                header: 'Edad',
                size: 72,
                meta: { align: 'center', headerAlign: 'center' },
                cell: ({ getValue }) => `${getValue()} años`,
            }),
            columnHelper.accessor('sexo', {
                header: 'Sexo',
                size: 100,
            }),
            columnHelper.accessor('telefono', {
                header: 'Teléfono',
                size: 130,
                cell: ({ getValue }) => getValue() || '—',
            }),
            columnHelper.accessor('activo', {
                header: 'Estado',
                size: 110,
                cell: ({ getValue }) => (
                    <Tag color={getValue() ? 'success' : 'default'}>
                        {getValue() ? 'Activo' : 'Inactivo'}
                    </Tag>
                ),
            }),
            columnHelper.display({
                id: 'actions',
                header: 'Acciones',
                size: 120,
                meta: {
                    align: 'right',
                    headerAlign: 'right',
                },
                cell: ({ row }) => {
                    const paciente = row.original

                    return (
                        <Space size="small">
                            <Button
                                type="text"
                                icon={<EditOutlined />}
                                aria-label={`Editar ${paciente.nombreCompleto}`}
                                onClick={() => onEdit(paciente)}
                            />
                            <Popconfirm
                                title="Desactivar paciente"
                                description={`¿Desea desactivar al paciente "${paciente.nombreCompleto}"?`}
                                okText="Desactivar"
                                cancelText="Cancelar"
                                okButtonProps={{
                                    danger: true,
                                    loading: deletingId === paciente.id,
                                }}
                                disabled={!paciente.activo}
                                onConfirm={() => onDelete(paciente)}
                            >
                                <Button
                                    type="text"
                                    danger
                                    icon={<DeleteOutlined />}
                                    aria-label={`Desactivar ${paciente.nombreCompleto}`}
                                    loading={deletingId === paciente.id}
                                    disabled={!paciente.activo}
                                />
                            </Popconfirm>
                        </Space>
                    )
                },
            }),
        ] as ColumnDef<Paciente, any>[],
        [onEdit, onDelete, deletingId],
    )

    return (
        <AppDataTable
            data={pacientes}
            columns={columns}
            loading={loading}
            emptyText="No hay pacientes registrados."
            getRowId={(row) => String(row.id)}
            pagination={{
                page,
                pageSize,
                total,
                pageSizeOptions: [10, 20, 50],
                onChange: onPageChange,
            }}
        />
    )
}
