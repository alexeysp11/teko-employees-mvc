using System; 
using Xunit;
using TekoEmployeesMvc.Models;

namespace Tests.TekoEmployeesMvc;

public class FilteredRepositoryTest
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void GetFiltered_IncorrectUid_ThrowsException(string uid)
    {
        // Arrange 
        var employees = new FilteredRepository<Employee>();
        var vacations = new FilteredRepository<Vacation>();

        // Act 
        Action actEmployees = () => employees.GetFiltered(uid); 
        Action actVacations = () => vacations.GetFiltered(uid); 

        // Assert 
        System.Exception exceptionEmployees = Assert.Throws<System.Exception>(actEmployees);
        System.Exception exceptionVacations = Assert.Throws<System.Exception>(actVacations);
    }
}