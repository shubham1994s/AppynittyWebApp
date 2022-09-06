using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppynittyWebApp.ViewModels
{
    public class NewsRplyVM
    {

        public List<NewsRplyDetailsIteam> ListNewsRplyDetails { get; set; }
    }

    public class NewsRplyDetailsIteam
    {
        public Nullable<int> Id { get; set; } = 0;
        public DateTime Date { get; set; } 
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile_No { get; set; }
        public string Comment { get; set; }
        public string NewsTitle { get; set; }
    }
}
