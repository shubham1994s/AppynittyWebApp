﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AppynittyWebApp.Models
{
    public partial class Blog
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

        // public string FileName { get; set; }

       // [Required(ErrorMessage = "Please choose Blog image")]
        public string FileName { get; set; }
    }
}
