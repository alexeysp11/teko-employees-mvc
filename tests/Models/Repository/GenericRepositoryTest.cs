using System; 
using System.Linq;
using Xunit;
using TekoEmployeesMvc.Models;

namespace Tests.TekoEmployeesMvc;

public class GenericRepositoryTest
{
    private GenericRepository<Employee> Employees; 
    private GenericRepository<Vacation> Vacations; 

    [Fact]
    public void Get_UsingTheFunctionAfterConstructor_ReturnsEmptyCollections()
    {
        // Arrange 
        Employees = new GenericRepository<Employee>();
        Vacations = new GenericRepository<Vacation>();

        // Act 
        var employeeCollection = Employees.Get(); 
        var vacationCollection = Vacations.Get(); 

        // Assert 
        Assert.True(employeeCollection.ToList().Count == 0); 
        Assert.True(vacationCollection.ToList().Count == 0); 
    }

    public void Insert_InsertOneRecord_OneElementsInsideReturnedCollection()
    {
        // Arrange 
        Employees = new GenericRepository<Employee>();
        Vacations = new GenericRepository<Vacation>();
        var employee = new Employee() 
        {
            FIO = "Random FIO", 
            Gender = Gender.Male, 
            JobTitle = JobTitle.ChiefExecutiveOfficer, 
            Department = Department.Administration, 
            BirthDate = new System.DateTime(1978, 12, 01)
        }; 
        var vacation = new Vacation() 
        {
            BeginDate = new System.DateTime(2023, 05, 11), 
            EndDate = new System.DateTime(2023, 05, 18), 
            Employee = employee 
        }; 

        // Act 
        Employees.Insert(employee); 
        Vacations.Insert(vacation); 
        var employeeCollection = Employees.Get(); 
        var vacationCollection = Vacations.Get(); 

        // Assert 
        Assert.True(employeeCollection.ToList().Count == 1); 
        Assert.True(vacationCollection.ToList().Count == 1); 
        Assert.True(employeeCollection.Where(x => x.FIO == employee.FIO).ToList().Count == 1); 
        Assert.True(vacationCollection.Where(x => x.Employee.FIO == employee.FIO).ToList().Count == 1); 
    }
}