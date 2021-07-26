using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Todos.WebApi.Application;

namespace Todos.WebApi.Api.Todos
{
    [ApiController]
    [Route("api/todos")]
    public class Controller : ControllerBase
    {
        public readonly IMediator mediator;

        public Controller(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public async Task<GetTodos.Response> GetTodos()
        {
            return await mediator.Send(new GetTodos.Request());
        }

        [HttpPost]
        public async Task CreateTodo(CreateTodo.Request request)
        {
            await mediator.Send(request);
        }

        public record SetTodoIsCompletedBody(bool IsCompleted);

        [HttpPost("{todoId}/is-completed")]
        public async Task SetTodoIsCompleted([FromRoute] Guid todoId, [FromBody] SetTodoIsCompletedBody body)
        {
            await mediator.Send(new SetTodoIsCompleted.Request(todoId, body.IsCompleted));
        }

        [HttpDelete("{todoId}")]
        public async Task DeleteTodo(Guid todoId)
        {
            await mediator.Send(new DeleteTodo.Request(todoId));
        }
    }
}
