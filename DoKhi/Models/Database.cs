using Dapper;
using LibraryExtentions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace DoKhi.Models
{
    public abstract partial class DatabaseConnectionBase
    {
        public abstract string NameConnectionStringConfig { get; }
        public string ConnectionString => System.Configuration.ConfigurationManager.ConnectionStrings[NameConnectionStringConfig].ConnectionString;
        public IDbConnection Connection => new SqlConnection(ConnectionString);

        public void LogToFile(object message)
        {
            message.LogToFile();
        }
    }

    public static class SQLHelper
    {
        public static string ValueToInsert<T>(this T value, Func<T, string> convertValue = null)
        {
            if (value == null)
            {
                return "NULL";
            }
            if (convertValue == null)
            {
                return value.ToString();
            }
            else
            {
                return convertValue?.Invoke(value);
            }
        }

        public static bool ExistTable(this IDbConnection Connection, string tableName)
        {
            using (Connection)
            {
                string query = $@" SELECT    TABLE_NAME
                  FROM      INFORMATION_SCHEMA.TABLES
                  WHERE     TABLE_SCHEMA = 'dbo'
                            AND TABLE_NAME = '[{tableName}]' ";
                var lstTableName = Connection.Query<string>(query);
                return lstTableName.Count() > 0;
            }
        }
    }

    public abstract partial class Repository<T> : DatabaseConnectionBase
    {

        #region Simble CRUD

        //
        // Summary:
        //     Deletes a record or records in the database by ID
        //     By default deletes records in the table matching the class name
        //     -Table name can be overridden by adding an attribute on your class [Table("YourTableName")]
        //     Deletes records where the Id property and properties with the [Key] attribute
        //     match those in the database
        //     The number of records affected
        //     Supports transaction and command timeout
        //
        // Parameters:
        //   connection:
        //
        //   id:
        //
        //   transaction:
        //
        //   commandTimeout:
        //
        // Type parameters:
        //   T:
        //
        // Returns:
        //     The number of records affected
        public int Delete(object id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return MethodHelper.CatchFunction(Connection.Delete, id, transaction, commandTimeout);
        }

        //  
        // Summary:
        //     Deletes a record or records in the database that match the object passed in
        //     -By default deletes records in the table matching the class name
        //     Table name can be overridden by adding an attribute on your class [Table("YourTableName")]
        //     Supports transaction and command timeout
        //     Returns the number of records affected
        //
        // Parameters:
        //   connection:
        //
        //   entityToDelete:
        //
        //   transaction:
        //
        //   commandTimeout:
        //
        // Type parameters:
        //   T:
        //
        // Returns:
        //     The number of records affected
        public int Delete(T entityToDelete, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return MethodHelper.CatchFunction(Connection.Delete, entityToDelete, transaction, commandTimeout);
        }
        //
        // Summary:
        //     Deletes a record or records in the database by ID asynchronously
        //     By default deletes records in the table matching the class name
        //     -Table name can be overridden by adding an attribute on your class [Table("YourTableName")]
        //     Deletes records where the Id property and properties with the [Key] attribute
        //     match those in the database
        //     The number of records affected
        //     Supports transaction and command timeout
        //
        // Parameters:
        //   connection:
        //
        //   id:
        //
        //   transaction:
        //
        //   commandTimeout:
        //
        // Type parameters:
        //   T:
        //
        // Returns:
        //     The number of records affected
        public Task<int> DeleteAsync(object id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return MethodHelper.CatchFunction(Connection.DeleteAsync, id, transaction, commandTimeout);
        }
        //
        // Summary:
        //     Deletes a record or records in the database that match the object passed in asynchronously
        //     -By default deletes records in the table matching the class name
        //     Table name can be overridden by adding an attribute on your class [Table("YourTableName")]
        //     Supports transaction and command timeout
        //     Returns the number of records affected
        //
        // Parameters:
        //   connection:
        //
        //   entityToDelete:
        //
        //   transaction:
        //
        //   commandTimeout:
        //
        // Type parameters:
        //   T:
        //
        // Returns:
        //     The number of records affected
        public Task<int> DeleteAsync(T entityToDelete, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return MethodHelper.CatchFunction(Connection.DeleteAsync, entityToDelete, transaction, commandTimeout);
        }
        //
        // Summary:
        //     Deletes a list of records in the database
        //     By default deletes records in the table matching the class name
        //     -Table name can be overridden by adding an attribute on your class [Table("YourTableName")]
        //     Deletes records where that match the where clause
        //     conditions is an SQL where clause ex: "where name='bob'" or "where age>=@Age"
        //     parameters is an anonymous type to pass in named parameter values: new { Age
        //     = 15 }
        //     Supports transaction and command timeout
        //
        // Parameters:
        //   connection:
        //
        //   conditions:
        //
        //   parameters:
        //
        //   transaction:
        //
        //   commandTimeout:
        //
        // Type parameters:
        //   T:
        //
        // Returns:
        //     The number of records affected
        public int DeleteList(string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return MethodHelper.CatchFunction(Connection.DeleteList<T>, conditions, parameters, transaction, commandTimeout);
        }
        //
        // Summary:
        //     Deletes a list of records in the database
        //     By default deletes records in the table matching the class name
        //     -Table name can be overridden by adding an attribute on your class [Table("YourTableName")]
        //     Deletes records where that match the where clause
        //     whereConditions is an anonymous type to filter the results ex: new {Category
        //     = 1, SubCategory=2}
        //     The number of records affected
        //     Supports transaction and command timeout
        //
        // Parameters:
        //   connection:
        //
        //   whereConditions:
        //
        //   transaction:
        //
        //   commandTimeout:
        //
        // Type parameters:
        //   T:
        //
        // Returns:
        //     The number of records affected
        public int DeleteList<T>(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return MethodHelper.CatchFunction(Connection.DeleteList<T>, whereConditions, transaction, commandTimeout);
        }
        //
        // Summary:
        //     Deletes a list of records in the database
        //     By default deletes records in the table matching the class name
        //     -Table name can be overridden by adding an attribute on your class [Table("YourTableName")]
        //     Deletes records where that match the where clause
        //     conditions is an SQL where clause ex: "where name='bob'" or "where age>=@Age"
        //     parameters is an anonymous type to pass in named parameter values: new { Age
        //     = 15 }
        //
        // Parameters:
        //   connection:
        //
        //   conditions:
        //
        //   parameters:
        //
        //   transaction:
        //
        //   commandTimeout:
        //
        // Type parameters:
        //   T:
        //
        // Returns:
        //     The number of records affected
        public Task<int> DeleteListAsync(string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return MethodHelper.CatchFunction(Connection.DeleteListAsync<T>, conditions, parameters, transaction, commandTimeout);
        }
        //
        // Summary:
        //     Deletes a list of records in the database
        //     By default deletes records in the table matching the class name
        //     -Table name can be overridden by adding an attribute on your class [Table("YourTableName")]
        //     Deletes records where that match the where clause
        //     whereConditions is an anonymous type to filter the results ex: new {Category
        //     = 1, SubCategory=2}
        //     The number of records affected
        //     Supports transaction and command timeout
        //
        // Parameters:
        //   connection:
        //
        //   whereConditions:
        //
        //   transaction:
        //
        //   commandTimeout:
        //
        // Type parameters:
        //   T:
        //
        // Returns:
        //     The number of records affected
        public Task<int> DeleteListAsync(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return MethodHelper.CatchFunction(Connection.DeleteListAsync<T>, whereConditions, transaction, commandTimeout);
        }
        //
        // Summary:
        //     By default queries the table matching the class name
        //     -Table name can be overridden by adding an attribute on your class [Table("YourTableName")]
        //     By default filters on the Id column
        //     -Id column name can be overridden by adding an attribute on your primary key
        //     property [Key]
        //     Supports transaction and command timeout
        //     Returns a single entity by a single id from table T
        //
        // Parameters:
        //   connection:
        //
        //   id:
        //
        //   transaction:
        //
        //   commandTimeout:
        //
        // Type parameters:
        //   T:
        //
        // Returns:
        //     Returns a single entity by a single id from table T.
        public T Get(object id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return MethodHelper.CatchFunction(Connection.Get<T>, id, transaction, commandTimeout);
        }
        //
        // Summary:
        //     By default queries the table matching the class name asynchronously
        //     -Table name can be overridden by adding an attribute on your class [Table("YourTableName")]
        //     By default filters on the Id column
        //     -Id column name can be overridden by adding an attribute on your primary key
        //     property [Key]
        //     Supports transaction and command timeout
        //     Returns a single entity by a single id from table T
        //
        // Parameters:
        //   connection:
        //
        //   id:
        //
        //   transaction:
        //
        //   commandTimeout:
        //
        // Type parameters:
        //   T:
        //
        // Returns:
        //     Returns a single entity by a single id from table T.
        public Task<T> GetAsync(object id, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return MethodHelper.CatchFunction(Connection.GetAsync<T>, id, transaction, commandTimeout);
        }
        //
        // Summary:
        //     By default queries the table matching the class name
        //     -Table name can be overridden by adding an attribute on your class [Table("YourTableName")]
        //     Returns a list of all entities
        //
        // Parameters:
        //   connection:
        //
        // Type parameters:
        //   T:
        //
        // Returns:
        //     Gets a list of all entities
        public IEnumerable<T> GetList()
        {
            return MethodHelper.CatchFunction(Connection.GetList<T>);
        }
        //
        // Summary:
        //     By default queries the table matching the class name
        //     -Table name can be overridden by adding an attribute on your class [Table("YourTableName")]
        //     conditions is an SQL where clause and/or order by clause ex: "where name='bob'"
        //     or "where age>=@Age"
        //     parameters is an anonymous type to pass in named parameter values: new { Age
        //     = 15 }
        //     Supports transaction and command timeout
        //     Returns a list of entities that match where conditions
        //
        // Parameters:
        //   connection:
        //
        //   conditions:
        //
        //   parameters:
        //
        //   transaction:
        //
        //   commandTimeout:
        //
        // Type parameters:
        //   T:
        //
        // Returns:
        //     Gets a list of entities with optional SQL where conditions
        public IList<T> GetList(string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return MethodHelper.CatchFunction(Connection.GetList<T>, conditions, parameters, transaction, commandTimeout).ToList();
        }
        //
        // Summary:
        //     By default queries the table matching the class name
        //     -Table name can be overridden by adding an attribute on your class [Table("YourTableName")]
        //     whereConditions is an anonymous type to filter the results ex: new {Category
        //     = 1, SubCategory=2}
        //     Supports transaction and command timeout
        //     Returns a list of entities that match where conditions
        //
        // Parameters:
        //   connection:
        //
        //   whereConditions:
        //
        //   transaction:
        //
        //   commandTimeout:
        //
        // Type parameters:
        //   T:
        //
        // Returns:
        //     Gets a list of entities with optional exact match where conditions
        public IList<T> GetList(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return MethodHelper.CatchFunction(Connection.GetList<T>, whereConditions, transaction, commandTimeout).ToList();
        }
        //
        // Summary:
        //     By default queries the table matching the class name asynchronously
        //     -Table name can be overridden by adding an attribute on your class [Table("YourTableName")]
        //     Returns a list of all entities
        //
        // Parameters:
        //   connection:
        //
        // Type parameters:
        //   T:
        //
        // Returns:
        //     Gets a list of all entities
        public Task<IEnumerable<T>> GetListAsync()
        {
            return MethodHelper.CatchFunction(Connection.GetListAsync<T>);
        }
        //
        // Summary:
        //     By default queries the table matching the class name
        //     -Table name can be overridden by adding an attribute on your class [Table("YourTableName")]
        //     conditions is an SQL where clause and/or order by clause ex: "where name='bob'"
        //     or "where age>=@Age"
        //     parameters is an anonymous type to pass in named parameter values: new { Age
        //     = 15 }
        //     Supports transaction and command timeout
        //     Returns a list of entities that match where conditions
        //
        // Parameters:
        //   connection:
        //
        //   conditions:
        //
        //   parameters:
        //
        //   transaction:
        //
        //   commandTimeout:
        //
        // Type parameters:
        //   T:
        //
        // Returns:
        //     Gets a list of entities with optional SQL where conditions
        public Task<IEnumerable<T>> GetListAsync(string conditions, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return MethodHelper.CatchFunction(Connection.GetListAsync<T>, conditions, parameters, transaction, commandTimeout);
        }
        //
        // Summary:
        //     By default queries the table matching the class name asynchronously
        //     -Table name can be overridden by adding an attribute on your class [Table("YourTableName")]
        //     whereConditions is an anonymous type to filter the results ex: new {Category
        //     = 1, SubCategory=2}
        //     Supports transaction and command timeout
        //     Returns a list of entities that match where conditions
        //
        // Parameters:
        //   connection:
        //
        //   whereConditions:
        //
        //   transaction:
        //
        //   commandTimeout:
        //
        // Type parameters:
        //   T:
        //
        // Returns:
        //     Gets a list of entities with optional exact match where conditions
        public Task<IEnumerable<T>> GetListAsync<T>(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return MethodHelper.CatchFunction(Connection.GetListAsync<T>, whereConditions, transaction, commandTimeout);
        }
        //
        // Summary:
        //     By default queries the table matching the class name
        //     -Table name can be overridden by adding an attribute on your class [Table("YourTableName")]
        //     conditions is an SQL where clause ex: "where name='bob'" or "where age>=@Age"
        //     - not required
        //     orderby is a column or list of columns to order by ex: "lastname, age desc" -
        //     not required - default is by primary key
        //     parameters is an anonymous type to pass in named parameter values: new { Age
        //     = 15 }
        //     Supports transaction and command timeout
        //     Returns a list of entities that match where conditions
        //
        // Parameters:
        //   connection:
        //
        //   pageNumber:
        //
        //   rowsPerPage:
        //
        //   conditions:
        //
        //   orderby:
        //
        //   parameters:
        //
        //   transaction:
        //
        //   commandTimeout:
        //
        // Type parameters:
        //   T:
        //
        // Returns:
        //     Gets a paged list of entities with optional exact match where conditions
        public IList<T> GetListPaged(int pageNumber, int rowsPerPage, string conditions, string orderby, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return MethodHelper.CatchFunction(Connection.GetListPaged<T>, pageNumber, rowsPerPage, conditions, orderby, parameters, transaction, commandTimeout).ToList();
        }
        //
        // Summary:
        //     By default queries the table matching the class name
        //     -Table name can be overridden by adding an attribute on your class [Table("YourTableName")]
        //     conditions is an SQL where clause ex: "where name='bob'" or "where age>=@Age"
        //     - not required
        //     orderby is a column or list of columns to order by ex: "lastname, age desc" -
        //     not required - default is by primary key
        //     parameters is an anonymous type to pass in named parameter values: new { Age
        //     = 15 }
        //     Returns a list of entities that match where conditions
        //
        // Parameters:
        //   connection:
        //
        //   pageNumber:
        //
        //   rowsPerPage:
        //
        //   conditions:
        //
        //   orderby:
        //
        //   parameters:
        //
        //   transaction:
        //
        //   commandTimeout:
        //
        // Type parameters:
        //   T:
        //
        // Returns:
        //     Gets a list of entities with optional exact match where conditions
        public Task<IEnumerable<T>> GetListPagedAsync(int pageNumber, int rowsPerPage, string conditions, string orderby, object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return MethodHelper.CatchFunction(Connection.GetListPagedAsync<T>, pageNumber, rowsPerPage, conditions, orderby, parameters, transaction, commandTimeout);
        }
        //
        // Summary:
        //     Inserts a row into the database
        //     By default inserts into the table matching the class name
        //     -Table name can be overridden by adding an attribute on your class [Table("YourTableName")]
        //     Insert filters out Id column and any columns with the [Key] attribute
        //     Properties marked with attribute [Editable(false)] and complex types are ignored
        //     Supports transaction and command timeout
        //     Returns the ID (primary key) of the newly inserted record if it is identity using
        //     the int? type, otherwise null
        //
        // Parameters:
        //   connection:
        //
        //   entityToInsert:
        //
        //   transaction:
        //
        //   commandTimeout:
        //
        // Returns:
        //     The ID (primary key) of the newly inserted record if it is identity using the
        //     int? type, otherwise null
        public int? Insert(T entityToInsert, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return MethodHelper.CatchFunction(Connection.Insert, entityToInsert, transaction, commandTimeout);
        }
        //
        // Summary:
        //     Inserts a row into the database, using ONLY the properties defined by T
        //     By default inserts into the table matching the class name
        //     -Table name can be overridden by adding an attribute on your class [Table("YourTableName")]
        //     Insert filters out Id column and any columns with the [Key] attribute
        //     Properties marked with attribute [Editable(false)] and complex types are ignored
        //     Supports transaction and command timeout
        //     Returns the ID (primary key) of the newly inserted record if it is identity using
        //     the defined type, otherwise null
        //
        // Parameters:
        //   connection:
        //
        //   entityToInsert:
        //
        //   transaction:
        //
        //   commandTimeout:
        //
        // Returns:
        //     The ID (primary key) of the newly inserted record if it is identity using the
        //     defined type, otherwise null
        public TKey Insert<TKey>(T entityToInsert, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return MethodHelper.CatchFunction(Connection.Insert<TKey, T>, entityToInsert, transaction, commandTimeout);
        }
        //
        // Summary:
        //     Inserts a row into the database, using ONLY the properties defined by T
        //     By default inserts into the table matching the class name
        //     -Table name can be overridden by adding an attribute on your class [Table("YourTableName")]
        //     Insert filters out Id column and any columns with the [Key] attribute
        //     Properties marked with attribute [Editable(false)] and complex types are ignored
        //     Supports transaction and command timeout
        //     Returns the ID (primary key) of the newly inserted record if it is identity using
        //     the defined type, otherwise null
        //
        // Parameters:
        //   connection:
        //
        //   entityToInsert:
        //
        //   transaction:
        //
        //   commandTimeout:
        //
        // Returns:
        //     The ID (primary key) of the newly inserted record if it is identity using the
        //     defined type, otherwise null
        public Task<TKey> InsertAsync<TKey>(T entityToInsert, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return MethodHelper.CatchFunction(Connection.InsertAsync<TKey, T>, entityToInsert, transaction, commandTimeout);
        }
        //
        // Summary:
        //     Inserts a row into the database asynchronously
        //     By default inserts into the table matching the class name
        //     -Table name can be overridden by adding an attribute on your class [Table("YourTableName")]
        //     Insert filters out Id column and any columns with the [Key] attribute
        //     Properties marked with attribute [Editable(false)] and complex types are ignored
        //     Supports transaction and command timeout
        //     Returns the ID (primary key) of the newly inserted record if it is identity using
        //     the int? type, otherwise null
        //
        // Parameters:
        //   connection:
        //
        //   entityToInsert:
        //
        //   transaction:
        //
        //   commandTimeout:
        //
        // Returns:
        //     The ID (primary key) of the newly inserted record if it is identity using the
        //     int? type, otherwise null
        public Task<int?> InsertAsync(T entityToInsert, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return MethodHelper.CatchFunction(Connection.InsertAsync, entityToInsert, transaction, commandTimeout);
        }
        //
        // Summary:
        //     By default queries the table matching the class name
        //     -Table name can be overridden by adding an attribute on your class [Table("YourTableName")]
        //     Returns a number of records entity by a single id from table T
        //     Supports transaction and command timeout
        //     conditions is an SQL where clause ex: "where name='bob'" or "where age>=@Age"
        //     - not required
        //     parameters is an anonymous type to pass in named parameter values: new { Age
        //     = 15 }
        //
        // Parameters:
        //   connection:
        //
        //   conditions:
        //
        //   parameters:
        //
        //   transaction:
        //
        //   commandTimeout:
        //
        // Type parameters:
        //   T:
        //
        // Returns:
        //     Returns a count of records.
        public int RecordCount(string conditions = "", object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return MethodHelper.CatchFunction(Connection.RecordCount<T>, conditions, parameters, transaction, commandTimeout);
        }
        //
        // Summary:
        //     By default queries the table matching the class name
        //     -Table name can be overridden by adding an attribute on your class [Table("YourTableName")]
        //     Returns a number of records entity by a single id from table T
        //     Supports transaction and command timeout
        //     whereConditions is an anonymous type to filter the results ex: new {Category
        //     = 1, SubCategory=2}
        //
        // Parameters:
        //   connection:
        //
        //   whereConditions:
        //
        //   transaction:
        //
        //   commandTimeout:
        //
        // Type parameters:
        //   T:
        //
        // Returns:
        //     Returns a count of records.
        public int RecordCount(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return MethodHelper.CatchFunction(Connection.RecordCount<T>, whereConditions, transaction, commandTimeout);
        }
        //
        // Summary:
        //     By default queries the table matching the class name
        //     -Table name can be overridden by adding an attribute on your class [Table("YourTableName")]
        //     conditions is an SQL where clause ex: "where name='bob'" or "where age>=@Age"
        //     - not required
        //     parameters is an anonymous type to pass in named parameter values: new { Age
        //     = 15 }
        //     Supports transaction and command timeout
        //     ///
        //
        // Parameters:
        //   connection:
        //
        //   conditions:
        //
        //   parameters:
        //
        //   transaction:
        //
        //   commandTimeout:
        //
        // Type parameters:
        //   T:
        //
        // Returns:
        //     Returns a count of records.
        public Task<int> RecordCountAsync(string conditions = "", object parameters = null, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return MethodHelper.CatchFunction(Connection.RecordCountAsync<T>, conditions, parameters, transaction, commandTimeout);
        }
        //
        // Summary:
        //     By default queries the table matching the class name
        //     -Table name can be overridden by adding an attribute on your class [Table("YourTableName")]
        //     Returns a number of records entity by a single id from table T
        //     Supports transaction and command timeout
        //     whereConditions is an anonymous type to filter the results ex: new {Category
        //     = 1, SubCategory=2}
        //
        // Parameters:
        //   connection:
        //
        //   whereConditions:
        //
        //   transaction:
        //
        //   commandTimeout:
        //
        // Type parameters:
        //   T:
        //
        // Returns:
        //     Returns a count of records.
        public Task<int> RecordCountAsync(object whereConditions, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return MethodHelper.CatchFunction(Connection.RecordCountAsync<T>, whereConditions, transaction, commandTimeout);
        }
        //
        // Summary:
        //     Updates a record or records in the database with only the properties of TEntity
        //     By default updates records in the table matching the class name
        //     -Table name can be overridden by adding an attribute on your class [Table("YourTableName")]
        //     Updates records where the Id property and properties with the [Key] attribute
        //     match those in the database.
        //     Properties marked with attribute [Editable(false)] and complex types are ignored
        //     Supports transaction and command timeout
        //     Returns number of rows affected
        //
        // Parameters:
        //   connection:
        //
        //   entityToUpdate:
        //
        //   transaction:
        //
        //   commandTimeout:
        //
        // Returns:
        //     The number of affected records
        public int Update(T entityToUpdate, IDbTransaction transaction = null, int? commandTimeout = null)
        {
            return MethodHelper.CatchFunction(Connection.Update, entityToUpdate, transaction, commandTimeout);
        }
        //
        // Summary:
        //     Updates a record or records in the database asynchronously
        //     By default updates records in the table matching the class name
        //     -Table name can be overridden by adding an attribute on your class [Table("YourTableName")]
        //     Updates records where the Id property and properties with the [Key] attribute
        //     match those in the database.
        //     Properties marked with attribute [Editable(false)] and complex types are ignored
        //     Supports transaction and command timeout
        //     Returns number of rows affected
        //
        // Parameters:
        //   connection:
        //
        //   entityToUpdate:
        //
        //   transaction:
        //
        //   commandTimeout:
        //
        // Returns:
        //     The number of affected records
        public async Task<int> UpdateAsync(T entityToUpdate, IDbTransaction transaction = null, int? commandTimeout = null, CancellationToken? token = null)
        {
            return await MethodHelper.CatchFunction(Connection.UpdateAsync, entityToUpdate, transaction, commandTimeout, token);
        }

        #endregion

        #region DAPPER

        //
        // Summary:
        //     Return a sequence of dynamic objects with properties matching the columns.
        //
        // Parameters:
        //   cnn:
        //     The connection to query on.
        //
        //   sql:
        //     The SQL to execute for the query.
        //
        //   param:
        //     The parameters to pass, if any.
        //
        //   transaction:
        //     The transaction to use, if any.
        //
        //   buffered:
        //     Whether to buffer the results in memory.
        //
        //   commandTimeout:
        //     The command timeout (in seconds).
        //
        //   commandType:
        //     The type of command to execute.
        //
        // Remarks:
        //     Note: each row can be accessed via "dynamic", or by casting to an IDictionary<string,object>
        public IEnumerable<T> Query(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return MethodHelper.CatchFunction(Connection.Query<T>, sql, param, transaction, buffered, commandTimeout, commandType);
        }

        //
        // Summary:
        //     Execute parameterized SQL.
        //
        // Parameters:
        //   cnn:
        //     The connection to query on.
        //
        //   sql:
        //     The SQL to execute for this query.
        //
        //   param:
        //     The parameters to use for this query.
        //
        //   transaction:
        //     The transaction to use for this query.
        //
        //   commandTimeout:
        //     Number of seconds before command execution timeout.
        //
        //   commandType:
        //     Is it a stored proc or a batch?
        //
        // Returns:
        //     The number of rows affected.
        public int Execute(string sql, object param = null, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null)
        {
            return MethodHelper.CatchFunction(Connection.Execute, sql, param, transaction, commandTimeout, commandType);
        }
        //
        // Summary:
        //     Execute parameterized SQL.
        //
        // Parameters:
        //   cnn:
        //     The connection to execute on.
        //
        //   command:
        //     The command to execute on this connection.
        //
        // Returns:
        //     The number of rows affected.
        public int Execute(CommandDefinition command)
        {
            return MethodHelper.CatchFunction(Connection.Execute, command);
        }

        #endregion


        #region MyFunc

        #endregion
    }
}
