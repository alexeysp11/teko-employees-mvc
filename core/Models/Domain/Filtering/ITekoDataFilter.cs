using System.Linq.Expressions;

namespace TekoEmployeesMvc.Models;

public interface ITekoDataFilter
{
    IEnumerable<User> FilterUsers(string fio, string ageFrom, string ageTo, string gender, string jobTitle, string department, 
        string filterOptions, Func<Expression<Func<User, bool>>, List<User>> getUsers); 
}