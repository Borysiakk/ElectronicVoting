using EntityFrameworkCore.Triggered;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Validator.Domain.Table.ChangeView;

namespace Validator.Infrastructure.Triggers
{
    public class AfterCreateInitializationChangeViewTransaction : IAfterSaveTrigger<InitializationChangeViewTransaction>
    {
        public async Task AfterSave(ITriggerContext<InitializationChangeViewTransaction> context, CancellationToken cancellationToken)
        {
            ///Kiedy validatorzy przeglosują zmiane lidera, zostaje uruchomione glosowanie nad jego zmianą 
            throw new NotImplementedException();
        }
    }
}
