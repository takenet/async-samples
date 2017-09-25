using System;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Take.Async.Samples
{
    class Program
    {
        static void Main(string[] args) => MainAsync(args).GetAwaiter().GetResult();

        static async Task MainAsync(string[] args)
        {
            var samples = Assembly
                .GetExecutingAssembly()
                .GetTypes()
                .Select(t => new
                {
                    Type = t,
                    Sample = t.GetCustomAttribute<SampleAttribute>()
                })
                .Where(s => s.Sample != null && typeof(ISample).IsAssignableFrom(s.Type))
                .OrderBy(s => s.Sample.Number)
                .ToDictionary(t => t.Sample.Number, t => t);

            foreach (var sample in samples)
            {
                Console.WriteLine($"{sample.Key} - {sample.Value.Sample.Name}");
            }

            while (true)
            {
                Console.WriteLine("Enter the sample number: ");
                if (!int.TryParse(Console.ReadLine(), out var number)) break;
                if (samples.TryGetValue(number, out var sampleAttribute))
                {
                    var sample = (ISample) Activator.CreateInstance(sampleAttribute.Type);
                    await sample.RunAsync(CancellationToken.None);
                }
                else
                {
                    Console.WriteLine("Invalid sample");
                }
            }

            Console.WriteLine("Press ENTER to EXIT.");
            Console.ReadLine();
        }
    }
}
