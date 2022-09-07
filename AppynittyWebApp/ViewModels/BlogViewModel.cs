using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AppynittyWebApp.ViewModels
{
    public class BlogViewModel
    {
        public int Id { get; set; }

        public DateTime BlogsDate { get; set; }

        [Required(ErrorMessage = "Please enter Blog Title")]
        public string BlogsTitle { get; set; }

        [Required(ErrorMessage = "Please enter Blog In English")]
        public string BlogsEng { get; set; }
        public string BlogsMar { get; set; }
        public bool IsActive { get; set; }
        public string Urlink { get; set; }

       // [Required(ErrorMessage = "Please choose profile image")]
        [Display(Name = "Blog Image")]
        public IFormFile FileName { get; set; }

       
        public int? BlogId { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Comment { get; set; }

        public string BFileName { get; set; }

    }
}
