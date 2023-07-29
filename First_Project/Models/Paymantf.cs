using System;
using System.Collections.Generic;

#nullable disable

namespace First_Project.Models
{
    public partial class Paymantf
    {
        public decimal Id { get; set; }
        public string Balance { get; set; }
        public DateTime? PayDate { get; set; }
        public decimal? PaymantId { get; set; }

        public virtual ProductOrderf Paymant { get; set; }
    }
}
