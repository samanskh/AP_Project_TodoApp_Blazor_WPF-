using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace TodoApp2.Models
{
    public class TodoTask
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
        [JsonPropertyName("isStarred")]
        public bool IsStarred { get; set; } 
        [JsonPropertyName("isCompleted")]
        public bool IsCompleted { get; set; } 
        [JsonPropertyName("listId")]
        public int ListId { get; set; } 
        [JsonPropertyName("dueDate")]
        public DateTime DueDate { get; set; }
    }
}
