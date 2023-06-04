using ElectronicVoting.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ElectronicVoting.Infrastructure.PipelineBehavior
{

    public interface INotUseTransaction { }

    public class DbContextTransactionPipelineBehavior<TRequest, TResponse> 
        : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {

        private readonly ApplicationDbContext _dbContext;
        public DbContextTransactionPipelineBehavior(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is (IRequest or IRequest<TResponse>) && request is not INotUseTransaction)
            {
                using (var transaction = _dbContext.Database.BeginTransaction())
                {
                    try
                    {
                        await transaction.CommitAsync(cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                        await transaction.RollbackAsync(cancellationToken);
                    }

                    return await next();
                }
            }
            else
            {
                return await next();
            }

            
        }
    }
}
