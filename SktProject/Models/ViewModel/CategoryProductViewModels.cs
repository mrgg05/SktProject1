using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SktProject.Models.ViewModel
{
    public class CategoryProductViewModels
    {

        public ICollection<Product> Product { get; set; }
        public ICollection<Category> Category { get; set; }
        public virtual ICollection<Cart> Cart { get; set; }

        public Order Order { get; set; }

        public OrderDetails OrderDetail { get; set; }


        public virtual LoginViewModel LoginViewModel { get; set; }
        public virtual RegisterViewModel RegisterViewModel { get; set; }

    }
}