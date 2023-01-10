using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AppynittyWebApp.Models
{
    [Keyless]
    [Table("LoginInfo")]
    public partial class LoginInfo
    {
        [Column("userId")]
        public int UserId { get; set; }
        [Column("userName")]
        [StringLength(500)]
        public string UserName { get; set; }
        [Column("userLoginId")]
        [StringLength(200)]
        public string UserLoginId { get; set; }
        [Column("userPassword")]
        [StringLength(100)]
        public string UserPassword { get; set; }
        [Column("userType")]
        [StringLength(50)]
        public string UserType { get; set; }
        [Column("isActive")]
        public bool? IsActive { get; set; }
    }
}
