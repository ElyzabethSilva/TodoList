using Grpc.Core;
using TodoList.Application.UseCases;
using TodoList.Core.Entities;
using TodoList.Grpc;

namespace TodoList.API.Services
{
    public class TodoService(
        AddTodoItemUseCase addUseCase,
        GetTodoUseCase getUseCase,
        GetAllTodosUseCase getAllUseCase,
        UpdateTodoUseCase updateUseCase,
        DeleteTodoUseCase deleteUseCase) : Todo.TodoBase
    {
        private readonly AddTodoItemUseCase _addUseCase = addUseCase;
        private readonly GetTodoUseCase _getUseCase = getUseCase;
        private readonly GetAllTodosUseCase _getAllUseCase = getAllUseCase;
        private readonly UpdateTodoUseCase _updateUseCase = updateUseCase;
        private readonly DeleteTodoUseCase _deleteUseCase = deleteUseCase;

        public override async Task<TodoResponse> AddTodo(TodoRequest request, ServerCallContext context)
        {
            var todo = await _addUseCase.ExecuteAsync(request.Title);

            return MapTodo(todo);
        }

        public override async Task<TodoResponse> GetTodo(TodoIdRequest request, ServerCallContext context)
        {
            if (!Guid.TryParse(request.Id, out var id))
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid todo id"));

            var todo = await _getUseCase.ExecuteAsync(id);

            return todo == null 
                ? throw new RpcException(new Status(StatusCode.NotFound, "Todo not found")) 
                : MapTodo(todo);
        }

        public override async Task<TodoListResponse> GetAllTodos(Empty request, ServerCallContext context)
        {
            var todos = await _getAllUseCase.ExecuteAsync();
            var response = new TodoListResponse();
            
            response.Todos.AddRange(todos.Select(MapTodo));
            
            return response;
        }

        public override async Task<TodoResponse> UpdateTodo(UpdateTodoRequest request, ServerCallContext context)
        {
            if (!Guid.TryParse(request.Id, out var id))
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid todo id"));

            var existing = await _getUseCase.ExecuteAsync(id) 
                ?? throw new RpcException(new Status(StatusCode.NotFound, "Todo not found"));

            var newTitle = string.IsNullOrWhiteSpace(request.Title) ? existing.Title : request.Title;
            var newIsCompleted = request.IsCompleted;

            if (existing.Title == newTitle && existing.IsCompleted == newIsCompleted)
                return MapTodo(existing);

            var updated = await _updateUseCase.ExecuteAsync(id, newTitle, newIsCompleted);
            if (!updated)
                throw new RpcException(new Status(StatusCode.NotFound, "Todo not found"));

            var updatedTodo = await _getUseCase.ExecuteAsync(id);
            return updatedTodo == null 
                ? throw new RpcException(new Status(StatusCode.NotFound, "Todo not found")) 
                : MapTodo(updatedTodo);
        }

        public override async Task<DeleteTodoResponse> DeleteTodo(TodoIdRequest request, ServerCallContext context)
        {
            if (!Guid.TryParse(request.Id, out var id))
                throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid todo id"));

            var success = await _deleteUseCase.ExecuteAsync(id);
            return new DeleteTodoResponse { Success = success };
        }

        private static TodoResponse MapTodo(TodoItem todoItem)
        {
            return new TodoResponse
            {
                Id = todoItem.Id.ToString(),
                Title = todoItem.Title,
                IsCompleted = todoItem.IsCompleted
            };
        }
    }
}
