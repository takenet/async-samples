using System.Threading;
using System.Threading.Tasks;

namespace Take.Async.Samples
{
    public interface ISample
    {
        Task RunAsync(CancellationToken cancellationToken);
    }
}