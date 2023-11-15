using FastEndpoints;
using IdentityService.Domain.Contracts;
using IdentityService.Domain.Dto;

namespace IdentityService.AuthEndpoints;

public class RegisterEndpoint : Endpoint<RegisterRequest, TokenResponse>
{
    private readonly IAuthService _authService;

    public RegisterEndpoint(IAuthService authService)
    {
        _authService = authService;
    }
    public override void Configure()
    {
        Post("/api/auth/register");
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(RegisterRequest request, CancellationToken cancellationToken)
    {
        var response = await _authService.RegisterAsync(request, cancellationToken);
        await SendAsync(response, 200, cancellationToken);
    }
}