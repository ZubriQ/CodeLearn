using Microsoft.Extensions.Options;
using Quartz;

namespace CodeLearn.Infrastructure.Jobs;

public class FinishingTestingsAndSessionsBackgroundJobSetup : IConfigureOptions<QuartzOptions>
{
    public void Configure(QuartzOptions options)
    {
        const string jobKey = nameof(FinishingTestingsAndSessionsBackgroundJob);

        options
            .AddJob<FinishingTestingsAndSessionsBackgroundJob>(jobBuilder => jobBuilder.WithIdentity(jobKey))
            .AddTrigger(trigger => trigger
                .ForJob(jobKey)
                .WithSimpleSchedule(schedule => schedule
                    .WithIntervalInMinutes(5)
                    .RepeatForever()));
    }
}