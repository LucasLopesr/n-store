
using EasyNetQ;
using NStore.Core.Messages.Integration;
using Polly;
using RabbitMQ.Client.Exceptions;
using System;
using System.Threading.Tasks;

namespace NStore.MessageBus
{
    public class MessageBus : IMessageBus
    {
        private IBus bus;
        private IAdvancedBus advancedBus;
        private readonly string connectionString;

        public MessageBus(string connectionString)
        {
            this.connectionString = connectionString;
            TryConnect();
        }

        public bool IsConnected => bus?.IsConnected ?? false;

        public IAdvancedBus AdvancedBus => bus?.Advanced;

        public void Publish<T>(T message) where T : IntegrationEvent
        {
            TryConnect();
            bus.Publish(message);
        }

        public async Task PublishAsync<T>(T message) where T : IntegrationEvent
        {
            TryConnect();
            await bus.PublishAsync(message);
        }

        public void Subscribe<T>(string subscriptionId, Action<T> onMessage) where T : class
        {
            TryConnect();
            bus.Subscribe(subscriptionId, onMessage);
        }

        public void SubscribeAsync<T>(string subscriptionId, Func<T, Task> onMessage) where T : class
        {
            TryConnect();
            bus.SubscribeAsync(subscriptionId, onMessage);
        }

        public TResponse Request<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            return bus.Request<TRequest, TResponse>(request);
        }

        public async Task<TResponse> RequestAsync<TRequest, TResponse>(TRequest request)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            return await bus.RequestAsync<TRequest, TResponse>(request);
        }

        public IDisposable Respond<TRequest, TResponse>(Func<TRequest, TResponse> responder)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            return bus.Respond(responder);
        }

        public IDisposable RespondAsync<TRequest, TResponse>(Func<TRequest, Task<TResponse>> responder)
            where TRequest : IntegrationEvent
            where TResponse : ResponseMessage
        {
            TryConnect();
            return bus.RespondAsync(responder);
        }

        private void TryConnect() 
        {
            if (IsConnected) return;

            var policy = Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .WaitAndRetry(retryCount: 3, sleepDurationProvider: retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

            policy.Execute(() => 
            { 
                bus = RabbitHutch.CreateBus(connectionString);
                advancedBus = bus.Advanced;
                advancedBus.Disconnected += OnDisconnect;
            });
        }

        private void OnDisconnect(object send, EventArgs eventArgs) 
        {
            var policy = Policy.Handle<EasyNetQException>()
                .Or<BrokerUnreachableException>()
                .RetryForever();

            policy.Execute(TryConnect);
        }
        public void Dispose()
        {
            bus.Dispose();
        }
    }
}
