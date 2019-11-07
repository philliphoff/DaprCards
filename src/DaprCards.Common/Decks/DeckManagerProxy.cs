using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace DaprCards.Decks
{
    public sealed class DeckManagerProxy : IDeckManager
    {
        public static IDeckManager CreateProxy()
        {
            return new DeckManagerProxy();
        }

        private DeckManagerProxy()
        {
        }

        #region IDeckManager Members

        public async Task<string> CreateRandomDeckAsync(CreateRandomDeckOptions options, CancellationToken cancellationToken = default)
        {
            using var client = new HttpClient();

            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue(MediaTypeNames.Application.Json));

            using var content = new StringContent(
                JsonSerializer.Serialize(options),
                Encoding.UTF8,
                MediaTypeNames.Application.Json);

            string url = GetDeckManagerInvocationUrl("decks/createRandomDeck");

            Console.WriteLine("URL: " + url);

            using var response = await client.PostAsync(url, content).ConfigureAwait(false);

            response.EnsureSuccessStatusCode();

            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            return JsonSerializer.Deserialize<string>(json);
        }

        #endregion

        private static int GetDaprPort()
        {
            string? portString = Environment.GetEnvironmentVariable("DAPR_HTTP_PORT");

            return Int32.TryParse(portString, out int port)
                ? port
                : 3500;
        }

        private static string GetDaprInvocationUrl(string appId, string method)
        {
            return $"http://localhost:{GetDaprPort()}/v1.0/invoke/{appId}/method/{method}";
        }

        private static string GetDeckManagerInvocationUrl(string method)
        {
            return GetDaprInvocationUrl("dapr-deck-manager", method);
        }
    }
}