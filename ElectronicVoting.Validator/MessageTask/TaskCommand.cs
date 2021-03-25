using System;
using System.Windows.Input;
using ElectronicVoting.Validator.Consensus;
using ElectronicVoting.Validator.Interface;
using ElectronicVoting.Validator.MessageTask.Serialization;
using ElectronicVoting.Validator.PriorityQueue;
using Newtonsoft.Json;

namespace ElectronicVoting.Validator.MessageTask
{
    public class TaskCommand :ITaskCommand
    {
        private readonly MessageBlock _messageBlock;
        private readonly ConnectionManagement _connectionManagement;

        public TaskCommand(ConnectionManagement connectionManagement, MessageBlock messageBlock)
        {
            _connectionManagement = connectionManagement;
            _messageBlock = messageBlock;
        }


        public void Call()
        {
            try
            {
                switch (_messageBlock.Stage)
                {
                    case Stage.PrePrepare:
                    {
                        Blockchain blockchain = _connectionManagement.Blockchain;
                        Block block = blockchain.CreateBlock();
                        block.Result = 200;
                    
                    
                    
                        block.Hash = BlockHelper.GetSha256Hash(block);

                        foreach (var validator in _messageBlock.Validators)
                        {
                            if (validator != _connectionManagement.Organization)
                            {
                                MessageBlock messageBlock = new MessageBlock()
                                {
                                    To = validator,
                                    From = _connectionManagement.Organization,
                                    Stage = Stage.Prepare,
                                    Block = block,
                                    Validators = _messageBlock.Validators,
                                };

                                byte[] message = EnvelopeHelper.CreateMessage(messageBlock, PriorityMessage.Normal);
                                
                                _connectionManagement.SendMessages(validator,message,PriorityMessage.Normal);
                            }
                        }
                        break;
                    }
                    case Stage.Prepare:
                    {
                        Console.WriteLine("Odebrano Wiadomosc TaskCommand");
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}