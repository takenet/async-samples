using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Take.Async.Samples
{
    [Sample(3, "Using Task.Run")]
    public class TaskRun : ISample
    {
        public async Task RunAsync(CancellationToken cancellationToken)
        {
            Console.WriteLine("How many times to execute? ");
            if (!int.TryParse(Console.ReadLine(), out var count))
            {
                count = 3;
            }

            Console.WriteLine($"Executing {count} times");

            await Task.Run(async () =>
            {
                for (int i = 0; i < count; i++)
                {
                    // Simulate an API call
                    await Task.Delay(1000, cancellationToken);
                    Console.WriteLine($"Executed {i + 1} times");
                }
            },
            cancellationToken);
        }
    }
}
