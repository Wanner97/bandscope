using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace BandScope.Client.Handler
{
    public class AuthorizationHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorageService;

        public AuthorizationHandler(ILocalStorageService localStorageService)
        {
            _localStorageService = localStorageService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _localStorageService.GetItemAsync<string>("authToken");

            if (!string.IsNullOrWhiteSpace(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            return await base.SendAsync(request, cancellationToken);
        }
    }
}
