using System;
using System.Collections.Generic;

#nullable disable

namespace First_Project.Models
{
    public partial class Rolef
    {
        public Rolef()
        {
            Userloginfs = new HashSet<Userloginf>();
        }

        public decimal Id { get; set; }
        public string Rolename { get; set; }

        public virtual ICollection<Userloginf> Userloginfs { get; set; }
    }
}
