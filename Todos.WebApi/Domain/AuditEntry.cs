using System;

namespace Todos.WebApi.Domain
{
    public class AuditEntry
    {
        public Guid AuditEntryId { get;set;}
        public DateTime AuditDate { get; set; }
        public string Action { get; set; }
        public Guid TodoId { get; set; }
        public string AuditInfo { get; set; }
    }
}