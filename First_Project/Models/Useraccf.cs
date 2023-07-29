using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace First_Project.Models
{
    public partial class Useraccf
    {
        public Useraccf()
        {
            BankAccInfofs = new HashSet<BankAccInfof>();
            Orderves = new HashSet<Orderf>();
            Reports = new HashSet<Report>();
            Tastimoniels = new HashSet<Tastimoniel>();
            Userloginfs = new HashSet<Userloginf>();
        }

        public decimal Id { get; set; }
        public string Fname { get; set; }
        public string Lname { get; set; }
        public string Imagepath { get; set; }
        public string Phonenumber { get; set; }
        [NotMapped]
        public virtual IFormFile ImageFile { get; set; }
        public virtual ICollection<BankAccInfof> BankAccInfofs { get; set; }
        public virtual ICollection<Orderf> Orderves { get; set; }
        public virtual ICollection<Report> Reports { get; set; }
        public virtual ICollection<Tastimoniel> Tastimoniels { get; set; }
        public virtual ICollection<Userloginf> Userloginfs { get; set; }
    }
}
