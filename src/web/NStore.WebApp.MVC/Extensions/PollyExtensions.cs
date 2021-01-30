using Polly;
using Polly.Extensions.Http;
using Polly.Retry;
using System;
using System.Net.Http;

namespace NStore.WebApp.MVC.Extensions
{
    public class PollyExtensions
    {
        public static AsyncRetryPolicy<HttpResponseMessage> EsperarTentar() 
        {
            return HttpPolicyExtensions.HandleTransientHttpError()
                   .WaitAndRetryAsync(new[]
                   {
                        TimeSpan.FromSeconds(1),
                        TimeSpan.FromSeconds(5),
                        TimeSpan.FromSeconds(10)
                   }, (outcome, timespan, retryCount, context) =>
                   {
                       Console.WriteLine($"Número de tentativas: {retryCount}");
                   });

        }
    }
}
