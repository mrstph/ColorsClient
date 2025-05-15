using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Net;

namespace ColorsClient.Handlers;

public class TokenHandler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        var accessToken = await SecureStorage.GetAsync("access_token");
        if (!string.IsNullOrEmpty(accessToken))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        }

        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            var refreshToken = await SecureStorage.GetAsync("refresh_token");
            if (string.IsNullOrEmpty(refreshToken))
                return response;

            var refreshClient = new HttpClient();
            var refreshResponse = await refreshClient.PostAsJsonAsync("https://localhost:5001/api/auth/refresh-token", new
            {
                Token = accessToken,
                RefreshToken = refreshToken
            });

            if (!refreshResponse.IsSuccessStatusCode)
                return response;

            var result = await refreshResponse.Content.ReadFromJsonAsync<RefreshResult>();
            if (result == null) return response;

            await SecureStorage.SetAsync("access_token", result.Token);
            await SecureStorage.SetAsync("refresh_token", result.RefreshToken);

            var retryRequest = await CloneRequestAsync(request);
            retryRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.Token);

            return await base.SendAsync(retryRequest, cancellationToken);
        }

        return response;
    }

    private async Task<HttpRequestMessage> CloneRequestAsync(HttpRequestMessage request)
    {
        var clone = new HttpRequestMessage(request.Method, request.RequestUri);

        if (request.Content != null)
        {
            var memoryStream = new MemoryStream();
            await request.Content.CopyToAsync(memoryStream);
            memoryStream.Position = 0;
            clone.Content = new StreamContent(memoryStream);

            foreach (var h in request.Content.Headers)
                clone.Content.Headers.Add(h.Key, h.Value);
        }

        foreach (var header in request.Headers)
            clone.Headers.TryAddWithoutValidation(header.Key, header.Value);

        return clone;
    }

    private class RefreshResult
    {
        public string Token { get; set; }
        public string RefreshToken { get; set; }
    }
}

