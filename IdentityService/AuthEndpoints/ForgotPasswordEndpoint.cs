using FastEndpoints;
using IdentityService.Domain.Contracts;
using IdentityService.Domain.Dto;

namespace IdentityService.AuthEndpoints;

public class ForgotPasswordEndpoint : Endpoint<ForgotPasswordRequest>
{
    private readonly IAuthService _authService;

    public ForgotPasswordEndpoint(IAuthService authService)
    {
        _authService = authService;
    }
    
    public override void Configure()
    {
        Post("/api/auth/forgotPassword");
        AllowAnonymous();
    }
    
    public override async Task HandleAsync(ForgotPasswordRequest request, CancellationToken cancellationToken)
    {
        await _authService.ForgotPasswordAsync(request, cancellationToken);
        await SendOkAsync(cancellationToken);
    }
}