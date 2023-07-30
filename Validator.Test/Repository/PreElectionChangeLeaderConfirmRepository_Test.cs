using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table.ChangeLeader;
using Validator.Infrastructure.Repository.ChangeLeader;

namespace Validator.Test.Repository;

public class PreElectionLeaderVoteRepository_Test
{
    private DbContextOptions<ValidatorDbContext> _dbContextOptions
           = new DbContextOptionsBuilder<ValidatorDbContext>().UseInMemoryDatabase(databaseName: "ApproverRepository_Test").Options;

    [Test]
    public async Task Add_AddElementToTable()
    {
        using var dbContext = new ValidatorDbContext(_dbContextOptions);
        var preElectionChangeLeaderRepository = new PreLeaderVoteChangeLeaderRepository(dbContext);

        var preElectionLeaderVote = new PreLeaderVoteChangeLeader()
        {
            Id = 1,
            ApproverId = 1,
            PreElectionChangeLeaderId = "12345",
        };

        var entity = await preElectionChangeLeaderRepository.Add(preElectionLeaderVote, CancellationToken.None);

        Assert.NotNull(entity);
        Assert.IsNotNull(entity.Id);
        Assert.IsInstanceOf<PreLeaderVoteChangeLeader>(entity);
    }
}
