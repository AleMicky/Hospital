import { Menu } from 'antd'
import type { MenuProps } from 'antd'
import {
    ApartmentOutlined,
    AuditOutlined,
    ExperimentOutlined,
    IdcardOutlined,
    MedicineBoxOutlined,
    NodeIndexOutlined,
    UserOutlined,
} from '@ant-design/icons'

import type { CatalogoClinicoSection } from '../types/catalogo-clinico.types'

type CatalogoClinicoSidebarProps = {
    activeSection: CatalogoClinicoSection
    onSectionChange: (section: CatalogoClinicoSection) => void
}

type MenuItem = Required<MenuProps>['items'][number]

const menuItems: MenuItem[] = [
    {
        type: 'group',
        label: 'Organización clínica',
        children: [
            {
                key: 'jerarquia',
                icon: <NodeIndexOutlined />,
                label: 'Áreas y servicios',
            },
        ],
    },
    {
        type: 'group',
        label: 'Recursos humanos',
        children: [
            {
                key: 'especialidades',
                icon: <MedicineBoxOutlined />,
                label: 'Especialidades',
            },
            {
                key: 'profesiones',
                icon: <UserOutlined />,
                label: 'Profesiones',
            },
            {
                key: 'cargos',
                icon: <IdcardOutlined />,
                label: 'Cargos',
            },
        ],
    },
    {
        type: 'group',
        label: 'Atención',
        children: [
            {
                key: 'tipos-atencion',
                icon: <AuditOutlined />,
                label: 'Tipos de atención',
            },
        ],
    },
]

const sectionMeta: Record<
    CatalogoClinicoSection,
    { title: string; description: string; icon: React.ReactNode }
> = {
    jerarquia: {
        title: 'Estructura organizacional',
        description: 'Áreas, departamentos, servicios y prestaciones médicas.',
        icon: <ApartmentOutlined />,
    },
    especialidades: {
        title: 'Especialidades médicas',
        description: 'Áreas de especialización del personal clínico.',
        icon: <MedicineBoxOutlined />,
    },
    profesiones: {
        title: 'Profesiones',
        description: 'Profesiones u oficios del personal de salud.',
        icon: <UserOutlined />,
    },
    cargos: {
        title: 'Cargos',
        description: 'Puestos y roles dentro de la institución.',
        icon: <IdcardOutlined />,
    },
    'tipos-atencion': {
        title: 'Tipos de atención',
        description: 'Clasificación de modalidades de atención clínica.',
        icon: <ExperimentOutlined />,
    },
}

export function CatalogoClinicoSidebar({
    activeSection,
    onSectionChange,
}: CatalogoClinicoSidebarProps) {
    return (
        <nav className="catalogo-clinico-sidebar" aria-label="Secciones del catálogo clínico">
            <Menu
                mode="inline"
                selectedKeys={[activeSection]}
                items={menuItems}
                onClick={({ key }) => onSectionChange(key as CatalogoClinicoSection)}
                className="catalogo-clinico-sidebar__menu"
            />
        </nav>
    )
}

export function getSectionMeta(section: CatalogoClinicoSection) {
    return sectionMeta[section]
}
