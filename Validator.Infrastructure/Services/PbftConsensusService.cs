using ElectronicVoting.Common.Helper;
using ElectronicVoting.Infrastructure.Repository;
using ElectronicVoting.Validator.Domain;
using ElectronicVoting.Validator.Domain.Contract.Request;
using ElectronicVoting.Validator.Domain.Handler.Command.Consensu;
using ElectronicVoting.Validator.Domain.Queue.Consensus;
using ElectronicVoting.Validator.Domain.Table;
using ElectronicVoting.Validator.Infrastructure.Helper;

namespace ElectronicVoting.Infrastructure.Services
{
    public interface IPbftConsensusService
    {
        public Task CommitAsync(PbftOperationConsensus operation, CancellationToken cancellationToken);
        public Task PrepareAsync(PbftOperationConsensus operation, CancellationToken cancellationToken);
        public Task PrePrepareAsync(PbftOperationConsensus operation, CancellationToken cancellationToken);
    }

    public class PbftConsesusService : IPbftConsensusService
    {
        private readonly ApproverRepository _validatorRepository;
        private readonly IProofOfKnowledgeService _proofOfKnowledgeService;
        private readonly TransactionPendingRepository _transactionPendingRepository;

        public PbftConsesusService(ApproverRepository validatorRepository, TransactionPendingRepository transactionPendingRepository, IProofOfKnowledgeService proofOfKnowledgeService)
        {
            _validatorRepository = validatorRepository;
            _proofOfKnowledgeService = proofOfKnowledgeService;
            _transactionPendingRepository = transactionPendingRepository;
        }


        public async Task CommitAsync(PbftOperationConsensus operation, CancellationToken cancellationToken)
        {
            Console.WriteLine("CommitAsync");
            var item = ItemBodyHelper.DeserializeObject<ItemBodyCommit>(operation.Body);

            TransactionPending transaction = new TransactionPending() {
                Hash = item.Hash,
                TransactionId = item.TransactionId
            };


            await _transactionPendingRepository.AddAsync(transaction, cancellationToken);
            await _transactionPendingRepository.SaveAsync(cancellationToken);
        }

        public async Task PrepareAsync(PbftOperationConsensus operation, CancellationToken cancellationToken)
        {
            Console.WriteLine("PrepareAsync");

            var validators = await _validatorRepository.GetAllWithoutMe(cancellationToken);

            var item = ItemBodyHelper.DeserializeObject<ItemBodyPrepare>(operation.Body);

            var result = await _proofOfKnowledgeService.Validation(new ProofOfKnowledgeRequest {
                Voice = item.Voice,
                TransactionId = item.TransactionId
            });

            Commit commit = new Commit() {
                Hash = result.Hash,
                TransactionId = result.TransactionId
            };

            foreach (var validator in validators)
                await HttpHelper.PostAsync<Commit>(validator.Address, Routes.Commit, commit, cancellationToken);

        }

        public async Task PrePrepareAsync(PbftOperationConsensus operation, CancellationToken cancellationToken)
        {
            Console.WriteLine("PrePrepareAsync");

            var validators = _validatorRepository.GetAll();

            var item = ItemBodyHelper.DeserializeObject<ItemBodyPrePrepare>(operation.Body);

            var prepare = new Prepare() {
                Voice = item.Voice,
                TransactionId = item.TransactionId
            };

            foreach (var validator in validators)
                await HttpHelper.PostAsync<Prepare>(validator.Address, Routes.Prepare, prepare, cancellationToken);

        }
    }
}
