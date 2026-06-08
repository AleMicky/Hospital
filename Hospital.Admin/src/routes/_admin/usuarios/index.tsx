import { createFileRoute } from '@tanstack/react-router'
import { UsuariosView } from '../../../features/usuarios/views/UsuariosView'
import { requireAdmin } from '../../../shared/utils/auth-guards'

export const Route = createFileRoute('/_admin/usuarios/')({
  beforeLoad: () => {
    requireAdmin()
  },
  component: UsuariosView,
})

 