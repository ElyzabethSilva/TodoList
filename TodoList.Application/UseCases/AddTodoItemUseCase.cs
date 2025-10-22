using TodoList.Core.Entities;
using TodoList.Core.Interfaces;

namespace TodoList.Application.UseCases
{
    public class AddTodoItemUseCase(ITodoRepository repository)
    {
        private readonly ITodoRepository _repository = repository;

        public async Task<TodoItem> ExecuteAsync(string title)
        {
            var todoItem = new TodoItem(title);
            return await _repository.AddAsync(todoItem);
        }
    }
}