using Microsoft.AspNetCore.Mvc;
using SweeftDigital.Models;
using SweeftDigital.Reusables.Core;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace SweeftDigital.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersController : WebProjectController<OrderModel>
    {
        #region Constructors
        public OrdersController()
        {
            Model = new OrderModel();
        }
        #endregion

        #region Actions
        [HttpPost]
        [Route("create", Name = "OrderCreate")]
        public async Task<ActionResult> Create(OrderModel.Order Order)
        {
            var RetVal = default(ActionResult);
            var Errors = await Model.ValidateOrder(Order);
            if (Errors.Count > 0)
            {
                var Error = string.Empty;
                foreach (var item in Errors)
                {
                    Error += item;
                }
                RetVal = ValidationProblem(detail: Error);
            }
            else
            {
                await Model.CreateOrder(Order);
                RetVal = Ok();
            }
            return RetVal;
        }

        [HttpPost]
        [Route("Approve/{OrderID}", Name = "ApproveOrder")]
        public async Task<ActionResult> Approve(int OrderID)
        {
            if(await Model.EnableApproveOrder(OrderID))
            {
                await Model.ApproveOrder(OrderID);
            }
            else
            {
                return ValidationProblem(detail: "Not Enough Product");
            }
           
            return Ok();
        }
        #endregion
    }
}
