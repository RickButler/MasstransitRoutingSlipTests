using System;
using System.Threading.Tasks;
using Couriers.Contracts;
using MassTransit;
using MassTransit.Courier;
using MassTransit.Courier.Contracts;

namespace Couriers
{
    public class ResponseCourier :
        RoutingSlipResponseProxy<Request, RequestResponse, RequestFaulted>
    {
        protected override async Task<RequestResponse> CreateResponseMessage(
            ConsumeContext<RoutingSlipCompleted> context, Request request)
        {
            return await context.Init<RequestResponse>(new { });
        }

        protected override Task<RequestFaulted> CreateFaultedResponseMessage(ConsumeContext<RoutingSlipFaulted> context, Request request, Guid requestId)
        {
            throw new NotImplementedException();
        }
    }
}