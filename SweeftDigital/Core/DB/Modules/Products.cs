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
    public class ProductsDataAccess : DataAccessBase
    {

        #region Properties
        AppSettingsCollection AppSettings;
        #endregion

        #region Constructors
        public ProductsDataAccess(ConnectionFactory ConnectionFactory, AppSettingsCollection AppSettings) : base(ConnectionFactory)
        {
            this.AppSettings = AppSettings;
        }
        #endregion

        #region Methods

        public async Task<List<ProductListItemResultItem>> ProductList()
        {
            try
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                   return  await db.ProductList().ToListAsync();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        #endregion
    }
}
