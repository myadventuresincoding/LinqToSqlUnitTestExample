using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace LinqToSqlUnitTestExample
{
public static class EmployeeRepositoryFactory
{
    public static IEmployeeRepository Create()
    {
        return new EmployeeRepository(new DataContextWrapper(ConfigurationManager.ConnectionStrings["EmployeeDatabaseConnection"].ConnectionString));
    }
}

    public interface IEmployeeRepository
    {
        string GetDatabaseConnectionString();
        EmployeeEntity AddEmployee(EmployeeEntity employee);
        EmployeeEntity GetEmployee(int id);
        List<EmployeeEntity> GetAllEmployees();
        List<EmployeeEntity> GetEmployeesByLastName(string lastName);
        List<EmployeeEntity> GetEmployeesByStartDate(DateTime minDate, DateTime maxDate);
    }

    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly IDataContextWrapper _dataContextWrapper;

        public EmployeeRepository(IDataContextWrapper dataContextWrapper)
        {
            _dataContextWrapper = dataContextWrapper;
        }

        public string GetDatabaseConnectionString()
        {
            return _dataContextWrapper.GetConnectionString();
        }

        public EmployeeEntity AddEmployee(EmployeeEntity employee)
        {       
            using (var db = _dataContextWrapper.CreateDataContext())
            {
                db.Employees().InsertOnSubmit(employee);
                db.SubmitChanges();
                return employee;
            }    
        }

        public EmployeeEntity GetEmployee(int id)
        {
            using (var db = _dataContextWrapper.CreateDataContext())
            {
                return (from employee in db.Employees() where employee.Id == id select employee).SingleOrDefault();
            }        
        }

        public List<EmployeeEntity> GetAllEmployees()
        {
            using (var db = _dataContextWrapper.CreateDataContext())
            {
                return (from employee in db.Employees() select employee).ToList();
            }  
        }

        public List<EmployeeEntity> GetEmployeesByLastName(string lastName)
        { 
            using (var db = _dataContextWrapper.CreateDataContext())
            {
                return (from employee in db.Employees() where employee.LastName == lastName select employee).ToList();
            }    
        }

        public List<EmployeeEntity> GetEmployeesByStartDate(DateTime minDate, DateTime maxDate)
        {
            using (var db = _dataContextWrapper.CreateDataContext())
            {
                return (from employee in db.Employees() where employee.StartDate >= minDate && employee.StartDate <= maxDate select employee).ToList();
            }
        }
    }
}
