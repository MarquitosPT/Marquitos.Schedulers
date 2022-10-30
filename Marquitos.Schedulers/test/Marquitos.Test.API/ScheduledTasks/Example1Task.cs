using Marquitos.Schedulers;

namespace Marquitos.Test.API.ScheduledTasks
{
    public class Example1Task : IScheduledTask
    {
        public async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            Console.WriteLine("Hello from Example1 Task!");

            await Task.CompletedTask;
        }
    }
}
