using Hangfire;
using MediatR;

namespace Validator.Infrastructure.Hangfire;

public interface IBackgroundJobMediatorClient
{
    void Enqueue(IRequest request);
}
public class BackgroundJobMediatorClient : IBackgroundJobMediatorClient
{
    private IMediator _mediator;
    private IBackgroundJobClient _backgroundJobClient;

    public BackgroundJobMediatorClient(IMediator mediator, IBackgroundJobClient backgroundJobClient)
    {
        _mediator = mediator;
        _backgroundJobClient = backgroundJobClient;
    }

    public void Enqueue(IRequest request)
    {
        _backgroundJobClient.Enqueue(() => _mediator.Send(request, default));
    }
}
