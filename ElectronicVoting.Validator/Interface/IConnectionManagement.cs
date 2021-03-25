using System.Collections.Generic;
using System.Threading.Tasks;
using ElectronicVoting.Validator.PriorityQueue;

namespace ElectronicVoting.Validator.Interface
{
    public interface IConnectionManagement
    {
        public Task StartAsync();
        public Task StopAsync();


        public List<string> GetAllConnectionValidatorsName();
        public List<string> GetCurrentConnectionValidatorName();
        public void SendMessages(string validator,byte [] msg, PriorityMessage priority);
    }
}