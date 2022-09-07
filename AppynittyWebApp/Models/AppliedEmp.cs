using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace AppynittyWebApp.Models
{
    public partial class AppliedEmp
    {
        public int Id { get; set; }
        public string CareersId { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string CurrentLocation { get; set; }
        public string TotExp { get; set; }
        [Required(ErrorMessage = "Please choose CV")]
        public string Filename { get; set; }
        public bool Tac { get; set; }
    }
}
