namespace ElectronicVoting.Validator.MessageTask
{
    public class Execution
    {
        private readonly ConnectionManagement _connectionManagement;

        public Execution(ConnectionManagement connectionManagement)
        {
            _connectionManagement = connectionManagement;
        }

        public void ExecutionTask(TaskObject task)
        {
            switch (task.Operation)
            {
                case TaskOperation.Validation :
                {

                    
                    break;
                }
            }
        }
    }
}