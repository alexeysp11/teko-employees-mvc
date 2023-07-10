using System; 
using System.Linq;
using Xunit;
using TekoEmployeesMvc.Models;

namespace Tests.TekoEmployeesMvc;

public class GenericRepositoryTest
{
    private GenericRepository<User> Users; 
    private GenericRepository<Holiday> Holidays; 

    [Fact]
    public void Get_UsingTheFunctionAfterConstructor_ReturnsEmptyCollections()
    {
        // Arrange 
        Users = new GenericRepository<User>();
        Holidays = new GenericRepository<Holiday>();

        // Act 
        var userCollection = Users.Get(); 
        var holidayCollection = Holidays.Get(); 

        // Assert 
        Assert.True(userCollection.ToList().Count == 0); 
        Assert.True(holidayCollection.ToList().Count == 0); 
    }

    public void Insert_InsertOneRecord_OneElementsInsideReturnedCollection()
    {
        // Arrange 
        Users = new GenericRepository<User>();
        Holidays = new GenericRepository<Holiday>();
        var user = new User() 
        {
            FIO = "Random FIO", 
            Gender = Gender.Male, 
            JobTitle = JobTitle.ChiefExecutiveOfficer, 
            Department = Department.Administration, 
            BirthDate = new System.DateTime(1978, 12, 01)
        }; 
        var holiday = new Holiday() 
        {
            BeginDate = new System.DateTime(2023, 05, 11), 
            EndDate = new System.DateTime(2023, 05, 18), 
            User = user 
        }; 

        // Act 
        Users.Insert(user); 
        Holidays.Insert(holiday); 
        var userCollection = Users.Get(); 
        var holidayCollection = Holidays.Get(); 

        // Assert 
        Assert.True(userCollection.ToList().Count == 1); 
        Assert.True(holidayCollection.ToList().Count == 1); 
        Assert.True(userCollection.Where(x => x.FIO == user.FIO).ToList().Count == 1); 
        Assert.True(holidayCollection.Where(x => x.User.FIO == user.FIO).ToList().Count == 1); 
    }
}