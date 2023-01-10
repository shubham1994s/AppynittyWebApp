using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AppynittyWebApp.Models
{
    public partial class Blog
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime BlogsDate { get; set; }
        [StringLength(500)]
        public string BlogsTitle { get; set; }
        public string BlogsEng { get; set; }
        public string BlogsMar { get; set; }
        [Column("isActive")]
        public bool IsActive { get; set; }
        [Column("URLink")]
        [StringLength(500)]
        public string Urlink { get; set; }
        public string FileName { get; set; }
    }
}
