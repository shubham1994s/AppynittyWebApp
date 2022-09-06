using System;
using System.Collections.Generic;

#nullable disable

namespace AppynittyWebApp.Models
{
    public partial class Blog
    {
        public int Id { get; set; }
        public DateTime BlogsDate { get; set; }
        public string BlogsTitle { get; set; }
        public string BlogsEng { get; set; }
        public string BlogsMar { get; set; }
        public bool IsActive { get; set; }
        public string Urlink { get; set; }
        public string FileName { get; set; }
    }
}
