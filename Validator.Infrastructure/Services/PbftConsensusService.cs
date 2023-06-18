using ElectronicVoting.Common.Helper;
using ElectronicVoting.Infrastructure.Repository;
using ElectronicVoting.Validator.Infrastructure.Helper;
using Validator.Domain;
using Validator.Domain.Contract.Request;
using Validator.Domain.Handler.Command.Consensu;
using Validator.Domain.Models.Queue.Consensus.ChangeView;
using Validator.Domain.Queue.Consensus;
using Validator.Domain.Table;
using Validator.Domain.Table.ChangeView;
using Validator.Infrastructure.Repository;

namespace ElectronicVoting.Infrastructure.Services
{
    public interface IPbftConsensusService
    {
        public Task CommitAsync(PbftOperationConsensus operation, CancellationToken cancellationToken);
        public Task PrepareAsync(PbftOperationConsensus operation, CancellationToken cancellationToken);
        public Task PrePrepareAsync(PbftOperationConsensus operation, CancellationToken cancellationToken);
        public Task InitializationChangeView(PbftOperationConsensus operation, CancellationToken cancellationToken);
        public Task CommitInitializationChangeView(PbftOperationConsensus operation, CancellationToken cancellationToken);
        public Task PrepareInitializationChangeView(PbftOperationConsensus operation, CancellationToken cancellationToken);
    }

    public class PbftConsesusService : IPbftConsensusService
    {
        private readonly ApproverRepository _validatorRepository;
        private readonly IProofOfKnowledgeService _proofOfKnowledgeService;
        private readonly TransactionPendingRepository _transactionPendingRepository;
        private readonly InitializationChangeViewTransactionRepository _initializationChangeViewTransactionRepository;


        public PbftConsesusService(ApproverRepository validatorRepository, TransactionPendingRepository transactionPendingRepository, IProofOfKnowledgeService proofOfKnowledgeService, InitializationChangeViewTransactionRepository initializationChangeViewTransactionRepository)
        {
            _validatorRepository = validatorRepository;
            _proofOfKnowledgeService = proofOfKnowledgeService;
            _transactionPendingRepository = transactionPendingRepository;
            _initializationChangeViewTransactionRepository = initializationChangeViewTransactionRepository;
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

        public async Task PrepareInitializationChangeView(PbftOperationConsensus operation, CancellationToken cancellationToken)
        {
            Console.WriteLine("PrepareInitializationChangeView");

            var transactionId = Guid.NewGuid().ToString();
            var user = Environment.GetEnvironmentVariable("CONTAINER_NAME");

            var validators = await _validatorRepository.GetAllWithoutMe(cancellationToken);

            var item = ItemBodyHelper.DeserializeObject<ItemBodyInitializationChangeView>(operation.Body);

            var initializationChangeView = new InitializationChangeView()
            {
               Round = item.Round,
               TransactionId = transactionId,
            };

            var commitInitializationChangeView = new CommitInitializationChangeView()
            {
                Decision = true,
                UserName = user,
                Round = item.Round,
                TransactionId = transactionId
            };

            var commit = new InitializationChangeViewTransaction()
            {
                Decision = true,
                UserName = user,
                Round = item.Round,
                TransactionId = transactionId
            };

            foreach (var validator in validators)
                HttpHelper.PostAsync<InitializationChangeView>(validator.Address, Routes.InitializationChangeView, initializationChangeView, cancellationToken);

            foreach (var validator in validators)
                HttpHelper.PostAsync<CommitInitializationChangeView>(validator.Address, Routes.CommitInitializationChangeView, commitInitializationChangeView, cancellationToken);

            await _initializationChangeViewTransactionRepository.AddAsync(commit, cancellationToken);
            await _initializationChangeViewTransactionRepository.SaveAsync(cancellationToken);
        }


        public async Task InitializationChangeView(PbftOperationConsensus operation, CancellationToken cancellationToken)
        {
            Console.WriteLine("InitializationChangeView");
            var validators = await _validatorRepository.GetAllWithoutMe(cancellationToken);

            var user = Environment.GetEnvironmentVariable("CONTAINER_NAME");

            var item = ItemBodyHelper.DeserializeObject<ItemBodyInitializationChangeView>(operation.Body);

            var commitInitializationChangeView = new CommitInitializationChangeView()
            {
                UserName = user,
                Round = item.Round,
                Decision = item.Decision,
                TransactionId = item.TransactionId
            };

            var commit = new InitializationChangeViewTransaction()
            {
                Decision = true,
                UserName = user,
                Round = item.Round,
                TransactionId = item.TransactionId
            };

            foreach (var validator in validators)
                 HttpHelper.PostAsync<CommitInitializationChangeView>(validator.Address, Routes.CommitInitializationChangeView, commitInitializationChangeView, cancellationToken);

            await _initializationChangeViewTransactionRepository.AddAsync(commit, cancellationToken);
            await _initializationChangeViewTransactionRepository.SaveAsync(cancellationToken);
        }

        public async Task CommitInitializationChangeView(PbftOperationConsensus operation, CancellationToken cancellationToken)
        {
            Console.WriteLine("CommitInitializationChangeView");
            var item = ItemBodyHelper.DeserializeObject<ItemBodyCommitInitializationChangeView>(operation.Body);

            var commit = new InitializationChangeViewTransaction()
            {
                Round = item.Round,
                Decision = item.Decision,
                UserName = item.UserName,
                TransactionId = item.TransactionId
            };

            await _initializationChangeViewTransactionRepository.AddAsync(commit, cancellationToken);
            await _initializationChangeViewTransactionRepository.SaveAsync(cancellationToken);

        }
    }
}
