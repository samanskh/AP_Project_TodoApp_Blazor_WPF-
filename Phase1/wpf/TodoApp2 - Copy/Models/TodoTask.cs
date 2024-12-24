using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp2.Models
{
    public class TodoTask
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsStarred { get; set; } // Add this property
        public bool IsCompleted { get; set; } // Add this property
        public int ListId { get; set; } // Add this property
        public DateTime DueDate { get; set; }
    }
}
