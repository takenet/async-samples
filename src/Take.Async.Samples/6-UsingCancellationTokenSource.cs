using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Take.Async.Samples
{
    [Sample(6, "Using CancellationTokenSource")]
    public class UsingCancellationTokenSource : ISample
    {
        public async Task RunAsync(CancellationToken cancellationToken)
        {
            // Create a CancellationTokenSource and link it to the passed CancellationToken
            using (var cts = new CancellationTokenSource()) 
            using (var linkedCts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken, cts.Token))
            {
                Console.WriteLine("Press ENTER to STOP.");
                var count = 0;
                var executionTask = Task.Run(async () =>
                {
                    while (!linkedCts.IsCancellationRequested)
                    {
                        try
                        {
                            // Simulate an API call
                            await Task.Delay(1000, linkedCts.Token);
                            Console.WriteLine($"Executed {++count} times");
                        }
                        catch (OperationCanceledException) when (linkedCts.IsCancellationRequested)
                        {
                            Console.WriteLine("Execution stop requested");
                            break;
                        }
                    }
                },
                linkedCts.Token);

                Console.ReadLine();
                cts.Cancel();

                await executionTask;
            }
        }
    }
}
