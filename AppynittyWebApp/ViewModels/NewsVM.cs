using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppynittyWebApp.ViewModels
{
    public class NewsVM
    {
        public int Id { get; set; }
        public DateTime NewsDate { get; set; }
        public string NewsTitle { get; set; }
        public string NewsEng { get; set; }
        public string NewsMar { get; set; }
        public bool IsActive { get; set; }

        public string BlogId { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string Comment { get; set; }

    }
}
