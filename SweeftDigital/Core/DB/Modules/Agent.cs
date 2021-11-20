using Core.DB.Common;
using Core.Utilities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.DB.Modules
{
    public class AgentsDataAccess : DataAccessBase
    {
        #region Properties
        AppSettingsCollection AppSettings;
        #endregion

        #region Constructors
        public AgentsDataAccess(ConnectionFactory ConnectionFactory, AppSettingsCollection AppSettings) : base(ConnectionFactory)
        {
            this.AppSettings = AppSettings;
        }
        #endregion

        #region Methods
        public async Task AgentsCreate(int? AgentParentID = null, string AgentFirstname = null, string AgentLastname = null, string AgentPersonalNumber = null)
        {
            try
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    await db.AgentsCreate(AgentParentID, AgentFirstname, AgentLastname, AgentPersonalNumber);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task AgentsDelete(int? AgentID = null)
        {
            try
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    await db.AgentsDelete(AgentID);
                }
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public async Task<List<DBCoreDataContext.AgentsGetBonusesByAgentIDAndDateResultItem>> AgentsGetBonusesByAgentIDAndDate(int? AgentID = null, DateTime? OrderDateCreated = null)
        {
            try
            {
                using (var db = ConnectionFactory.GetDBCoreDataContext())
                {
                    return await db.AgentsGetBonusesByAgentIDAndDate(AgentID, OrderDateCreated).ToListAsync();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion
    }

    public class Agent
    {
        #region Properties
        AppSettingsCollection AppSettings;
        public int? AgentID { get; set; }
        public int? AgentParentID { get; set; }
        public string AgentFirstname { get; set; }
        public string AgentLastname { get; set; }
        public string AgentPersonalNumber { get; set; }
        public bool? AgentIsActive { get; set; }
        #endregion

        #region Constructors
        public Agent() { }

        public Agent(AppSettingsCollection AppSettings)
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
