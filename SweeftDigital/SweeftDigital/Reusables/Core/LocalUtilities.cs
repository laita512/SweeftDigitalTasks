using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SweeftDigital.Reusables.Core
{
    public class LocalUtilities
    {
        #region Methods
        public static T GetModelFromController<T>(object Controller) where T : class
        {
            dynamic C = Controller;
            var Model = C.Model as T;
            return Model;
        }
        #endregion
    }
}
