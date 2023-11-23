using MediatR;

namespace Validator.Application.Handler.Command.ChangeLeader
{
    public class LeadershipChangeAssessment :IRequest
    {

    }

    public class LeadershipChangeAssessmentHandler : IRequestHandler<LeadershipChangeAssessment>
    {
        public Task Handle(LeadershipChangeAssessment request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
