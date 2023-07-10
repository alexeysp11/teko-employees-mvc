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
        var users = UnitOfWork.GetUsers(); 
        var holidays = UnitOfWork.GetHolidays(); 

        // Assert 
        Assert.True(users.Count == ConfigHelper.UserQty); 
        Assert.True(holidays.Count == ConfigHelper.HolidayQty); 
    }
}