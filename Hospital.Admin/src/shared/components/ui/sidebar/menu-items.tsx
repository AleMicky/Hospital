import type { MenuProps } from 'antd'
import {
    DashboardOutlined,
    TeamOutlined,
    UserOutlined,
    SettingOutlined,
    MedicineBoxOutlined,
    SafetyCertificateOutlined,
    AppstoreOutlined,
} from '@ant-design/icons'

type MenuItem = NonNullable<MenuProps['items']>[number]

export type MenuGroup = {
    key: string
    label: string
    items: MenuItem[]
}

export const menuGroups: MenuGroup[] = [
    {
        key: 'general',
        label: 'General',
        items: [
            {
                key: '/',
                icon: <DashboardOutlined />,
                label: 'Dashboard',
            },
        ],
    },
    {
        key: 'clinical',
        label: 'Clínica',
        items: [
            {
                key: '/pacientes',
                icon: <TeamOutlined />,
                label: 'Pacientes',
            },
            {
                key: '/atenciones',
                icon: <MedicineBoxOutlined />,
                label: 'Atenciones',
            },
        ],
    },
    {
        key: 'administration',
        label: 'Administración',
        items: [
            {
                key: '/usuarios',
                icon: <UserOutlined />,
                label: 'Usuarios',
            },
            {
                key: '/roles',
                icon: <SafetyCertificateOutlined />,
                label: 'Roles y permisos',
            },
        ],
    },
    {
        key: 'configuration',
        label: 'Configuración',
        items: [
            {
                key: '/parametros',
                icon: <SettingOutlined />,
                label: 'Parámetros',
            },
            {
                key: '/catalogos',
                icon: <AppstoreOutlined />,
                label: 'Catálogos',
            },
        ],
    },
]

export function buildMenuItems(): MenuProps['items'] {
    return menuGroups.map((group) => ({
        type: 'group' as const,
        label: group.label,
        children: group.items,
    }))
}

export function buildFlatMenuItems(): MenuProps['items'] {
    return menuGroups.flatMap((group, index) => {
        const items = group.items.map((item) => {
            if (!item || typeof item !== 'object' || !('label' in item)) {
                return item
            }

            return {
                ...item,
                title: typeof item.label === 'string' ? item.label : group.label,
            }
        })

        if (index === 0) {
            return items
        }

        return [{ type: 'divider' as const }, ...items]
    })
}
