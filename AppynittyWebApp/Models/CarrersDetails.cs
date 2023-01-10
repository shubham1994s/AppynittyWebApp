using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppynittyWebApp.Models
{
    [Keyless]
    public class CarrersDetails
    {
        public int? Id { get; set; }
        public DateTime Date { get; set; }
        public string JobTitle { get; set; }
        public bool IsActive { get; set; }
        public int? CareersCount { get; set; }
    }
}
