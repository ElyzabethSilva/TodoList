namespace TodoList.Core.Entities
{
    public class TodoItem(string title)
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Title { get; private set; } = title;
        public bool IsCompleted { get; private set; } = false;

        public void MarkAsCompleted()
        {
            IsCompleted = true;
        }

        public void UpdateTitle(string title)
        {
            Title = title;
        }
    }
}