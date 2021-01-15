using System.Threading.Tasks;
using Couriers.Contracts;
using MassTransit;
using MassTransit.Courier;
using Microsoft.Extensions.Options;

namespace Couriers
{
    public class RequestCourier :
        RoutingSlipRequestProxy<Request>
    {
        private readonly IOptionsMonitor<RequestCourierOptions> _options;

        public RequestCourier(IOptionsMonitor<RequestCourierOptions> options)
        {
            _options = options;
        }

        public RequestCourierOptions Options => _options.CurrentValue;

        protected override async Task BuildRoutingSlip(RoutingSlipBuilder builder, ConsumeContext<Request> request)
        {
            builder.AddActivity(Options.ActivityOne.Name, Options.ActivityOne.ExecuteUri);
            builder.AddActivity(Options.ActivityTwo.Name, Options.ActivityTwo.ExecuteUri);
        }
    }
}