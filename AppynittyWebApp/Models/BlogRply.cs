using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AppynittyWebApp.Models
{
    [Table("Blog_Rply")]
    public partial class BlogRply
    {
        [Key]
        public int Id { get; set; }
        [Column("Blog_Id")]
        public int? BlogId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [Column("Mobile_No")]
        public string MobileNo { get; set; }
        public string Comment { get; set; }
    }
}
