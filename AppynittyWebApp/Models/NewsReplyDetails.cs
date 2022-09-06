using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppynittyWebApp.Models
{
    [Keyless]
    public class NewsReplyDetails
    {
      
        public int? News_Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile_No { get; set; }
        public string Comment { get; set; }

        public string NewsTitle { get; set; }
    }
}
