using MediatR;
using Validator.Domain;
using ElectronicVoting.Common.Helper;
using Validator.Domain.Table.ChangeView;
using Validator.Infrastructure.Repository;
using Validator.Domain.Handler.Command.Consensu.ChangeLeader;

namespace Validator.API.Handler.Command.PbftConsensus.ChangeLeader
{
    public class PreElectionPreparation : IRequest
    {
        public int Round { get; set; }
        public bool Decision { get; set; }
        public string TransactionId { get; set; }
        public bool IsReportingApprover { get; set; }
    }

    public class PreElectionPreparationHandler : IRequestHandler<PreElectionPreparation>
    {

        private readonly ApproverRepository _approverRepository;
        private readonly PreElectionVoteRepository _preElectionVoteRepository;

        public PreElectionPreparationHandler(ApproverRepository approverRepository, PreElectionVoteRepository preElectionVoteRepository)
        {
            _approverRepository = approverRepository;
            _preElectionVoteRepository = preElectionVoteRepository;
        }

        public async Task Handle(PreElectionPreparation request, CancellationToken cancellationToken)
        {

            Console.WriteLine("PreElectionPreparation");
            var user = Environment.GetEnvironmentVariable("CONTAINER_NAME");

            var approver = await _approverRepository.GetByName(user, cancellationToken);
            var validators = await _approverRepository.GetAllWithoutMe(cancellationToken);

            var preElectionVote = new PreElectionVote
            {
                Decision = true,
                Round = request.Round,
                ApproverId = approver.Id,
                TransactionId = request.TransactionId,
            };

            await _preElectionVoteRepository.AddAsync(preElectionVote, cancellationToken);
            await _preElectionVoteRepository.SaveAsync(cancellationToken);

            var preElectionVoteRecord = new PreElectionVoteRecord
            {
                Decision = true,
                Round = request.Round,
                ApproverId = approver.Id,
                TransactionId = request.TransactionId
            };

            foreach (var validator in validators)
                await HttpHelper.PostAsync<PreElectionVoteRecord>(validator.Address, Routes.PreElectionVoteRecord, preElectionVoteRecord, cancellationToken);

            if (request.IsReportingApprover)
            {
                request.IsReportingApprover = false;
                foreach (var validator in validators)
                    await HttpHelper.PostAsync<PreElectionPreparation>(validator.Address, Routes.PreElectionPreparation, request, cancellationToken);
            }

        }
    }
}
