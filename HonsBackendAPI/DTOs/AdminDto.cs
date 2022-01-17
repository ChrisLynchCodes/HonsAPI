﻿namespace HonsBackendAPI.DTOs
{
    public class AdminDto
    {
     
        public string? Id { get; set; }

    
        public string FirstName { get; set; } = null!;

      
        public string LastName { get; set; } = null!;

   
        public string Email { get; set; } = null!;

   
        public string Password { get; set; } = null!;

   
        public bool ConfirmPassword { get; set; }



        public DateTime CreatedAt { get; set; } 

      
        public DateTime UpdatedAt { get; set; } 
    }
}
