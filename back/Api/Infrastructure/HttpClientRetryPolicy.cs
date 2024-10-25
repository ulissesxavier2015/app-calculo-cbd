using Polly;
using Polly.Extensions.Http;
using Polly.Timeout;

namespace Api.Infrastructure
{
    public static class HttpClientRetryPolicy
    {
        /// <summary>
        ///  É adicionado uma política para tentar 3 vezes com uma repetição exponencial, começando em um segundo.
        /// </summary>
        /// <returns></returns>        
        public static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
                .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                .Or<TimeoutRejectedException>()
                .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(1, retryAttempt)));
        }
    }
}
