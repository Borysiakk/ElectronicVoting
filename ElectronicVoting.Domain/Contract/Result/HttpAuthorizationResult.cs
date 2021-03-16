using System;
using System.Collections.Generic;
using System.Net;

namespace ElectronicVoting.Domain.Contract.Result
{
    public class HttpAuthorizationResult
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public DateTime DateIssue { get; set; }
        public HttpStatusCode Code { get; set; }
        public IEnumerable<string> Errors { get; set; } 
    }
}