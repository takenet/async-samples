using System;
using System.Threading;
using System.Threading.Tasks;

namespace Take.Async.Samples
{
    [Sample(5, "Using Task.WhenAny")]
    public class TaskWhenAny : ISample
    {
        public async Task RunAsync(CancellationToken cancellationToken)
        {
            var firstThingTask = DoFirstThingAsync(cancellationToken);
            var secondThingTask = DoSecondThingAsync(cancellationToken);
            var thirdThingTask = DoThirdThingAsync(cancellationToken);

            var completedTask = await Task.WhenAny(
                firstThingTask,
                secondThingTask,
                thirdThingTask);

            if (completedTask == firstThingTask)
            {
                Console.WriteLine($"The completed task was {nameof(firstThingTask)}");
            }
            else if (completedTask == secondThingTask)
            {
                Console.WriteLine($"The completed task was {nameof(secondThingTask)}");
            }
            else if (completedTask == thirdThingTask)
            {
                Console.WriteLine($"The completed task was {nameof(thirdThingTask)}");
            }
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