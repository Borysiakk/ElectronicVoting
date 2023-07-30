using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table.ChangeLeader;
using Validator.Infrastructure.Repository.ChangeLeader;

namespace Validator.Test.Repository;

public class PreElectionLocalVotesRepository_Test
{

    private DbContextOptions<ValidatorDbContext> _dbContextOptions
            = new DbContextOptionsBuilder<ValidatorDbContext>().UseInMemoryDatabase(databaseName: "ApproverRepository_Test").Options;

    [Test]
    public async Task Add_AddElementToTable()
    {
        using var dbContext = new ValidatorDbContext(_dbContextOptions);
        var preElectionChangeLeaderRepository = new PreLocalVoteChangeLeaderRepository(dbContext);

        var preElectionLocalVoteChangeLeader = new PreLocalVoteChangeLeader()
        {
            Id = 1,
            Decision = true,
            ApproverId = 10,
            PreElectionChangeLeaderId = Guid.NewGuid().ToString(),
        };

        var entity = await preElectionChangeLeaderRepository.Add(preElectionLocalVoteChangeLeader, CancellationToken.None);

        Assert.NotNull(entity);
        Assert.IsNotNull(entity.Id);
        Assert.IsInstanceOf<PreLocalVoteChangeLeader>(entity);
    }
}
