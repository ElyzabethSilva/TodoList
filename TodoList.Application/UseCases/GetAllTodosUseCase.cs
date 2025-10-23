using TodoList.Core.Entities;
using TodoList.Core.Interfaces;

namespace TodoList.Application.UseCases
{
    public class GetAllTodosUseCase(ITodoRepository repository)
    {
        private readonly ITodoRepository _repository = repository;

        public async Task<IEnumerable<TodoItem>> ExecuteAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}