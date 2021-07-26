using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;
using Todos.WebApi.Data;
using Todos.WebApi.Domain;

namespace Todos.WebApi.Application
{
    public class CreateTodo
    {
        public record Request(string Text) : IRequest;

        public class RequestHandler : IRequestHandler<Request>
        {
            public readonly TodoContext context;

            public RequestHandler(TodoContext context)
            {
                this.context = context;
            }

            public async Task<Unit> Handle(Request request, CancellationToken cancellationToken)
            {
                var todo = new Todo
                {
                    TodoId = Guid.NewGuid(),
                    Text = request.Text,
                    IsCompleted = false,
                    Created = DateTime.Now,
                    LastUpdated = DateTime.Now
                };

                await context.Todos.AddAsync(todo, cancellationToken);
                await context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }
}
