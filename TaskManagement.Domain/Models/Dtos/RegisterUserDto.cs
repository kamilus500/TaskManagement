﻿namespace TaskManagement.Domain.Models.Dtos
{
    public class RegisterUserDto
    {
        public string Email { get; set; } = default!;
        public string Login { get; set; } = default!;
        public string Password { get; set; } = default!;
    }
}
