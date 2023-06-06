using ElectronicVoting.Domain.Table.Main;
using ElectronicVoting.Infrastructure.Repository;
using ElectronicVoting.Persistence;
using Microsoft.EntityFrameworkCore;


namespace ElectronicVoting.Test
{
    [TestFixture]
    public class SettingRepositoryUnitTest
    {

        private SettingRepository _settingRepository;
        private DbContextOptions<MainDbContext> _dbContextOptions = new DbContextOptionsBuilder<MainDbContext>()
            .UseInMemoryDatabase(databaseName: "SettingRepositoryUnitTest").Options;
        
        private void InitDb()
        {
            var dbContext = new MainDbContext(_dbContextOptions);
            var settings = new List<Setting>
            {
                new Setting()
                {
                    Id = 1,
                    Name = "Candidate",
                    SubName = "Count",
                    Value = "0",
                },
                new Setting()
                {
                    Id = 2,
                    Name = "Voters",
                    SubName = "Count",
                    Value = "255",
                },
                new Setting()
                {
                    Id = 3,
                    Name = "Validator",
                    SubName = "AcceptableValidatorsCount",
                    Value = "2"
                }
            };

            dbContext.AddRange(settings);
            dbContext.SaveChanges();
        }

        [OneTimeSetUp]
        public void Setup()
        {
            _settingRepository = new SettingRepository(new MainDbContext(_dbContextOptions));

            InitDb();
        }

        [TestCase("Validator", "AcceptableValidatorsCount")]
        public async Task GetSetting(string parent, string child)
        {
            var setting = await _settingRepository.GetAsync(parent, child, CancellationToken.None);

            Assert.IsNotNull(setting);
        }

    }
}
