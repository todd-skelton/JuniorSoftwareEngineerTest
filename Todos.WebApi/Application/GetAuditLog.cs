using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Todos.WebApi.Data;
using Todos.WebApi.Domain;

namespace Todos.WebApi.Application
{
    public class GetAuditLog
    {
        public record Request : IRequest<Response>;

        public class RequestHandler : IRequestHandler<Request, Response>
        {
            public readonly TodoContext context;

            public RequestHandler(TodoContext context)
            {
                this.context = context;
            }

            public async Task<Response> Handle(Request request, CancellationToken cancellationToken)
            {
                var auditLog = await context.AuditEntries.ToListAsync(cancellationToken);

                return new Response(auditLog);
            }
        }

        public record Response(List<AuditEntry> AuditLog);
    }
}
