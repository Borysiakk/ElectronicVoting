using Hangfire;
using MediatR;
using Validator.Infrastructure.Handler.Command.Election.Job;

namespace Validator.Infrastructure.Hangfire;

public class HangfireJobsInitializer
{
    public void InitializeJobs()
    {

        RecurringJob.AddOrUpdate<IMediator>("CommitConfirmedVotes", a => a.Send(new CommitConfirmedVotes(), CancellationToken.None), "1/1 * * * *");
    }
}

