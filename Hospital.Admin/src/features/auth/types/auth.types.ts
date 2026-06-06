export type User = {
    id: number;
    userName: string;
    nombreCompleto: string;
    roles: string[];
};

export type LoginResponse = {
    accessToken: string;
    refreshToken: string;
    expiresAt: string;
    user: User;
};

export type RefreshTokenRequest = {
    accessToken: string
    refreshToken: string
}