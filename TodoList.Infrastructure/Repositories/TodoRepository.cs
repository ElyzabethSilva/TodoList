using TodoList.Core.Entities;
using TodoList.Core.Interfaces;

namespace TodoList.Infrastructure.Repositories
{
    public class TodoRepository : ITodoRepository
    {
        private readonly List<TodoItem> _items = [];

        public Task<TodoItem> AddAsync(TodoItem item)
        {
            _items.Add(item);
            return Task.FromResult(item);
        }

        public Task<bool> DeleteAsync(Guid id)
        {
            var item = _items.FirstOrDefault(x => x.Id == id);
            if (item == null) return Task.FromResult(false);

            _items.Remove(item);
            return Task.FromResult(true);
        }

        public Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return Task.FromResult(_items.AsEnumerable());
        }

        public Task<TodoItem?> GetByIdAsync(Guid id)
        {
            var item = _items.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(item);
        }

        public Task<bool> UpdateAsync(TodoItem item)
        {
            var existing = _items.FirstOrDefault(x => x.Id == item.Id);
            if (existing == null) return Task.FromResult(false);

            existing.UpdateTitle(item.Title);
            if (item.IsCompleted) existing.MarkAsCompleted();

            return Task.FromResult(true);
        }
    }
}