using System;
using System.Collections.Generic;

#nullable disable

namespace AppynittyWebApp.Models
{
    public partial class Test
    {
        public int Id { get; set; }
        public DateTime TestDate { get; set; }
        public string TestTitle { get; set; }
        public string TestEng { get; set; }
        public string TestMar { get; set; }
        public bool? IsActive { get; set; }
    }
}
