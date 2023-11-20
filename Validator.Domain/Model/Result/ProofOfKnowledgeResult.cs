
namespace Validator.Domain.Model.Result
{
    public class ProofOfKnowledgeResult
    {
        public bool Result { get; set; }
        public byte[] Hash { get; set; }
        public string SessionElectionId { get; set; }
    }

}
