using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AppynittyWebApp.Models
{
    public partial class Career
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        [StringLength(500)]
        public string JobTitle { get; set; }
        [Column("Min_Exp")]
        public string MinExp { get; set; }
        [Column("Max_Exp")]
        public string MaxExp { get; set; }
        [Column("Min_Salary")]
        public string MinSalary { get; set; }
        [Column("Max_Salary")]
        public string MaxSalary { get; set; }
        [Column("Job_Location")]
        public string JobLocation { get; set; }
        [Column("Job_Desc")]
        public string JobDesc { get; set; }
        [Column("isActive")]
        public bool IsActive { get; set; }
    }
}
