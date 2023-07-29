using System;
using System.Collections.Generic;

#nullable disable

namespace First_Project.Models
{
    public partial class Report
    {
        public decimal Id { get; set; }
        public decimal? UseraccId { get; set; }
        public decimal? OrderId { get; set; }
        public decimal? ProductId { get; set; }
        public decimal? CategoryId { get; set; }

        public virtual Categoryf Category { get; set; }
        public virtual Orderf Order { get; set; }
        public virtual Productf Product { get; set; }
        public virtual Useraccf Useracc { get; set; }
    }
}
