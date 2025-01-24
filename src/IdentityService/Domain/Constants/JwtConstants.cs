namespace IdentityService.Domain.Constants;

public static class JwtConstants
{
    public static int TokenExpirationTimeInHours => 24;
    public static int RefreshTokenExpirationTimeInHours => 48;
}