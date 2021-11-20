using Core.DB.Common;
using Core.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.DB.Modules
{
    public class DataAccessFactory
    {
        #region Properties        
        public AgentsDataAccess Agents { get; set; }
        public OrdersDataAccess Orders { get; set; }
        public ProductsDataAccess Products { get; set; }
        #endregion

        #region Constructors
        public DataAccessFactory(AppSettingsCollection AppSettings)
        {
            var ConnectionFactory = new ConnectionFactory(AppSettings.DBConnectionStrings.DBConnectionString);
            Agents = new AgentsDataAccess(ConnectionFactory, AppSettings);
            Orders = new OrdersDataAccess(ConnectionFactory, AppSettings);
            Products = new ProductsDataAccess(ConnectionFactory, AppSettings);
        }
        #endregion
    }
}
