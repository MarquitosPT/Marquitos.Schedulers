using Marquitos.Schedulers;
using Marquitos.Schedulers.Extensions.Configuration;
using Marquitos.Test.API.ScheduledTasks;

namespace Marquitos.Test.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Register Marquitos Scheduler service
            builder.Services.AddSchedulerService();
            builder.Services.AddScheduledTask<Example1Task>(o => {
                o.Schedule = Cron.Minutely();
                o.IsEnabled = true;
            });
            builder.Services.AddScheduledTask<Example2Task>(o => {
                o.Schedule = Cron.Minutely();
                o.IsEnabled = false;
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}