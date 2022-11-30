[![NuGet Badge](https://buildstats.info/nuget/Marquitos.Schedulers)](https://www.nuget.org/packages/Marquitos.Schedulers/)

# Marquitos.Schedulers

A simple Scheduler engine for .Net Applications.

# Usage
To create a scheduled task first create a class that implements the IScheduledTask interface.
``` csharp
    public class Example1Task : IScheduledTask
    {
        public async Task ExecuteAsync(CancellationToken cancellationToken = default)
        {
            Console.WriteLine("Hello from Example1 Task!");

            await Task.CompletedTask;
        }
    }
```

Then register the scheduler service engine and the scheduled task on your services configuration:

``` csharp
...
    // Register the scheduler service engine
    builder.Services.AddSchedulerService();

    // Register an example scheduled task
    builder.Services.AddScheduledTask<Example1Task>(o => {
                o.Schedule = Cron.Minutely();
                o.IsEnabled = true;
            });
...
```
