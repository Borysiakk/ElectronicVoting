using EntityFrameworkCore.Triggered;
using Validator.Domain.Table;
using Validator.Infrastructure.Helper;

namespace Validator.API.Triggers.Blockchain;

public class BeforeCreateBlockTrigger : IBeforeSaveTrigger<Block>
{
    public Task BeforeSave(ITriggerContext<Block> context, CancellationToken cancellationToken)
    {
        if(context.ChangeType == ChangeType.Added || context.ChangeType == ChangeType.Modified)
        {
            var block = context.Entity;
            block.Hash = HashHelper.ComputeHash(block);
        }

        return Task.CompletedTask;
    }
}
