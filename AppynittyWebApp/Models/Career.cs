using System;
using System.Collections.Generic;

#nullable disable

namespace AppynittyWebApp.Models
{
    public partial class Career
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
    }
}
