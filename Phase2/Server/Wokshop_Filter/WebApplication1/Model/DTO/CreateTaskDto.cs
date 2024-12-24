﻿using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Model.DTO
{
    public class CreateTaskDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsStarred { get; set; } 
        public bool IsCompleted { get; set; } 
        public int ListId { get; set; } 
        public DateTime DueDate { get; set; }

    }
}