using TodoList.Core.Entities;
using TodoList.Core.Interfaces;

namespace TodoList.Application.UseCases
{
    public class GetTodoUseCase(ITodoRepository repository)
    {
        private readonly ITodoRepository _repository = repository;

        public async Task<TodoItem?> ExecuteAsync(Guid id)
        {
            return await _repository.GetByIdAsync(id);
        }
    }
}