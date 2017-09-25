using System;
using System.Threading;
using System.Threading.Tasks;

namespace Take.Async.Samples
{
    [Sample(4, "Using Task.WhenAll")]
    public class TaskWhenAll : ISample
    {
        public async Task RunAsync(CancellationToken cancellationToken)
        {
            await Task.WhenAll(
                DoFirstThingAsync(cancellationToken),
                DoSecondThingAsync(cancellationToken),
                DoThirdThingAsync(cancellationToken));

            Console.WriteLine("All things done!");
        }

        public Task DoFirstThingAsync(CancellationToken cancellationToken)
        {
            // NOTE: This method is not using ASYNC keyword
            Console.WriteLine("Doing the first thing...");
            return Task
                .Delay(2000, cancellationToken)
                .ContinueWith(t => Console.WriteLine("First thing is done!"), cancellationToken);
        }

        public async Task DoSecondThingAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Doing the second thing...");
            await Task.Delay(4000, cancellationToken); // Do something
            Console.WriteLine("Second thing is done!");
        }

        public async Task DoThirdThingAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("Doing the third thing...");
            await Task.Delay(6000, cancellationToken); // Do something
            Console.WriteLine("Third thing is done!");
        }
    }
}