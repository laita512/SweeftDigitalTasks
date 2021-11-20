using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Core.Utilities
{
    public class AppSettingsCollection
    {
        #region Properties        
        IConfiguration Configuration { get; set; }
        public DBConnectionStringsModel DBConnectionStrings { get; set; }

        #endregion

        #region Constructors
        public AppSettingsCollection(IConfiguration Configuration)
        {
            this.Configuration = Configuration;
            DBConnectionStrings = new DBConnectionStringsModel(Configuration);
        }
        #endregion

        #region Private Methods
       
        #region Sub Classes
        public class DBConnectionStringsModel
        {
            #region Properties
            IConfiguration Configuration { get; set; }
            public string DBConnectionString => GetDBConnectionString();
            #endregion

            #region Constructors
            public DBConnectionStringsModel(IConfiguration Configuration)
            {
                this.Configuration = Configuration;
            }
            #endregion

            #region Methods
            string GetDBConnectionString([CallerMemberName] string Key = "")
            {
                return Configuration.GetConnectionString(Key);
            }
            #endregion
        }
        #endregion

        #endregion
    }
}
