using FastEndpoints;
using IdentityService.Domain.Contracts;
using IdentityService.Domain.Dto;

namespace IdentityService.AuthEndpoints;

public class RefreshEndpoint : Endpoint<string,JwtResponse>
{
    private readonly IAuthService _authService;

    public RefreshEndpoint(IAuthService authService)
    {
        _authService = authService;
    }

    public override void Configure()
    {
        Post("/api/auth/refresh");
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(string refreshToken, CancellationToken cancellationToken)
    {
        var response = await _authService.RefreshTokenAsync(refreshToken, cancellationToken);
        await SendAsync(response, 200, cancellationToken);
    }
}