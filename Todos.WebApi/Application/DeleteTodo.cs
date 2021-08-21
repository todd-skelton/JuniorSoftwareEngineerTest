using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Todos.WebApi.Data;
using Todos.WebApi.Domain;

namespace Todos.WebApi.Application
{
    public class DeleteTodo
    {
        public record Request(Guid TodoId) : IRequest;

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

                var audit = new AuditEntry
                {
                    AuditEntryId = Guid.NewGuid(),
                    AuditDate = DateTime.Now,
                    Action = "DeleteTodo",
                    TodoId = todo.TodoId,
                    AuditInfo = todo.Text
                };

                context.Todos.Remove(todo);
                await context.AuditEntries.AddAsync(audit, cancellationToken);

                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
