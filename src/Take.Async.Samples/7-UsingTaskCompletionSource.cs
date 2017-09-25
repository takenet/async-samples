using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Take.Async.Samples
{
    [Sample(7, "Using TaskCompletionSource")]
    public class UsingTaskCompletionSource : ISample
    {
        public async Task RunAsync(CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<string>();
            var getNameTask = Task.Run(() =>
            {
                Console.WriteLine("Enter your name or press ENTER to cancel: ");
                var name = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(name))
                {
                    tcs.SetResult(name);
                }
                else
                {
                    tcs.SetCanceled();
                }
            },
            cancellationToken);

            try
            {
                var result = await tcs.Task;
                Console.WriteLine($"The result was '{result}'.");
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("The operation was canceled.");
            }
        }
    }
}
