using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table.ChangeLeader;
using Validator.Infrastructure.Repository;

namespace Validator.Test.Repository;

public class LeaderRepository_Test
{
    [Test]
    public async Task Add_AddLeaderToTable()
    {
        DbContextOptions<ValidatorDbContext> _dbContextOptions
        = new DbContextOptionsBuilder<ValidatorDbContext>().UseInMemoryDatabase(databaseName: "ApproverRepository_Test1").Options;

        using var dbContext = new ValidatorDbContext(_dbContextOptions);
        var leaderRepository = new LeaderRepository(dbContext);

        var leader = new Leader()
        {
            LeaderId = 1,
            ChangeAproverId = 2,
            LeaderApproverId = 3,
            PreElectionId = "1234-1234"
        };

        var entity = await leaderRepository.Add(leader, CancellationToken.None);

        Assert.NotNull(entity);
        Assert.IsInstanceOf<Leader>(entity);
        Assert.IsNotNull(entity.LeaderId);
    }

    [Test]
    public async Task GetApproverIdForLatestLeader_GetLastApproverIdFromLeader()
    {
        DbContextOptions<ValidatorDbContext> _dbContextOptions
        = new DbContextOptionsBuilder<ValidatorDbContext>().UseInMemoryDatabase(databaseName: "ApproverRepository_Test2").Options;

        using var dbContext = new ValidatorDbContext(_dbContextOptions);
        var leaderRepository = new LeaderRepository(dbContext);

        var leaderAdded = new Leader()
        {
            LeaderId = 1,
            ChangeAproverId = 2,
            LeaderApproverId = 3,
            PreElectionId = "1234-1234"
        };

        var entity = await leaderRepository.Add(leaderAdded, CancellationToken.None);


        var leader = await leaderRepository.GetApproverIdForLatestLeader(CancellationToken.None);

        Assert.NotNull(leader);
        Assert.IsInstanceOf<Int64>(leader);

    }
}
