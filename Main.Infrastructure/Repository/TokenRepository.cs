using Main.Domain.Table;
using ElectronicVoting.Persistence;
using ElectronicVoting.Common.Infrastructure;


namespace Main.Infrastructure.Repository
{

    public class TokenRepository : Repository<Token>
    {
        public TokenRepository(MainDbContext dbContext) : base(dbContext) {}
    }
}
