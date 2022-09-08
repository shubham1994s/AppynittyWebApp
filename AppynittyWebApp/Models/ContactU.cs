using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AppynittyWebApp.Models
{
    public partial class ContactU
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Please Enter Name")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Please Enter Mobile Number")]
        public string MobileNo { get; set; }
        [Required(ErrorMessage = "Please Enter Country")]
        public string Country { get; set; }
        [Required(ErrorMessage = "Please Enter City")]
        public string City { get; set; }
        [Required(ErrorMessage = "Please Enter your Business")]
        public string Business { get; set; }

    }
}
