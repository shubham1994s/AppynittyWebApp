﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AppynittyWebApp.Models
{
    [Table("News_Rply")]
    public partial class NewsRply
    {
        [Key]
        public int Id { get; set; }
        [Column("News_Id")]
        public int? NewsId { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        [Column("Mobile_No")]
        public string MobileNo { get; set; }
        public string Comment { get; set; }
    }
}
