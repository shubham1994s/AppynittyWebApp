using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AppynittyWebApp.Models
{
    public partial class News
    {
        [Key]
        public int Id { get; set; }
       
        public DateTime NewsDate { get; set; }
        [StringLength(500)]
        public string NewsTitle { get; set; }
        public string NewsEng { get; set; }
        public string NewsMar { get; set; }
        [Column("isActive")]
        public bool IsActive { get; set; }
    }
}
