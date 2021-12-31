using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BookstoreModel
{
    public class CreateUserModel
    {
        [Key]
        public long UserId { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z\s]{1,40}$",
          ErrorMessage = "Name is Invalid.")]
        public string Name { get; set; }
        [Required]
        [RegularExpression(@"^[a-z]+[0-9]?[@]+[a-z]+[.]+[a-z]{3}$",
          ErrorMessage = "Email is Invalid.")]
        public string Email { get; set; }
        [Required]
        [RegularExpression(@"^((\+91)?|91)?[789][0-9]{9}",
          ErrorMessage = "Mobile No is Invalid.")]
        public long Mobile { get; set; }
        [Required]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$",
          ErrorMessage = "Password is Invalid.")]
        public string Password { get; set; }
    }
}
