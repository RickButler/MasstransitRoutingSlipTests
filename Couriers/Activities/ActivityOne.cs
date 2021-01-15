using System;
using System.Threading.Tasks;
using Couriers.Contracts;
using Couriers.Dependencies;
using MassTransit.Courier;

namespace Couriers.Activities
{
    public class ActivityOne :
        IActivity<ActivityOneArguments, ActivityLog>,
        IDisposable
    {
        private readonly DependencyOne _depOne;

        public ActivityOne()
        {
        }

        public ActivityOne(DependencyOne depOne)
        {
            _depOne = depOne;
        }

        public async Task<ExecutionResult> Execute(ExecuteContext<ActivityOneArguments> context)
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