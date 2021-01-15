using System.Threading.Tasks;
using MassTransit;
using MassTransit.Courier.Contracts;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;


namespace Courier.Tests
{
    public class RoutingSlipProxyTestFixture<TRequestProxy, TResponseProxy, TRequest, TResponse, TFault>
        where TRequestProxy : class, IConsumer<TRequest>, IConsumer
        where TResponseProxy : class, IConsumer<RoutingSlipCompleted>, IConsumer<RoutingSlipFaulted>
        where TRequest : class
        where TResponse : class
        where TFault : class
    {
        public IConsumerTestHarness<TRequestProxy> ConsumerRequestTestHarness;
        public IConsumerTestHarness<TResponseProxy> ConsumerResponseTestHarness;
        public ServiceProvider Provider;
        public ConsumerTestHarness<TRequestProxy> RequestProxyHarness;
        public IConsumer<TRequest> RequestProxyInstance;
        public IConsumer<RoutingSlipCompleted> ResponseProxyCompletedInstance;
        public IConsumer<RoutingSlipFaulted> ResponseProxyFaultedInstance;
        public InMemoryTestHarness TestHarness;

        [OneTimeSetUp]
        public async Task Setup()
        {
            var collection = new ServiceCollection()
                .AddMassTransitInMemoryTestHarness(cfg =>
                {
                    cfg.AddConsumer<TRequestProxy>();
                    cfg.AddConsumer<TResponseProxy>();
                    cfg.AddConsumerTestHarness<TRequestProxy>();
                    cfg.AddConsumerTestHarness<TResponseProxy>();
                    ConfigureMassTransit(cfg);
                });
            
            ConfigureServices(collection);
            Provider = collection.BuildServiceProvider(true);
            TestHarness = Provider.GetRequiredService<InMemoryTestHarness>();
            TestHarness.OnConfigureInMemoryBus += ConfigureInMemoryBus;
            TestHarness.OnConfigureInMemoryReceiveEndpoint += ConfigureInMemoryReceiveEndpoint;
            TestHarness.OnConfigureReceiveEndpoint += ConfigureReceiveEndpoint;

            await TestHarness.Start();

            ConsumerRequestTestHarness = Provider.GetRequiredService<IConsumerTestHarness<TRequestProxy>>();
            ConsumerResponseTestHarness = Provider.GetRequiredService<IConsumerTestHarness<TResponseProxy>>();
            //RequestProxyInstance = Provider.GetRequiredService<IConsumer<TRequest>>();
            //ResponseProxyCompletedInstance = Provider.GetRequiredService<IConsumer<RoutingSlipCompleted>>();
            //ResponseProxyFaultedInstance = Provider.GetRequiredService<IConsumer<RoutingSlipFaulted>>();
        }

        [OneTimeTearDown]
        public async Task Teardown()
        {
            try
            {
                await TestHarness.Stop();
            }
            finally
            {
                await Provider.DisposeAsync();
            }
        }

        protected virtual void ConfigureMassTransit(IServiceCollectionBusConfigurator configurator)
        {
        }

        protected virtual void ConfigureInMemoryBus(IInMemoryBusFactoryConfigurator configurator)
        {
        }

        protected virtual void ConfigureInMemoryReceiveEndpoint(IInMemoryReceiveEndpointConfigurator configurator)
        {
        }

        protected virtual void ConfigureReceiveEndpoint(IReceiveEndpointConfigurator configurator)
        {
        }

        protected virtual void ConfigureServices(IServiceCollection collection)
        {
        }
    }
}