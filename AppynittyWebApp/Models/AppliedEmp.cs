using System;
using System.Collections.Generic;

#nullable disable

namespace AppynittyWebApp.Models
{
    public partial class AppliedEmp
    {
        public int Id { get; set; }
        public int? CareersId { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string CurrentLocation { get; set; }
        public string TotExp { get; set; }
        public string Filename { get; set; }
        public bool Tac { get; set; }
    }
}
