using Microsoft.Extensions.Options;
using OutboxHandler.Contracts.Dtos;
using OutboxHandler.Contracts.Enums;
using OutboxHandler.Interfaces;
using OutboxHandler.Services.Outbox.Settings;
using System.Text;
using System.Text.Json;


namespace OutboxHandler.Services.Outbox;

internal class OutboxService : IOutboxService
{
    private readonly ILogger<OutboxService> _logger;

    private readonly IHttpClientFactory _httpClientFactory;

    private readonly ApiSettings _settings;

    public OutboxService(
        ILogger<OutboxService> logger,
        IHttpClientFactory httpClientFactory,
        IOptions<ApiSettings> settings
    )
    {
        _logger = logger;

        _httpClientFactory = httpClientFactory;

        _settings = settings.Value ?? throw new ArgumentNullException($"{nameof(settings)} of type {typeof(IOptions<ApiSettings>)}");
    }

    public async Task<OutboxMessageDto?> NextMessagesAsync(MessageType messageType, CancellationToken cancellationToken)
    {
        using var httpClient = _httpClientFactory.CreateClient();

        ConfigureHttpClient(httpClient);

        try
        {
            var endpoint = $"{_settings.EndPoints.NextMessageEndPoint}?messageType={messageType}";

            var nextMessagesResponse = await httpClient.PostAsync(endpoint, null, cancellationToken);

            nextMessagesResponse.EnsureSuccessStatusCode();

            if (nextMessagesResponse.StatusCode == System.Net.HttpStatusCode.NoContent)
            {
                return null;
            }

            var outboxMessage = await nextMessagesResponse.Content.ReadFromJsonAsync<OutboxMessageDto>(cancellationToken);

            return outboxMessage;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, null);
        }

        return null;
    }

    public async Task<bool> SetMessageProcessedAsync(Guid messageId, bool isSuccess, string? error, CancellationToken cancellationToken)
    {
        using var httpClient = _httpClientFactory.CreateClient();

        ConfigureHttpClient(httpClient);

        try
        {
            if (isSuccess)
            {
                return await SetMessageSuccessAsync(httpClient, messageId, cancellationToken);
            }
            else
            {
                return await SetMessageErrorAsync(httpClient, messageId, error, cancellationToken);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, null);
        }

        return false;
    }

    private void ConfigureHttpClient(HttpClient httpClient)
    {
        httpClient.BaseAddress = new Uri(_settings.Address);

        httpClient.DefaultRequestHeaders.Add(_settings.ApiKeyName, _settings.ApiKeyValue);
    }

    private async Task<bool> SetMessageSuccessAsync(HttpClient httpClient, Guid messageId, CancellationToken cancellationToken)
    {
        var endpoint = string.Format(_settings.EndPoints.MassageSuccessEndPoint, messageId);

        var setMessageProcessedResponse = await httpClient.PutAsync(endpoint, null, cancellationToken);

        return setMessageProcessedResponse.IsSuccessStatusCode;
    }

    private async Task<bool> SetMessageErrorAsync(HttpClient httpClient, Guid messageId, string? error, CancellationToken cancellationToken)
    {
        var endpoint = string.Format(_settings.EndPoints.MassageErrorEndPoint, messageId);

        var content = GetErrorContent(error);

        var setMessageProcessedResponse = await httpClient.PutAsync(endpoint, content, cancellationToken);

        return setMessageProcessedResponse.IsSuccessStatusCode;
    }

    private HttpContent? GetErrorContent(string? error)
    {
        if (string.IsNullOrWhiteSpace(error))
        {
            return null;
        }

        var body = JsonSerializer.Serialize(new
        {
            Error = error
        });

        return new StringContent(body, Encoding.UTF8, "application/json");
    }
}
