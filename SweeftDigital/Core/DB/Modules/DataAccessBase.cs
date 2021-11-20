using Core.DB.Common;

namespace Core.DB.Modules
{
    public class DataAccessBase
    {
        #region Properties
        public ConnectionFactory ConnectionFactory { get; set; }
        #endregion

        #region Constructors
        public DataAccessBase(ConnectionFactory ConnectionFactory)
        {
            this.ConnectionFactory = ConnectionFactory;
        }
        #endregion
    }
}
