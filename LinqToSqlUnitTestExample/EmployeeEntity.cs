using System;
using System.Data.Linq.Mapping;

namespace LinqToSqlUnitTestExample
{
    [Table(Name = "dbo.Employee")]
    public class EmployeeEntity
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)] public int Id { get; set; }
        [Column] public string FirstName { get; set; }
        [Column] public string LastName { get; set; }
        [Column] public DateTime StartDate { get; set; }

        public override string ToString()
        {
            return string.Format("Id={0}, FirstName={1}, LastName={2}, StartDate={3}", Id, FirstName, LastName, StartDate);
        }
    }
}
