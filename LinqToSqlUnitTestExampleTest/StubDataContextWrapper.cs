using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using LinqToSqlUnitTestExample;

namespace LinqToSqlUnitTestExampleTest
{
public class StubDataContextWrapper : IDataContextWrapper
{
    private readonly IEmployeeDataContext _dataContext;
    public StubDataContextWrapper(IEmployeeDataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public string GetConnectionString()
    {
        return "connectionstring";
    }

    public IEmployeeDataContext CreateDataContext()
    {
        return _dataContext;
    }
}

public class StubEmployeeTable : ITable<EmployeeEntity>
{
    protected List<EmployeeEntity> internalList;

    public StubEmployeeTable(List<EmployeeEntity> list)
    {
        internalList = list;
    }

    public void Attach(EmployeeEntity entity)
    {
    }

    public void DeleteOnSubmit(EmployeeEntity entity)
    {
        internalList.Remove(entity);
    }

    public void InsertOnSubmit(EmployeeEntity entity)
    {
        internalList.Add(entity);
    }

    public IEnumerator<EmployeeEntity> GetEnumerator()
    {
        return this.internalList.GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return this.internalList.GetEnumerator();
    }

    public Type ElementType
    {
        get { return this.internalList.AsQueryable().ElementType; }
    }

    public System.Linq.Expressions.Expression Expression
    {
        get { return this.internalList.AsQueryable().Expression; }
    }

    public IQueryProvider Provider
    {
        get { return this.internalList.AsQueryable().Provider; }
    }
}
}
