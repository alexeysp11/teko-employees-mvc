using System.Linq.Expressions;

namespace TekoEmployeesMvc.Models;

public interface ITekoDataFilter
{
    IEnumerable<Employee> FilterEmployees(string fio, string ageMin, string ageMax, string gender, string jobTitle, string department, 
        string filterOptions, Func<Expression<Func<Employee, bool>>, List<Employee>> getEmployees); 
    IEnumerable<Employee> FilterEmployees(string fio, int ageMin, int ageMax, string gender, string jobTitle, string department, 
        string filterOptions, Func<Expression<Func<Employee, bool>>, List<Employee>> getEmployees); 
    IEnumerable<Employee> FilterEmployees(string fio, System.DateTime dateMin, System.DateTime dateMax, string gender, string jobTitle, string department, 
        string filterOptions, Func<Expression<Func<Employee, bool>>, List<Employee>> getEmployees); 

    IEnumerable<Vacation> FilterVacations(string fio, string ageMin, string ageMax, string gender, string jobTitle, string department, 
        string currentFio, string filterOptions, Func<Expression<Func<Employee, bool>>, List<Employee>> getEmployees,
        Func<Expression<Func<Vacation, bool>>, List<Vacation>> getVacations);  
}