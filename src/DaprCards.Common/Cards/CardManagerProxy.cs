using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DaprCards.Cards
{
    public sealed class CardManagerProxy : ICardManager, IDisposable
    {
        public static CardManagerProxy CreateProxy()
        {
            string daprPort = Environment.GetEnvironmentVariable("DAPR_HTTP_PORT") ?? "3500";
            string daprUrl = $"http://localhost:{daprPort}/v1.0";
            string cardUrl = $"{daprUrl}/invoke/{Constants.AppIds.CardManager}/method/cards";

            return new CardManagerProxy(cardUrl);
        }

        private readonly HttpClient client;
        private readonly string endpoint;

        public CardManagerProxy(string endpoint)
        {
            this.endpoint = endpoint;

            this.client = new HttpClient();
            this.client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));
        }

        #region ICardManager Members

        public async Task<string> CreateCardAsync(CardDetails details, CancellationToken cancellationToken = default)
        {
            using var requestContent =
                new StringContent(
                    JsonSerializer.Serialize(details),
                    Encoding.UTF8,
                    MediaTypeNames.Application.Json);
                    
            using var response = await client.PostAsync(this.endpoint, requestContent, cancellationToken).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            string cardId = JsonSerializer.Deserialize<string>(responseBody);

            return cardId;
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            this.client.Dispose();
        }

        #endregion
    }
}