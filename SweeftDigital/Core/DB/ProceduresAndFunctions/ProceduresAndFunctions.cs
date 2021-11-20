using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Core.DB
{
    public partial class DBCoreDataContext
    {
        #region Sub Classes        
        public class ScalarFunctionResult<T>
        {
            #region Properties
            public T Value { get; set; }
            #endregion
        }
        #endregion

        #region Stored Procedures  
        public async Task AgentsCreate(int? AgentParentID, string AgentFirstname, string AgentLastname, string AgentPersonalNumber)
        {
            var PR = new PrepareQueryExecution(
             DatabaseObjectType: PrepareQueryExecution.DatabaseObjectTypes.STORED_PROCEDURE,
             DatabaseObjectName: nameof(AgentsCreate),
             ResultItemType: null,
             SqlParameters: new SqlParameter[]
             {
                 AgentParentID.ToSqlParameter(nameof(AgentParentID),SqlDbType.Int),
                 AgentFirstname.ToSqlParameter(nameof(AgentFirstname),SqlDbType.NVarChar),
                 AgentLastname.ToSqlParameter(nameof(AgentLastname),SqlDbType.NVarChar),
                 AgentPersonalNumber.ToSqlParameter(nameof(AgentPersonalNumber),SqlDbType.NVarChar)
             }
             );

            var DBResult = await Database.ExecuteSqlRawAsync(PR.SqlQuery, PR.SqlParameters);
        }

        public async Task AgentsDelete(int? AgentID)
        {
            var PR = new PrepareQueryExecution(
             DatabaseObjectType: PrepareQueryExecution.DatabaseObjectTypes.STORED_PROCEDURE,
             DatabaseObjectName: nameof(AgentsDelete),
             ResultItemType: null,
             SqlParameters: new SqlParameter[]
             {
                 AgentID.ToSqlParameter(nameof(AgentID),SqlDbType.Int),
             }
             );

            var DBResult = await Database.ExecuteSqlRawAsync(PR.SqlQuery, PR.SqlParameters);
        }

        public async Task OrdersApprove(int? OrderID)
        {
            var PR = new PrepareQueryExecution(
             DatabaseObjectType: PrepareQueryExecution.DatabaseObjectTypes.STORED_PROCEDURE,
             DatabaseObjectName: nameof(OrdersApprove),
             ResultItemType: null,
             SqlParameters: new SqlParameter[]
             {
                 OrderID.ToSqlParameter(nameof(OrderID),SqlDbType.Int),
             }
             );

            var DBResult = await Database.ExecuteSqlRawAsync(PR.SqlQuery, PR.SqlParameters);
        }

        public async Task OrdersCreate(int? AgentID, int? ProductID, int? OrderProductQuantityKg)
        {
            var PR = new PrepareQueryExecution(
             DatabaseObjectType: PrepareQueryExecution.DatabaseObjectTypes.STORED_PROCEDURE,
             DatabaseObjectName: nameof(OrdersCreate),
             ResultItemType: null,
             SqlParameters: new SqlParameter[]
             {
                 AgentID.ToSqlParameter(nameof(AgentID),SqlDbType.Int),
                 ProductID.ToSqlParameter(nameof(ProductID),SqlDbType.Int),
                 OrderProductQuantityKg.ToSqlParameter(nameof(OrderProductQuantityKg),SqlDbType.Int),
             }
             );

            var DBResult = await Database.ExecuteSqlRawAsync(PR.SqlQuery, PR.SqlParameters);
        }


        public class AgentsGetBonusesByAgentIDAndDateResultItem
        {
            public int? AgentID { get; set; }
            public string AgentFirstname { get; set; }
            public decimal? AgentBonus { get; set; }
        }
        internal virtual DbSet<AgentsGetBonusesByAgentIDAndDateResultItem> AgentsGetBonusesByAgentIDAndDateResult { get; set; }
        public IQueryable<AgentsGetBonusesByAgentIDAndDateResultItem> AgentsGetBonusesByAgentIDAndDate(int? AgentID, DateTime? OrderDateCreated)
        {
            var PR = new PrepareQueryExecution(
              DatabaseObjectType: PrepareQueryExecution.DatabaseObjectTypes.TABLE_VALUED_FUNCTION,
              DatabaseObjectName: nameof(AgentsGetBonusesByAgentIDAndDate),
              ResultItemType: typeof(AgentsGetBonusesByAgentIDAndDateResultItem),
               SqlParameters: new SqlParameter[]
                {
                    AgentID.ToSqlParameter(nameof(AgentID), SqlDbType.Int),
                    OrderDateCreated.ToSqlParameter(nameof(OrderDateCreated), SqlDbType.DateTime)
                }
            );
            var DBResult = AgentsGetBonusesByAgentIDAndDateResult.FromSqlRaw(PR.SqlQuery, PR.SqlParameters).AsNoTracking();
            return DBResult;
        }

        public class ProductListItemResultItem
        {
            public int? ProductID { get; set; }
            public string ProductName { get; set; }
            public decimal? ProductPricePerKg { get; set; }
            public int? ProductQuantityKg { get; set; }
        }
        internal virtual DbSet<ProductListItemResultItem> ProductListResult { get; set; }
        public IQueryable<ProductListItemResultItem> ProductList()
        {
            var PR = new PrepareQueryExecution(
              DatabaseObjectType: PrepareQueryExecution.DatabaseObjectTypes.TABLE_VALUED_FUNCTION,
              DatabaseObjectName: nameof(ProductList),
              ResultItemType: typeof(ProductListItemResultItem)
            );
            var DBResult = ProductListResult.FromSqlRaw(PR.SqlQuery, PR.SqlParameters).AsNoTracking();
            return DBResult;
        }

        public class OrderListItemResultItem
        {
            public int? AgentID { get; set; }
            public int? OrderID { get; set; }
            public int? ProductID { get; set; }
            public int? OrderProductQuantityKg { get; set; }
            public decimal? OrderTotalPrice { get; set; }
            public DateTime? OrderDateCreated { get; set; }
            public bool? OrderIsApproved { get; set; }
        }
        internal virtual DbSet<OrderListItemResultItem> OrderListResult { get; set; }
        public IQueryable<OrderListItemResultItem> OrderList()
        {
            var PR = new PrepareQueryExecution(
              DatabaseObjectType: PrepareQueryExecution.DatabaseObjectTypes.TABLE_VALUED_FUNCTION,
              DatabaseObjectName: nameof(OrderList),
              ResultItemType: typeof(OrderListItemResultItem)
            );
            var DBResult = OrderListResult.FromSqlRaw(PR.SqlQuery, PR.SqlParameters).AsNoTracking();
            return DBResult;
        }

        internal virtual DbSet<ScalarFunctionResult<bool>> IsEnableApproveOrderResult { get; set; }
        public async Task<bool> IsEnableApproveOrder(int? OrderID)
        {
            var PR = new PrepareQueryExecution(
                DatabaseObjectType: PrepareQueryExecution.DatabaseObjectTypes.SCALAR_VALUED_FUNCTION,
                DatabaseObjectName: nameof(IsEnableApproveOrder),
                ResultItemType: typeof(ScalarFunctionResult<string>),
                SqlParameters: new SqlParameter[]
                {
                    OrderID.ToSqlParameter(nameof(OrderID), SqlDbType.Int)
                }
            );
            var DBResult = IsEnableApproveOrderResult.FromSqlRaw(PR.SqlQuery, PR.SqlParameters).AsNoTracking();
            var DBFunctionResult = await DBResult.FirstOrDefaultAsync();
            return DBFunctionResult?.Value == true;
        }


        #endregion

        partial void OnModelCreatingPartial(ModelBuilder ModelBuilder)
        {
            ModelBuilder.Entity<ScalarFunctionResult<string>>(Entity => { Entity.HasNoKey(); });
            ModelBuilder.Entity<ScalarFunctionResult<bool>>(Entity => { Entity.HasNoKey(); });
            ModelBuilder.Entity<AgentsGetBonusesByAgentIDAndDateResultItem>(Entity => { Entity.HasNoKey(); });
            ModelBuilder.Entity<ProductListItemResultItem>(Entity => { Entity.HasNoKey(); });
            ModelBuilder.Entity<OrderListItemResultItem>(Entity => { Entity.HasNoKey(); });
        }

        #region Query Preparation
        class PrepareQueryExecution
        {
            #region Properties
            public string SqlQuery { get; set; }
            public SqlParameter[] SqlParameters { get; set; }

            string ParametersString;

            readonly DatabaseObjectTypes DatabaseObjectType;
            readonly string DatabaseObjectName;
            readonly Type ResultType;
            #endregion

            #region Constructors
            public PrepareQueryExecution(DatabaseObjectTypes DatabaseObjectType, string DatabaseObjectName, Type ResultItemType, params SqlParameter[] SqlParameters)
            {
                this.DatabaseObjectType = DatabaseObjectType;
                this.DatabaseObjectName = DatabaseObjectName;
                this.SqlParameters = SqlParameters;
                this.ResultType = ResultItemType;

                BuildParameters();

                switch (DatabaseObjectType)
                {
                    case DatabaseObjectTypes.SCALAR_VALUED_FUNCTION:
                        {
                            BuildScalarValuedFunction();
                            break;
                        }
                    case DatabaseObjectTypes.STORED_PROCEDURE:
                        {
                            BuildStoredProcedure();
                            break;
                        }
                    case DatabaseObjectTypes.TABLE_VALUED_FUNCTION:
                        {
                            BuildTableValuedFunction();
                            break;
                        }
                }
            }

            void BuildScalarValuedFunction()
            {
                SqlQuery = $"SELECT dbo.{DatabaseObjectName}({ParametersString}) as {nameof(ScalarFunctionResult<object>.Value)}";
            }

            void BuildStoredProcedure()
            {
                SqlQuery = $"EXEC dbo.{DatabaseObjectName} {ParametersString}";
            }

            void BuildTableValuedFunction()
            {
                var PropertiesStringBuilder = new StringBuilder();

                var PropertyNames = ResultType.GetProperties().Select(Item => Item.Name);
                var PropertiesString = string.Join(", ", PropertyNames);

                SqlQuery = $"SELECT {PropertiesString} FROM dbo.{DatabaseObjectName}({ParametersString})";
            }

            void BuildParameters()
            {
                var ParametersStringBuilder = new StringBuilder();

                if (SqlParameters.Length > 0)
                {
                    foreach (var P in SqlParameters)
                    {
                        ParametersStringBuilder.Append($", @{P.ParameterName}");
                        if (P.Direction == System.Data.ParameterDirection.InputOutput)
                        {
                            ParametersStringBuilder.Append(" OUTPUT");
                        }
                    }
                    ParametersStringBuilder.Remove(0, 2);
                }
                ParametersString = ParametersStringBuilder.ToString();
            }

            #endregion

            #region Enums
            public enum DatabaseObjectTypes
            {
                #region Properties
                STORED_PROCEDURE,
                TABLE_VALUED_FUNCTION,
                SCALAR_VALUED_FUNCTION
                #endregion
            }
            #endregion
        }
        #endregion
    }

    static class SqlParameterConverter
    {
        #region Methods
        public static SqlParameter ToSqlParameter(this object Parameter, string ParameterName, SqlDbType SqlDbType, bool IsOutput = false)
        {
            var ParameterValue = Parameter == null ? DBNull.Value : Parameter;
            var P = new SqlParameter(ParameterName, ParameterValue);

            P.SqlDbType = SqlDbType;

            if (Parameter != null && Parameter.GetType() == typeof(string))
            {
                P.Size = (Parameter as string).Length;
            }

            if (IsOutput)
            {
                P.Direction = ParameterDirection.InputOutput;
            }

            return P;
        }
        #endregion
    }
}
