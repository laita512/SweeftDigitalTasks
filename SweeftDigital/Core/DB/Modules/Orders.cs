using Core.DB.Common;
using Core.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Core.DB.DBCoreDataContext;

namespace Core.DB.Modules
{
    public class OrdersDataAccess : DataAccessBase
    {
        #region Properties
        AppSettingsCollection AppSettings;
        #endregion

        #region Constructors
        public OrdersDataAccess(ConnectionFactory ConnectionFactory, AppSettingsCollection AppSettings) : base(ConnectionFactory)
        {
            this.AppSettings = AppSettings;
        }
        #endregion

        #region Methods
        public async Task OrdersApprove(int? OrderID = null)
        {
            try
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    await db.OrdersApprove(OrderID);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task OrdersCreate(int? AgentID = null, int? ProductID=null, int? OrderProductQuantityKg=null)
        {
            try
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    await db.OrdersCreate(AgentID, ProductID, OrderProductQuantityKg);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<List<OrderListItemResultItem>> OrderList()
        {
            try
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    return await db.OrderList().ToListAsync();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<bool> IsEnableApproveOrder(int? OrderID = null)
        {
            try
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    return await db.IsEnableApproveOrder(OrderID);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion
    }

    public class Order
    {
        #region Properties
        AppSettingsCollection AppSettings;
        public int? OrderID { get; set; }
        public int? AgentID { get; set; }
        public int? ProductID { get; set; }
        public int? OrderProductQuantityKg { get; set; }
        public decimal? OrderTotalPrice { get; set; }
        public DateTime? OrderDateCreated { get; set; }
        public bool? OrderIsApproved { get; set; }
        #endregion

        #region Constructors
        public Order() { }

        public Order(AppSettingsCollection AppSettings)
        {
            this.AppSettings = AppSettings;
        }
        #endregion

        #region Methods
        public void SetAppSettings(AppSettingsCollection AppSettings)
        {
            this.AppSettings = AppSettings;
        }

        #endregion
    }
}
