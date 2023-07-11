using Xunit;
using TekoEmployeesMvc.Models;
using TekoEmployeesMvc.Helpers;

namespace Tests.TekoEmployeesMvc;

public class UnitOfWorkTest
{
    private IUnitOfWork UnitOfWork; 

    [Fact]
    public void Constructor_CorrectNumberOfGeneratedElements()
    {
        // Arrange
        UnitOfWork = new UnitOfWork(); 

        // Act 
        var employees = UnitOfWork.GetEmployees(); 
        var vacations = UnitOfWork.GetVacations(); 

        // Assert 
        Assert.True(employees.Count == ConfigHelper.EmployeeQty); 
        Assert.True(vacations.Count == ConfigHelper.VacationQty); 
    }
}