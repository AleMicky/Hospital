import { createFileRoute } from '@tanstack/react-router'
import { RolesView } from '../../../features/roles/views/RolesView'
import { requireAdmin } from '../../../shared/utils/auth-guards'

export const Route = createFileRoute('/_admin/roles/')({
  beforeLoad: () => {
    requireAdmin()
  },
  component: RolesView,
})

 
