export interface JwtResponse{
    accessToken: string;
    refreshToken: string;
    tokenType: string;
    error: string;
}