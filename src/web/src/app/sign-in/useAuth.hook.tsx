import { AuthService, SignInRequest, RegisterRequest } from '../core/http/authService'

export interface UseAuthType {
    register(email: string, password: string, passwordConfirm: string): Promise<void>;
    signIn(email: string, password: string): Promise<void>;
}

const selfWindow: Window = window;

export function useAuth(): UseAuthType {

    async function signIn(email: string, password: string) {
        try {
            const request: SignInRequest = { email, password };
            var response = await AuthService.signIn(request);
            localStorage.setItem("jwt", response);
            selfWindow.location = window.location.protocol + "//" + window.location.host + "/public";
        } catch (error) {
            console.log(error);
        }
    }

    async function register(email: string, password: string, passwordConfirm: string) {
        try {
            const request: RegisterRequest = { email, password, password_confirm: passwordConfirm };
            await AuthService.register(request);
        } catch (error) {
            console.log(error);
        }
    }

    return { register, signIn };
}
