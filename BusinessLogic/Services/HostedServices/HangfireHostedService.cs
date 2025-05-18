using BusinessLogic.Interfaces;
using Hangfire;
using Microsoft.Extensions.Hosting;

namespace BusinessLogic.Services.HostedServices;

public class HangfireHostedService : IHostedService, IHangfireHostedService
{
    private readonly IBackgroundJobClient _backgroundJobs;

    public HangfireHostedService(IBackgroundJobClient backgroundJobs)
    {
        _backgroundJobs = backgroundJobs;
    }
    public void ScheduleHangfireTasks(CancellationToken cancellationToken)
    {
        var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Russian Standard Time");
        var options = new RecurringJobOptions
        {
            TimeZone = timeZone
        };

        RecurringJob.AddOrUpdate<BackgroundJob>("CheckDailyCompleteon", x => x.UpdateStatisticsAndLvLAsync(cancellationToken), "0 0 * * *", options); // Каждый день в 00:00
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        ScheduleHangfireTasks(cancellationToken);
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}
