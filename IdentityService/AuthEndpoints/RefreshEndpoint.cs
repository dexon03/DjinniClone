using FastEndpoints;
using IdentityService.Domain.Contracts;
using IdentityService.Domain.Dto;

namespace IdentityService.AuthEndpoints;

public class RefreshEndpoint : Endpoint<RefreshTokenRequest,JwtResponse>
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
    
    public override async Task HandleAsync(RefreshTokenRequest request, CancellationToken cancellationToken)
    {
        var response = await _authService.RefreshTokenAsync(request.RefreshToken, cancellationToken);
        await SendAsync(response, 200, cancellationToken);
    }
}