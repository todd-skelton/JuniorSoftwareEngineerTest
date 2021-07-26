using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Todos.WebApi.Data;

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

                context.Todos.Remove(todo);

                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
