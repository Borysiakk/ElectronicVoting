using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ElectronicVoting.Domain.Entities
{
    public class SessionValidator
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string ConnectionId { get; set; }
        public string Organization { get; set; }
        public string OrganizationId { get; set; }
        public Boolean StatusConnection { get; set; }
        
        [ForeignKey("Id")]
        public virtual ValidatorUser User { get; set; }
    }
}