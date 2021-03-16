using System.ComponentModel.DataAnnotations;

namespace ElectronicVoting.Domain.Entities
{
    public class ElectionCandidate
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
    }
}