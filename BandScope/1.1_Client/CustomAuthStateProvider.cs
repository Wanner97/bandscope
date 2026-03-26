using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BandScope.Client
{
    public class CustomAuthStateProvider : AuthenticationStateProvider
    {
        private readonly NotificationService _notificationService;
        private readonly ILocalStorageService _localStorageService;

        public CustomAuthStateProvider(NotificationService notificationService, ILocalStorageService localStorageService)
        {
            _notificationService = notificationService;
            _localStorageService = localStorageService;
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await _localStorageService.GetItemAsync<string>("authToken");

            if (string.IsNullOrWhiteSpace(token))
            {
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var handler = new JwtSecurityTokenHandler();
            JwtSecurityToken jwtSecurityToken;

            try
            {
                jwtSecurityToken = handler.ReadJwtToken(token);
            }
            catch
            {
                await HandleLogoutAsync();
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }

            var expClaim = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "exp")?.Value;

            if (expClaim != null && long.TryParse(expClaim, out long expUnix))
            {
                var expirationTime = DateTimeOffset.FromUnixTimeSeconds(expUnix).UtcDateTime;
                if (expirationTime < DateTime.UtcNow)
                {
                    await HandleLogoutAsync();
                    return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
                }
            }

            var identity = new ClaimsIdentity(jwtSecurityToken.Claims, "jwtAuth");
            var user = new ClaimsPrincipal(identity);

            return new AuthenticationState(user);
        }

        public void NotifyUserAuthenticationStateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        private async Task HandleLogoutAsync()
        {
            await _localStorageService.RemoveItemAsync("authToken");

            _notificationService.Notify(new NotificationMessage
            {
                Severity = NotificationSeverity.Warning,
                Summary = "Session expired",
                Detail = "Please log in again.",
                Duration = 4000
            });
        }
    }
}
