using Grpc.Core;
using TodoList.Application.UseCases;
using TodoList.Grpc;

namespace TodoList.API.Services
{
    public class TodoService(AddTodoItemUseCase addTodoItemUseCase) : Todo.TodoBase
    {
        private readonly AddTodoItemUseCase _addTodoItemUseCase = addTodoItemUseCase;

        public override async Task<TodoResponse> AddTodo(TodoRequest request, ServerCallContext context)
        {
            var todoItem = await _addTodoItemUseCase.ExecuteAsync(request.Title);

            return new TodoResponse
            {
                Id = todoItem.Id.ToString(),
                Title = todoItem.Title,
                IsCompleted = todoItem.IsCompleted
            };
        }

        // Adicionar:
        // GetTodo, GetAllTodos, UpdateTodo, DeleteTodo
    }
}