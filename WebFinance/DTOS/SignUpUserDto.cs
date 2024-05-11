﻿using System.ComponentModel.DataAnnotations;

namespace WebFinance.DTOS
{
    public class SignUpUserDto
    {
        [Required, EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required, Compare(nameof(Password), ErrorMessage = "The passwords didn't match.")]
        public required string ConfirmPassword { get; set; }
    }
}
