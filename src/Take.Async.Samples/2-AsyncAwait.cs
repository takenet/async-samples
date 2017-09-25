using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Take.Async.Samples
{
    [Sample(2, "Using async/await keywords")]
    public class AsyncAwait : ISample
    {
        public async Task RunAsync(CancellationToken cancellationToken)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.UserAgent.Add(new ProductInfoHeaderValue("Take", "1.0"));
            var result = await httpClient.GetAsync("https://api.github.com/users/takenet", cancellationToken);
            Console.WriteLine(await result.Content.ReadAsStringAsync());                
        }
    }
}
