using System;
using System.Collections.Generic;

#nullable disable

namespace First_Project.Models
{
    public partial class Userloginf
    {
        public decimal Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public decimal? RoleId { get; set; }
        public decimal? AccId { get; set; }

        public virtual Useraccf Acc { get; set; }
        public virtual Rolef Role { get; set; }
    }
}
