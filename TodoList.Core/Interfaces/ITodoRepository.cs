using TodoList.Core.Entities;

namespace TodoList.Core.Interfaces
{
    public interface ITodoRepository
    {
        Task<TodoItem> AddAsync(TodoItem item);
        Task<TodoItem?> GetByIdAsync(Guid id);
        Task<IEnumerable<TodoItem>> GetAllAsync();
        Task<bool> UpdateAsync(TodoItem item);
        Task<bool> DeleteAsync(Guid id);
    }
}