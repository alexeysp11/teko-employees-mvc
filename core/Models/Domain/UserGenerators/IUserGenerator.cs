namespace TekoEmployeesMvc.Models;

public interface IUserGenerator
{
    List<User> GenerateUsers(
        int count, 
        System.Func<System.DateTime, System.DateTime, System.DateTime> generateDate); 
}