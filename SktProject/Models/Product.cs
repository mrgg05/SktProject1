
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SktProject.Models
{
    public class Product
    {
        public int ProductId { get; set; }

        public int CategoryId { get; set; }
        public  Category Category { get; set; }

        public string Title { get; set; }
        public decimal Price { get; set; }
        public string ProductUrl { get; set; }

        public DateTime SKT { get; set; }

        public DateTime TETT { get; set; }

        public DateTime ProductionDate { get; set; }

        


        public virtual ApplicationUser User { get; set; }


    }
}