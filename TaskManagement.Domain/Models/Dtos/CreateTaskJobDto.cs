﻿namespace TaskManagement.Domain.Models.Dtos
{
    public class CreateTaskJobDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
    }
}
