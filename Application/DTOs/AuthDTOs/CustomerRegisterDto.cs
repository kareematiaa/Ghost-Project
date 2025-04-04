using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.AuthDTOs
{
   public class CustomerRegisterDto
    {
        [Required]
        public required string FullName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string PhoneNumber { get; set; }


        //[Required]
        //public DateOnly DateOfBirth { get; set; }


        //[Required]
        //public bool Gender { get; set; }


        [Required]
        //[RegularExpression("^(?=.*?[A-Z])(?=.*?[#?!@$%^&*-]).{8,15}$",
        //    ErrorMessage = "Password Must have At Least 1 Uppercase, 1 special character")]
        public required string Password { get; set; }


        [Required]
        [Compare("Password")]
        public required string ConfirmPassword { get; set; }


    }
}
