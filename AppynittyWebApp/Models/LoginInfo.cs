using System;
using System.Collections.Generic;

#nullable disable

namespace AppynittyWebApp.Models
{
    public partial class LoginInfo
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserLoginId { get; set; }
        public string UserPassword { get; set; }
        public string UserType { get; set; }
        public bool? IsActive { get; set; }
    }
}
