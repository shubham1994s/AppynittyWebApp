using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppynittyWebApp.ViewModels
{
    public class CareersVM
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string JobTitle { get; set; }
        public string MinExp { get; set; }
        public string MaxExp { get; set; }
        public string MinSalary { get; set; }
        public string MaxSalary { get; set; }
        public string JobLocation { get; set; }
        public string JobDesc { get; set; }
        public bool IsActive { get; set; }
        public int Careers_Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string MobileNo { get; set; }
        public string CurrentLocation { get; set; }
        public string TotExp { get; set; }
        public IFormFile FileName { get; set; }
        public bool Tac { get; set; }

    }


}
