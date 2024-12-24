using System.Text.Json.Serialization;
public class TodoTask
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    [JsonPropertyName("name")]
    public string Name { get; set; }
    [JsonPropertyName("dueDate")]
    public DateTime DueDate { get; set; }
    [JsonPropertyName("isCompleted")]
    public bool IsDone { get; set; }
    [JsonPropertyName("isStarred")]
    public bool IsStarred { get; set; }
    [JsonPropertyName("listId")]
    public int ListId { get; set; } // Foreign key to TodoList
}
