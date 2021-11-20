using SweeftDigital.Reusables.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SweeftDigital.Models
{
    public class AgentModel : WebProjectModelBase
    {
        #region Methods
        public async Task CreateAgent(Agent Agent)
        {
            await DataAccessFactory.Agents.AgentsCreate(Agent.AgentParentID, Agent.AgentFirstname, Agent.AgentLastname, Agent.AgentPersonalNumber);
        }

        public async Task DeleteAgent(int AgentID)
        {
            await DataAccessFactory.Agents.AgentsDelete(AgentID);
        }

        public List<string> ValidateAgent(Agent Agent)
        {
            var Errors = new List<string>();

            if (!Agent.AgentParentID.HasValue)
            {
                Errors.Add($"{nameof(Agent.AgentParentID)} Is Required");
            }
            if (string.IsNullOrWhiteSpace(Agent.AgentFirstname))
            {
                Errors.Add($"{nameof(Agent.AgentFirstname)} Is Required");
            }
            if (string.IsNullOrWhiteSpace(Agent.AgentLastname))
            {
                Errors.Add($"{nameof(Agent.AgentLastname)} Is Required");
            }
            if (string.IsNullOrWhiteSpace(Agent.AgentPersonalNumber))
            {
                Errors.Add($"{nameof(Agent.AgentPersonalNumber)} Is Required");
            }

            return Errors;
        }
        #endregion

        #region Sub Classes
        public class Agent
        {
            #region Properties
            public int? AgentParentID { get; set; }
            public string AgentFirstname { get; set; }
            public string AgentLastname { get; set; }
            public string AgentPersonalNumber { get; set; }
            public bool? AgentIsActive { get; set; }
            #endregion

        }

        #endregion
    }
}
