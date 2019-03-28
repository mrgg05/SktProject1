using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SktProject.Models
{
    public class Order
    {
        [ScaffoldColumn(false)]
        public int OrderId { get; set; }
        [ScaffoldColumn(false)]
        public DateTime OrderDate { get; set; }
        [ScaffoldColumn(false)]
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Adres { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }

        [ScaffoldColumn(false)]
        public decimal Total { get; set; }
        //order detail ile baglanacak

        public List<OrderDetails> OrderDetails { get; set; }

    }
}