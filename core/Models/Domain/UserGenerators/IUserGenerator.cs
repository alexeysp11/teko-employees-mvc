namespace TekoEmployeesMvc.Models;

public interface IEmployeeGenerator
{
    List<Employee> GenerateEmployees(
        int count, 
        System.Func<System.DateTime, System.DateTime, System.DateTime> generateDate); 
}