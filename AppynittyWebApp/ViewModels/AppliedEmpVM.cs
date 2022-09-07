using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppynittyWebApp.ViewModels
{
    public class AppliedEmpVM
    {
        public int? Careers_Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile_No { get; set; }
        public string Current_Location { get; set; }
        public string Tot_Exp { get; set; }
        public string Filename { get; set; }

        public string JobTitle { get; set; }
        public bool TAC { get; set; }
        public List<AppEmpCVDetailsIteam> ListAppEmpCVDetails { get; set; }
    }

    public class AppEmpCVDetailsIteam
    {
        public int? Careers_Id { get; set; }
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Mobile_No { get; set; }
        public string Current_Location { get; set; }
        public string Tot_Exp { get; set; }
        public string Filename { get; set; }

        public string JobTitle { get; set; }
        public bool TAC { get; set; }
    }
}
