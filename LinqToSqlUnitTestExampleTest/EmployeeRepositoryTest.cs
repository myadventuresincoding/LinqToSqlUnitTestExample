using System;
using System.Collections.Generic;
using LinqToSqlUnitTestExample;
using Moq;
using NUnit.Framework;

namespace LinqToSqlUnitTestExampleTest
{
[TestFixture]
public class EmployeeRepositoryTest
{
    private Mock<IEmployeeDataContext> _mockEmployeeDataContext;
    private IDataContextWrapper _stubDataContextWrapper;
    private IEmployeeRepository _employeeRepository;

    [SetUp]
    public void Setup()
    {
        _mockEmployeeDataContext = new Mock<IEmployeeDataContext>();
        _stubDataContextWrapper = new StubDataContextWrapper(_mockEmployeeDataContext.Object);
        _employeeRepository = new EmployeeRepository(_stubDataContextWrapper);

        var employee1 = new EmployeeEntity { Id  = 1, FirstName = "John", LastName = "Smith", StartDate = new DateTime(2013,01,10)};
        var employee2 = new EmployeeEntity { Id  = 2, FirstName = "Frank", LastName = "Smith", StartDate = new DateTime(2013,01,15)};
        var employee3 = new EmployeeEntity { Id  = 3, FirstName = "Stan", LastName = "Johnson", StartDate = new DateTime(2013,01,20)};

        var employees = new List<EmployeeEntity> {employee1, employee2, employee3};
        _mockEmployeeDataContext.Setup(x => x.Employees()).Returns(new StubEmployeeTable(employees));
    }

    [Test]
    public void AddEmployeeShouldAddNewEmployeeWhenDoesNotExist()
    {
        _employeeRepository.AddEmployee(new EmployeeEntity { FirstName = "New", LastName = "Employee", StartDate = new DateTime(2013, 01, 25) });
        Assert.AreEqual(4, _employeeRepository.GetAllEmployees().Count);
    }

    [Test]
    public void GetAllEmployeesByIdShouldReturnEmployeeWhenIdExists()
    {
        var results = _employeeRepository.GetAllEmployees();
        Assert.AreEqual(3, results.Count);
    }

    [Test]
    public void GetEmployeeByIdShouldReturnEmployeeWhenIdExists()
    {
        var result = _employeeRepository.GetEmployee(1);
        Assert.AreEqual(1, result.Id);
    }

    [Test]
    public void GetEmployeesByLastNameShouldReturnEmployeeWhenLastNameMatches()
    {
        var results = _employeeRepository.GetEmployeesByLastName("Smith");
        Assert.AreEqual(2, results.Count);
    }

    [Test]
    public void GetEmployeesByStartDateShouldReturnEmployeeWhenStartDateIsWithinRange()
    {
        var results = _employeeRepository.GetEmployeesByStartDate(new DateTime(2013, 01, 14), new DateTime(2013, 01, 16));
        Assert.AreEqual(1, results.Count);
        Assert.AreEqual(new DateTime(2013, 01, 15), results[0].StartDate);
    }
}
}
