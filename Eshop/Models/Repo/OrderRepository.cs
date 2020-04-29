using Eshop.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Eshop.Models.Repo
{
    public class OrderRepository
    {
        private readonly MyContext _context = MyContext.GetInstance();

        public int AddOrder(Order order)
        {
            var dbOrder = _context.Order.Add(order);
            this._context.SaveChanges();
            return dbOrder.Id;
        }

        public void AddProductsForOrder(List<ProductLabelImages> products, int idOrder)
        {
            List<ProductForOrder> list = new List<ProductForOrder>();
            foreach (var item in products)
            {
                ProductForOrder product = new ProductForOrder();
                product.Id_Order = idOrder;
                product.Id_Product = item.Id;
                list.Add(product);
            }

            this._context.Product_For_Orders.AddRange(list);
            this._context.SaveChanges();
        }
    }
}