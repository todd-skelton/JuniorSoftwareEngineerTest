using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Todos.WebApi.Data;
using Todos.WebApi.Domain;

namespace Todos.WebApi.Application
{
    public class GetTodos
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
                var todos = await context.Todos.ToListAsync(cancellationToken);

                return new Response(todos);
            }
        }

        public record Response(List<Todo> Todos);
    }
}
