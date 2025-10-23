using TodoList.Core.Interfaces;

namespace TodoList.Application.UseCases
{
    public class DeleteTodoUseCase(ITodoRepository repository)
    {
        private readonly ITodoRepository _repository = repository;

        public async Task<bool> ExecuteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}