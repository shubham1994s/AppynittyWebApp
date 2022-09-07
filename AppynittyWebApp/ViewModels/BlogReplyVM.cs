using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppynittyWebApp.ViewModels
{
    public class BlogReplyVM
    {
        public int? Blog_Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile_No { get; set; }
        public string Comment { get; set; }

        public string BlogTitle { get; set; }
        public List<BlogRplyDetailsIteam> ListNewsRplyDetails { get; set; }
    }

    public class BlogRplyDetailsIteam
    {
        public int? Blog_Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile_No { get; set; }
        public string Comment { get; set; }

        public string BlogTitle { get; set; }
    }
}
