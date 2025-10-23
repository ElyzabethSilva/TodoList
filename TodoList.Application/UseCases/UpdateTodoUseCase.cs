using TodoList.Core.Interfaces;

namespace TodoList.Application.UseCases
{
    public class UpdateTodoUseCase(ITodoRepository repository)
    {
        private readonly ITodoRepository _repository = repository;

        public async Task<bool> ExecuteAsync(Guid id, string title, bool isCompleted)
        {
            var todo = await _repository.GetByIdAsync(id);

            if (todo == null)
                return false;

            todo.UpdateTitle(title);

            if (isCompleted)
                todo.MarkAsCompleted();

            return await _repository.UpdateAsync(todo);
        }
    }
}