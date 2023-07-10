using System.Linq.Expressions;

namespace TekoEmployeesMvc.Models;

public interface ITekoDataFilter
{
    IEnumerable<User> FilterUsers(string fio, string ageMin, string ageMax, string gender, string jobTitle, string department, 
        string filterOptions, Func<Expression<Func<User, bool>>, List<User>> getUsers); 
    IEnumerable<User> FilterUsers(string fio, int ageMin, int ageMax, string gender, string jobTitle, string department, 
        string filterOptions, Func<Expression<Func<User, bool>>, List<User>> getUsers); 
    IEnumerable<User> FilterUsers(string fio, System.DateTime dateMin, System.DateTime dateMax, string gender, string jobTitle, string department, 
        string filterOptions, Func<Expression<Func<User, bool>>, List<User>> getUsers); 
}