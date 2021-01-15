using System;
using System.Threading.Tasks;
using Couriers.Contracts;
using Couriers.Dependencies;
using MassTransit.Courier;

namespace Couriers.Activities
{
    public class ActivityTwo :
        IActivity<ActivityTwoArguments, ActivityLog>,
        IDisposable
    {
        private readonly DependencyTwo _depTwo;

        public ActivityTwo(){}
        
        public ActivityTwo(DependencyTwo depTwo)
        {
            _depTwo = depTwo;
        }

        public async Task<ExecutionResult> Execute(ExecuteContext<ActivityTwoArguments> context)
        {
            return context.Completed();
        }

        public async Task<CompensationResult> Compensate(CompensateContext<ActivityLog> context)
        {
            return context.Compensated();
        }

        public void Dispose()
        {
        }
    }
}