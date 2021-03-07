using System;
using System.Collections.Generic;
using System.Net;

namespace ElectronicVoting.Domain.Contract.Result
{
    public class HttpAuthorizationResult
    {
        public string Token { get; set; }
        
        public string Organization { get; set; }
        public string OrganizationId { get; set; }
        public DateTime DateIssue { get; set; }
        public HttpStatusCode Code { get; set; }
        public IEnumerable<string> Errors { get; set; } 
    }
}