using System; 
using Xunit;
using TekoEmployeesMvc.Models;

namespace Tests.TekoEmployeesMvc;

public class FilteredRepositoryTest
{
    private FilteredRepository<User> Users; 
    private FilteredRepository<Holiday> Holidays; 

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void GetFiltered_IncorrectUid_ThrowsException(string uid)
    {
        // Arrange 
        Users = new FilteredRepository<User>();
        Holidays = new FilteredRepository<Holiday>();

        // Act 
        Action actUsers = () => Users.GetFiltered(uid); 
        Action actHolidays = () => Holidays.GetFiltered(uid); 

        // Assert 
        System.Exception exceptionUsers = Assert.Throws<System.Exception>(actUsers);
        System.Exception exceptionHolidays = Assert.Throws<System.Exception>(actHolidays);
    }
}