using System;
using System.Threading.Tasks;
using Couriers;
using Couriers.Activities;
using Couriers.Contracts;
using MassTransit;
using MassTransit.ExtensionsDependencyInjectionIntegration;
using MassTransit.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;

namespace Courier.Tests
{
    public class UnitTest1 : RoutingSlipProxyTestFixture<RequestCourier, ResponseCourier, Request, RequestResponse,
        RequestFaulted>
    {

        public Mock<IOptionsMonitor<RequestCourierOptions>> MockOptions = new Mock<IOptionsMonitor<RequestCourierOptions>>();
        
        protected override void ConfigureServices(IServiceCollection collection)
        {
            MockOptions.Setup(s => s.CurrentValue).Returns(new RequestCourierOptions()
            {
                ActivityOne = new ActivityConfig(){ Name = "ActivityOne", ExecuteUri =  new Uri("loopback://localhost/ActivityOne_execute")},
                ActivityTwo =  new ActivityConfig(){Name = "ActivityTwo", ExecuteUri =  new Uri("loopback://localhost/ActivityTwo_execute")}
            });
            
            collection.AddSingleton<IOptionsMonitor<RequestCourierOptions>>(MockOptions.Object);
        }

        protected override void ConfigureMassTransit(IServiceCollectionBusConfigurator configurator)
        {
            configurator.AddActivity<ActivityOne, ActivityOneArguments, ActivityLog>();
            configurator.AddActivity<ActivityTwo, ActivityTwoArguments, ActivityLog>();
            //configurator.AddActivity<ActivityTwo>(s => s.);
            configurator.AddRequestClient<Request>();
        }
        
        protected override void ConfigureReceiveEndpoint(IReceiveEndpointConfigurator configurator)
        {
            configurator.Consumer<ResponseCourier>();
        }

        [Test]
        public async Task TestMethod1()
        {
            using var scope = Provider.CreateScope();
            var requestClient = scope.ServiceProvider.GetRequiredService<IRequestClient<Request>>();
            var (response, fault) = await requestClient.GetResponse<RequestResponse, RequestFaulted>(new { });
          
                if (response.IsCompletedSuccessfully)
                {
                    var result = await response;
                }
            var d =response.Result.Message;
        }
    }
}