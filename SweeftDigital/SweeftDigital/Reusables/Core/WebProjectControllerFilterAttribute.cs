using Core.DB.Modules;
using Core.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Threading.Tasks;

namespace SweeftDigital.Reusables.Core
{
    public class BeforeWebProjectControllerLoaded : IAsyncActionFilter
    {
        AppSettingsCollection AppSettings;

        public BeforeWebProjectControllerLoaded(AppSettingsCollection AppSettings)
        {
            this.AppSettings = AppSettings;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext FilterContext, ActionExecutionDelegate next)
        {
            var C = FilterContext.Controller as ControllerBase;
            var Model = LocalUtilities.GetModelFromController<WebProjectModelBase>(C);
            if (Model != null)
            {
                var ActionDescriptor = FilterContext.ActionDescriptor as ControllerActionDescriptor;

                Model.AppSettings = AppSettings;
                Model.DataAccessFactory = new DataAccessFactory(AppSettings);

                await next();
            }
        }

    }
}
