using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace First_Project.Models
{
    public partial class Homef
    {
        public decimal Id { get; set; }
        public string Imagepath1 { get; set; }
        public string Text1 { get; set; }
        public string Text2 { get; set; }
        public string Email { get; set; }
        public decimal? Phonenumber { get; set; }
        [NotMapped]
        public virtual IFormFile ImageFile { get; set; }
    }
}
