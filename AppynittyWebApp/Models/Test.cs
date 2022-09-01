using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AppynittyWebApp.Models
{
    [Keyless]
    [Table("Test")]
    public partial class Test
    {
        public int Id { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime TestDate { get; set; }
        [StringLength(500)]
        public string TestTitle { get; set; }
        public string TestEng { get; set; }
        public string TestMar { get; set; }
        [Column("isActive")]
        public bool? IsActive { get; set; }
    }
}
