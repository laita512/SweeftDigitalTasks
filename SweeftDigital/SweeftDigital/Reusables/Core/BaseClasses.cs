using Core.DB.Modules;
using Core.Utilities;
using Microsoft.AspNetCore.Mvc;


namespace SweeftDigital.Reusables.Core
{
    [TypeFilter(typeof(BeforeWebProjectControllerLoaded), Order = 0)]
    public class WebProjectController<T> : ControllerBase
    {
        #region Properties
        public T Model { get; set; }
        #endregion        
    }

    public class WebProjectModelBase
    {
        #region Properties
        
        public DataAccessFactory DataAccessFactory { get; set; }
        public AppSettingsCollection AppSettings { get; set; }
        public IUrlHelper Url { get; set; }
        #endregion

    }
}
