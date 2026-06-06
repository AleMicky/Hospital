import { create } from 'zustand'
import { persist } from 'zustand/middleware'

import type { LoginResponse, User } from '../features/auth/types/auth.types'

type AuthState = {
    accessToken: string | null
    refreshToken: string | null
    expiresAt: string | null
    user: User | null

    setAuth: (auth: LoginResponse) => void
    logout: () => void
}

export const authStore = create<AuthState>()(
    persist(
        (set) => ({
            accessToken: null,
            refreshToken: null,
            expiresAt: null,
            user: null,

            setAuth: (auth) =>
                set({
                    accessToken: auth.accessToken,
                    refreshToken: auth.refreshToken,
                    expiresAt: auth.expiresAt,
                    user: auth.user,
                }),

            logout: () =>
                set({
                    accessToken: null,
                    refreshToken: null,
                    expiresAt: null,
                    user: null,
                }),
        }),
        {
            name: 'hospital-auth',
        },
    ),
)