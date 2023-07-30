using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;
using Validator.Domain.Table.ChangeLeader;
using Validator.Domain.Table;
using NUnit.Framework;

namespace Validator.Test.Table;
public class PreElectionChangeLeaderTable
{
    private DbContextOptions<ValidatorDbContext> _dbContextOptions
        = new DbContextOptionsBuilder<ValidatorDbContext>().UseInMemoryDatabase(databaseName: "PreElectionChangeLeaderTable").Options;


    private void ClearDatebase()
    {
        using var dbContext = new ValidatorDbContext(_dbContextOptions);
        var approvers = dbContext.Approvers.ToList();

        dbContext.Approvers.RemoveRange(approvers);

        dbContext.SaveChanges();
    }
    private void InitDatebase()
    {
        using var dbContext = new ValidatorDbContext(_dbContextOptions);

        var approvers = new Approver[]
        {
            new Approver()
            {
                ApproverId = 1,
                Name = "ValidatorA",
                NetworkAddress = "https://validatorA:443",
            },
            new Approver()
            {
                ApproverId = 2,
                Name = "ValidatorB",
                NetworkAddress = "https://ValidatorB:443",
            }
        };

        dbContext.AddRange(approvers);
        dbContext.SaveChanges();
    }

    [OneTimeSetUp]
    public void Setup()
    {
        ClearDatebase();
        InitDatebase();
    }

    [Test]
    public async Task AddPreElectionChangeLeaderEntityToTable()
    {
        using var dbContext = new ValidatorDbContext(_dbContextOptions);
        var approver = await dbContext.Approvers.OrderBy(a => a.ApproverId).FirstOrDefaultAsync();

        var preElectionLocalVoteChangeLeader = new PreLocalVoteChangeLeader()
        {
            Id = 1,
            Decision = true,
            ApproverId = approver.ApproverId,
            PreElectionChangeLeaderId = Guid.NewGuid().ToString(),
        };

        await dbContext.PreLocalVoteChangeLeaders.AddAsync(preElectionLocalVoteChangeLeader);
        await dbContext.SaveChangesAsync();

        var added = await dbContext.PreLocalVoteChangeLeaders.FirstOrDefaultAsync(b => b.Id == 1);

        Assert.NotNull(added);
    }

    [Test]
    public async Task AddRangePreElectionChangeLeaderEntityToTable()
    {
        using var dbContext = new ValidatorDbContext(_dbContextOptions);
        var approver = await dbContext.Approvers.OrderBy(a => a.ApproverId).FirstOrDefaultAsync();

        var preElectionLocalVote1 = new PreLocalVoteChangeLeader()
        {
            Id = 2,
            Decision = true,
            ApproverId = approver.ApproverId,
            PreElectionChangeLeaderId = Guid.NewGuid().ToString(),
        };

        var preElectionLocalVote2 = new PreLocalVoteChangeLeader()
        {
            Id = 3,
            Decision = true,
            ApproverId = approver.ApproverId,
            PreElectionChangeLeaderId = Guid.NewGuid().ToString(),
        };

        await dbContext.PreLocalVoteChangeLeaders.AddAsync(preElectionLocalVote1, CancellationToken.None);
        await dbContext.PreLocalVoteChangeLeaders.AddAsync(preElectionLocalVote2, CancellationToken.None);
        await dbContext.SaveChangesAsync();

        var added1 = await dbContext.PreLocalVoteChangeLeaders.FirstOrDefaultAsync(b => b.Id == 2);
        var added2 = await dbContext.PreLocalVoteChangeLeaders.FirstOrDefaultAsync(b => b.Id == 3);

        Assert.NotNull(added1);
        Assert.NotNull(added2);
    }
}
