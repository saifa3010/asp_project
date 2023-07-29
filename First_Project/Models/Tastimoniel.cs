using System;
using System.Collections.Generic;

#nullable disable

namespace First_Project.Models
{
    public partial class Tastimoniel
    {
        public decimal Id { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public DateTime? Publishdate { get; set; }
        public decimal? AccId { get; set; }

        public virtual Useraccf Acc { get; set; }
    }
}
