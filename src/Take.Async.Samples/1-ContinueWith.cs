using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Take.Async.Samples
{
    [Sample(1, "Task.ContinueWith usage")]
    public class ContinueWith : ISample
    {
        public void Run()
        {
            var semaphore = new Semaphore(0, 1);

            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Take", "1.0"));
            httpClient
                .GetAsync("https://api.github.com/users/takenet")
                .ContinueWith(t =>                    
                    t.Result.Content.ReadAsStringAsync()
                        .ContinueWith(s =>
                        {
                            Console.WriteLine(s.Result);
                            semaphore.Release();
                        }));


            semaphore.WaitOne();
        }

        public Task RunAsync(CancellationToken cancellationToken)
        {
            Run();
            return Task.CompletedTask;
        }
    }
}
