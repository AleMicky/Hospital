import { redirect } from '@tanstack/react-router'
import { authStore } from '../../stores/auth.store'

export function requireAuth() {

    const { accessToken, expiresAt } = authStore.getState()

    if (!accessToken ||
        !expiresAt ||
        new Date(expiresAt) <= new Date()
    ) {
        throw redirect({
            to: '/login',
        })
    }

}

export function redirectIfAuthenticated() {

    const { accessToken, expiresAt } = authStore.getState()

    if ( accessToken &&
        expiresAt &&
        new Date(expiresAt) > new Date()
    ) {
        throw redirect({
            to: '/',
        })
    }

}