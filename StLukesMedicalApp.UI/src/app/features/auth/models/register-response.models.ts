export interface RegisterResponse {
    success: boolean;
    message: string;
    user?: {
        id: string;
        email: string;
    };
    token?: string;
}