using SweeftDigital.Reusables.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SweeftDigital.Models
{
    public class OrderModel : WebProjectModelBase
    {
        #region Methods
        public async Task CreateOrder(Order Order)
        {
            await DataAccessFactory.Orders.OrdersCreate(Order.AgentID, Order.ProductID, Order.OrderProductQuantityKg);
        }

        public async Task<bool> EnableApproveOrder(int OrderID)
        {
            return await DataAccessFactory.Orders.IsEnableApproveOrder(OrderID);
        }

        public async Task ApproveOrder(int OrderID)
        {
            await DataAccessFactory.Orders.OrdersApprove(OrderID);
        }

        public async Task<List<string>> ValidateOrder(Order Order)
        {
            var Errors = new List<string>();
            if (!Order.AgentID.HasValue)
            {
                Errors.Add($"{nameof(Order.AgentID)} Is Required");
            }

            if (!Order.ProductID.HasValue)
            {
                Errors.Add($"{nameof(Order.ProductID.HasValue)} Is Required");
            }
            if (!Order.OrderProductQuantityKg.HasValue)
            {
                Errors.Add($"{nameof(Order.OrderProductQuantityKg)} Is Required");
            }
            if (!Order.OrderTotalPrice.HasValue)
            {
                Errors.Add($"{nameof(Order.OrderTotalPrice)} Is Required");
            }
            if (Errors.Count == 0)
            {
                var Products = await DataAccessFactory.Products.ProductList();
                var TotalProductsKg = Products.Where(Item => Item.ProductID == Order.ProductID).Sum(Item => Item.ProductQuantityKg);
                var OrdersQuantityKg = Order.OrderProductQuantityKg;
                if (TotalProductsKg < OrdersQuantityKg )
                {
                    Errors.Add($"Not Enough Product");
                }
            }

            return Errors;
        }
        #endregion

        #region Sub Classes
        public class Order
        {
            #region Properties
            public int? AgentID { get; set; }
            public int? ProductID { get; set; }
            public int? OrderProductQuantityKg { get; set; }
            public decimal? OrderTotalPrice { get; set; }
            #endregion

        }

        #endregion
    }
}
