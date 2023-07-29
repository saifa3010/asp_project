using System;
using System.Collections.Generic;

#nullable disable

namespace First_Project.Models
{
    public partial class Contactf
    {
        public decimal Id { get; set; }
        public string Text1 { get; set; }
        public string Location { get; set; }
        public string Email { get; set; }
        public decimal? Phonenumber { get; set; }
    }
}
