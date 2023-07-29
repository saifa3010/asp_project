using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace First_Project.Models
{
    public partial class Aboutf
    {
        public decimal Id { get; set; }
        public string Imagepath { get; set; }
        public string Team { get; set; }
        public string Text1 { get; set; }
        public string Info1 { get; set; }
        public string Info2 { get; set; }
        [NotMapped]
        public virtual IFormFile ImageFile { get; set; }
    }
}
