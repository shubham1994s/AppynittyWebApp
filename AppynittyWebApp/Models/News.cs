using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

#nullable disable

namespace AppynittyWebApp.Models
{
    [Keyless]
    public partial class News
    {
        public int Id { get; set; }
        public DateTime NewsDate { get; set; }
        public string NewsTitle { get; set; }
        public string NewsEng { get; set; }
        public string NewsMar { get; set; }
        public bool? IsActive { get; set; }


    }
}
