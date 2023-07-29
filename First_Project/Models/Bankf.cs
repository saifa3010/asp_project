using System;
using System.Collections.Generic;

#nullable disable

namespace First_Project.Models
{
    public partial class Bankf
    {
        public decimal Id { get; set; }
        public DateTime? ExpireDate { get; set; }
        public decimal? CardNumber { get; set; }
        public decimal? Cvv { get; set; }
        public decimal? Balance { get; set; }
    }
}
