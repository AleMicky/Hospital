import { Avatar, Breadcrumb, Button, Dropdown, Flex, Space, theme, Typography } from 'antd'
import {
    LogoutOutlined,
    MenuFoldOutlined,
    MenuUnfoldOutlined,
    UserOutlined,
} from '@ant-design/icons'
 
import { ThemeToggle } from '../theme-toggle/ThemeToggle'
import { useLogout, useMe } from '../../../../features/auth/hooks/auth.hooks'

const { Text } = Typography

type AppHeaderProps = {
    collapsed: boolean
    isMobile: boolean
    onToggleSidebar: () => void
}

export function AppHeader({ collapsed, isMobile, onToggleSidebar }: AppHeaderProps) {
 
    const logout = useLogout()
    const { data: user } = useMe()

    const { token } = theme.useToken()

    return (
        <header className="admin-header">
            <Flex align="center" gap={16} className="admin-header__left">
                <Button
                    type="text"
                    className="admin-header__toggle"
                    icon={collapsed ? <MenuUnfoldOutlined /> : <MenuFoldOutlined />}
                    onClick={onToggleSidebar}
                    aria-label={collapsed ? 'Abrir menú' : 'Cerrar menú'}
                />

                <Breadcrumb
                    className="admin-header__breadcrumb"
                    items={[
                        { title: 'Hospital Admin' },
                        { title: 'Panel' },
                    ]}
                />
            </Flex>

            <Flex align="center" gap={4} className="admin-header__actions">
                <ThemeToggle />

                <Dropdown
                    trigger={['click']}
                    menu={{
                        items: [
                            {
                                key: 'profile',
                                icon: <UserOutlined />,
                                label: 'Mi perfil',
                            },
                            { type: 'divider' },
                            {
                                key: 'logout',
                                icon: <LogoutOutlined />,
                                label: 'Cerrar sesión',
                                danger: true,
                                onClick: logout,
                            },
                        ],
                    }}
                >
                    <Space className="admin-header__user" style={{ cursor: 'pointer' }}>
                        <Avatar
                            size={36}
                            style={{ backgroundColor: token.colorPrimary }}
                            icon={<UserOutlined />}
                        />
                        {!isMobile && <Text strong>{user?.nombreCompleto}</Text>}
                    </Space>
                </Dropdown>
            </Flex>
        </header>
    )
}
