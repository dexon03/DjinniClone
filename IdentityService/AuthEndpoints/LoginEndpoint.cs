using FastEndpoints;
using IdentityService.Domain.Contracts;
using IdentityService.Domain.Dto;

namespace IdentityService.AuthEndpoints;

public class LoginEndpoint : Endpoint<LoginRequest, JwtResponse>
{
    private readonly IAuthService _authService;

    public LoginEndpoint(IAuthService authService)
    {
        _authService = authService;
    }

    public override void Configure()
    {
        Post("/api/auth/login");
        AllowAnonymous();
    } 
    
    public override async Task HandleAsync(LoginRequest request, CancellationToken cancellationToken)
    {
        var response = await _authService.LoginAsync(request, cancellationToken);
        await SendAsync(response, 200, cancellationToken);
    }
}

