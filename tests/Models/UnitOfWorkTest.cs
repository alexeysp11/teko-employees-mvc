using Xunit;
using TekoEmployeesMvc.Models;
using TekoEmployeesMvc.Helpers;

namespace Tests.TekoEmployeesMvc;

public class UnitOfWorkTest
{
    [Fact]
    public void Constructor_CorrectNumberOfGeneratedElements()
    {
        // Arrange
        var unitOfWork = new UnitOfWork(); 

        // Act 
        var employees = unitOfWork.GetEmployees(); 
        var vacations = unitOfWork.GetVacations(); 

        // Assert 
        Assert.True(employees.Count == ConfigHelper.EmployeeQty); 
        Assert.True(vacations.Count == ConfigHelper.VacationQty); 
    }
}