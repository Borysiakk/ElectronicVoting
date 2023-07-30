using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table.ChangeLeader;
using Validator.Infrastructure.Repository.ChangeLeader;

namespace Validator.Test.Repository
{
    public class PreElectionVoteLocalCompletedHistory_Test
    {

        private DbContextOptions<ValidatorDbContext> _dbContextOptions
        = new DbContextOptionsBuilder<ValidatorDbContext>().UseInMemoryDatabase(databaseName: "PreElectionVoteLocalCompletedHistory_Test").Options;

        [Test]
        public async Task Add_AddElementToTable()
        {
            using var dbContext = new ValidatorDbContext(_dbContextOptions);
            var preElectionVoteLeaderCompletedHistoryRepository = new PreLeaderVoteChangeLeaderHistoryRepository(dbContext);

            var preElectionLocalVote = new PreLocalVoteChangeLeaderHistory()
            {
                Id = 1,
                PreElectionChangeLeaderId = Guid.NewGuid().ToString(),
            };

            var entity = await preElectionVoteLeaderCompletedHistoryRepository.Add(preElectionLocalVote, CancellationToken.None);

            Assert.NotNull(entity);
            Assert.IsNotNull(entity.Id);
            Assert.IsInstanceOf<PreLocalVoteChangeLeaderHistory>(entity);
        }
    }
}
