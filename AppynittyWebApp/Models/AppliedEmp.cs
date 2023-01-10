using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AppynittyWebApp.Models
{
    [Table("Applied_Emp")]
    public partial class AppliedEmp
    {
        [Key]
        public int Id { get; set; }
        [Column("Careers_Id")]
        public int? CareersId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [Column("Mobile_No")]
        public string MobileNo { get; set; }
        [Column("Current_Location")]
        public string CurrentLocation { get; set; }
        [Column("Tot_Exp")]
        public string TotExp { get; set; }
        public string Filename { get; set; }
        [Column("TAC")]
        public bool Tac { get; set; }
        [Column("View_Status")]
        public bool? ViewStatus { get; set; }
    }
}
