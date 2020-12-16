using System;
using System.Data.Linq;

namespace LinqToSqlUnitTestExample
{
   public interface IDataContextWrapper
    {
        string GetConnectionString();
        IEmployeeDataContext CreateDataContext();
    }

    public class DataContextWrapper: IDataContextWrapper
    {
        private readonly string _connectionString;
        public DataContextWrapper(string connectionString)
        {
            _connectionString = connectionString;
        }

        public string GetConnectionString()
        {
            return _connectionString;
        }

        public IEmployeeDataContext CreateDataContext()
        {
            return new EmployeeDataContext(new DataContext(_connectionString));
        }
    }

    public interface IEmployeeDataContext : IDisposable
    {
        ITable<EmployeeEntity> Employees();
        void ExecuteCommand(string command, params object[] parameters);
        void SubmitChanges();
    }

    public class EmployeeDataContext : IEmployeeDataContext
    {
        private readonly DataContext _dataContext;

        public EmployeeDataContext(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public ITable<EmployeeEntity> Employees()
        {
            return _dataContext.GetTable<EmployeeEntity>();
        }

        public void ExecuteCommand(string command, params object[] parameters)
        {
            _dataContext.ExecuteCommand(command, parameters);
        }

        public void SubmitChanges()
        {
            _dataContext.SubmitChanges();
        }

        public void Dispose()
        {
            _dataContext.Dispose();
        }
    }
}
