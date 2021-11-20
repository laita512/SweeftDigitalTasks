using Microsoft.AspNetCore.Mvc;
using SweeftDigital.Models;
using SweeftDigital.Reusables.Core;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SweeftDigital.Controllers
{
    [ApiController]
    [Route("api/agents")]
    public class AgentsController : WebProjectController<AgentModel>
    {
        #region Constructors
        public AgentsController()
        {
            Model = new AgentModel();
        }
        #endregion

        #region Actions
        [HttpPost]
        [Route("registration", Name = "registration")]
        public async Task<ActionResult> Registration(AgentModel.Agent Agent)
        {
            var RetVal = default(ActionResult);
            var Errors = Model.ValidateAgent(Agent);
            if (Errors.Count > 0)
            {
                var Error = string.Empty;
                foreach (var item in Errors)
                {
                    Error += item;
                }
                RetVal =  ValidationProblem(detail: Error);
            }
            else
            {
                await Model.CreateAgent(Agent);
                RetVal =  Ok();
            }
            return RetVal;
        }

        [HttpDelete]
        [Route("delete/{AgentID}", Name = "delete")]
        public async Task<ActionResult> Delete(int AgentID)
        {
            await Model.DeleteAgent(AgentID);
            return Ok();
        }
        #endregion
    }
}
