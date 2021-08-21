using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Todos.WebApi.Data;
using Todos.WebApi.Domain;

namespace Todos.WebApi.Application
{
    public class SetTodoIsCompleted
    {
        public record Request(Guid TodoId, bool IsCompleted) : IRequest;

        public class RequestHandler : IRequestHandler<Request>
        {
            public readonly TodoContext context;

            public RequestHandler(TodoContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var todo = await context.Todos.FindAsync(new object[] { request.TodoId }, cancellationToken: cancellationToken);

                todo.IsCompleted = request.IsCompleted;
                todo.LastUpdated = DateTime.Now;

                var audit = new AuditEntry
                {
                    AuditEntryId = Guid.NewGuid(),
                    AuditDate = DateTime.Now,
                    Action = "SetTodoIsCompleted",
                    TodoId = todo.TodoId,
                    AuditInfo = todo.IsCompleted.ToString()
                };

                await context.AuditEntries.AddAsync(audit, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
