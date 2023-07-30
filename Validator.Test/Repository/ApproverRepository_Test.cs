using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Validator.Domain.Table;
using Validator.Infrastructure.Repository;
using Validator.Infrastructure.Service;

namespace Validator.Test.Repository;

public class ApproverRepository_Test
{
    private DbContextOptions<ValidatorDbContext> _dbContextOptions
    = new DbContextOptionsBuilder<ValidatorDbContext>().UseInMemoryDatabase(databaseName: "ApproverRepository_Test").Options;


    private void InitDatebase()
    {
        using var dbContext = new ValidatorDbContext(_dbContextOptions);

        var approvers = new Approver[]
        {
            new Approver()
            {
                ApproverId = 1,
                Name = "ValidatorA",
                NetworkAddress = "http://validatorA:80",
            },
            new Approver()
            {
                ApproverId = 2,
                Name = "ValidatorB",
                NetworkAddress = "http://ValidatorB:80",
            },
            new Approver()
            {
                ApproverId = 3,
                Name = "ValidatorC",
                NetworkAddress = "http://ValidatorC:80",
            }
        };
        dbContext.Approvers.AddRange(approvers);
        dbContext.SaveChanges();
    }

    [OneTimeSetUp]
    public void Setup()
    {
        InitDatebase();
    }

    [Test]
    public async Task GetAll_GetAllApprovers()
    {
        using MemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());
        using var dbContext = new ValidatorDbContext(_dbContextOptions);
        CacheService cacheService = new CacheService(memoryCache);

        ApproverRepository approverRepository = new ApproverRepository(dbContext, cacheService);
        var approvers = await approverRepository.GetAll(CancellationToken.None);

        var approversMemory = memoryCache.Get("ApproverRepository.GetAll");

        Assert.That(approvers, Is.Not.Null);
        Assert.That(approversMemory, Is.Not.Null);
        Assert.That(approvers.ElementAt(0).Name, Is.EqualTo("ValidatorA"));
        Assert.That(approvers.ElementAt(1).Name, Is.EqualTo("ValidatorB"));
        Assert.That(approvers.ElementAt(2).Name, Is.EqualTo("ValidatorC"));
        Assert.That(approversMemory, Is.EqualTo(approvers));
    }

    [TestCase("ValidatorA", "ValidatorB", "ValidatorC")]
    [TestCase("ValidatorB", "ValidatorA", "ValidatorC")]
    [TestCase("ValidatorC", "ValidatorA", "ValidatorB")]
    public async Task GetAllWithout_GetAllWithoutApproverName(string nameExceptApprover, string nameFirtApprover, string nameSecondApprover)
    {
        using MemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());
        using var dbContext = new ValidatorDbContext(_dbContextOptions);
        CacheService cacheService = new CacheService(memoryCache);

        ApproverRepository approverRepository = new ApproverRepository(dbContext, cacheService);
        var approvers = await approverRepository.GetAllWithout(nameExceptApprover, CancellationToken.None);

        var approversMemory = memoryCache.Get("ApproverRepository.GetAllWithout");

        Assert.That(approvers, Is.Not.Null);
        Assert.That(approversMemory, Is.Not.Null);
        Assert.That(nameFirtApprover, Is.EqualTo(approvers.ElementAt(0).Name));
        Assert.That(nameSecondApprover, Is.EqualTo(approvers.ElementAt(1).Name));
        Assert.That(approversMemory, Is.EqualTo(approvers));
    }

    [TestCase("ValidatorA")]
    [TestCase("ValidatorB")]
    [TestCase("ValidatorC")]
    public async Task GetByName_GetApproverByName(string name)
    {
        using MemoryCache memoryCache = new MemoryCache(new MemoryCacheOptions());
        using var dbContext = new ValidatorDbContext(_dbContextOptions);
        CacheService cacheService = new CacheService(memoryCache);
        ApproverRepository approverRepository = new ApproverRepository(dbContext, cacheService);

        var approver = await approverRepository.GetByName(name,CancellationToken.None);

        Assert.That(approver, Is.Not.Null);
    }

}
