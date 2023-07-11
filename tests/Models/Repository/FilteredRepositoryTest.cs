using System; 
using Xunit;
using TekoEmployeesMvc.Models;

namespace Tests.TekoEmployeesMvc;

public class FilteredRepositoryTest
{
    private FilteredRepository<Employee> Employees; 
    private FilteredRepository<Vacation> Vacations; 

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void GetFiltered_IncorrectUid_ThrowsException(string uid)
    {
        // Arrange 
        Employees = new FilteredRepository<Employee>();
        Vacations = new FilteredRepository<Vacation>();

        // Act 
        Action actEmployees = () => Employees.GetFiltered(uid); 
        Action actVacations = () => Vacations.GetFiltered(uid); 

        // Assert 
        System.Exception exceptionEmployees = Assert.Throws<System.Exception>(actEmployees);
        System.Exception exceptionVacations = Assert.Throws<System.Exception>(actVacations);
    }
}