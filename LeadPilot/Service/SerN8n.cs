using System.Text.Json;

namespace LeadPilot.Service
{
    public class SerN8n
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;
        private readonly string baseURL;
        private readonly string secret;
        public SerN8n(HttpClient httpClient, IConfiguration config) 
        {
            _httpClient = httpClient;
            _config = config;
            baseURL = _config["N8N:WebhookBaseURL"];
            secret = _config["N8N:WebhookSecret"];
        }

        public async Task NotifyLeadCreation(object request)
        {
            var webhookRequest=new HttpRequestMessage(HttpMethod.Post, baseURL+ "/lead-created");

            webhookRequest.Headers.Add("x-leadpilot-secret", secret);

            webhookRequest.Content = new StringContent(
                JsonSerializer.Serialize(request),
                System.Text.Encoding.UTF8,
                "application/json"
                );

            var response = await _httpClient.SendAsync(webhookRequest);
            response.EnsureSuccessStatusCode();
        }
    }
}
