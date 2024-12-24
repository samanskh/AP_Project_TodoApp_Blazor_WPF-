public class TodoTask
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DueDate { get; set; }
    public bool IsDone { get; set; }
    public bool IsStarred { get; set; }
    public int ListId { get; set; } // Foreign key to TodoList
}
