using ElectronicVoting.Common.Helper;
using MediatR;
using Validator.Domain;
using Validator.Domain.Handler.Command.Consensu.ChangeLeader;
using Validator.Infrastructure.Repository;

namespace Validator.API.Handler.Command.PbftConsensus.ChangeLeader
{
    //InitializatioElectionPreparationHandler
    public class ElectionPreparationHandler : IRequestHandler<ElectionPreparation>
    {
        
        private readonly IRoundRepository _roundRepository;
        private readonly ApproverRepository _approverRepository;

        public ElectionPreparationHandler(IRoundRepository roundRepository, ApproverRepository approverRepository)
        {
            _roundRepository = roundRepository;
            _approverRepository = approverRepository;
        }

        public async Task Handle(ElectionPreparation request, CancellationToken cancellationToken)
        {
            var localApproverName = Environment.GetEnvironmentVariable("CONTAINER_NAME");
            var localApprover = await _approverRepository.GetByName(localApproverName, cancellationToken);

            var selectedApproverId = await _roundRepository.GetNextApprover(cancellationToken);
            var approvers = await _approverRepository.GetAllAsync(cancellationToken);

            var electionVoteRecord = new ElectionVoteRecord()
            {
                ApproverId = localApprover.Id,
                SelectedApproverId = selectedApproverId,
                TransactionId = request.TransactionId
            };

            foreach (var approver in approvers)
                await HttpHelper.PostAsync<ElectionVoteRecord>(approver.Address, Routes.ElectionVoteRecord, electionVoteRecord, cancellationToken);
        }
    }
}
