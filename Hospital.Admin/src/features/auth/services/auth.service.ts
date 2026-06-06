import { get, post } from "../../../shared/api/http";
import { authEndpoints } from "../../../shared/api/endpoints";
import type { LoginFormValues } from "../schemas/login.schema";
import type { LoginResponse, RefreshTokenRequest, User } from "../types/auth.types";

export class AuthService {
    login(credentials: LoginFormValues) {
        return post<LoginResponse, LoginFormValues>(
            authEndpoints.login,
            credentials,
        )
    }

    refresh(
        data: RefreshTokenRequest,
    ) {
        return post<LoginResponse, RefreshTokenRequest>(
            authEndpoints.refresh,
            data,
        )
    }

    logout() {
        return post<void, void>(
            authEndpoints.logout,
            undefined,
        )
    }

    me() {
        return get<User>(
            authEndpoints.me,
        )
    }
}

export const authService = new AuthService()