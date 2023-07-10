using System.Linq.Expressions;
using TekoEmployeesMvc.Helpers; 

namespace TekoEmployeesMvc.Models;

public class TekoDataFilter : ITekoDataFilter
{
    public IEnumerable<User> FilterUsers(string fio, string ageFrom, string ageTo, string gender, string jobTitle, string department, 
        string filterOptions, Func<Expression<Func<User, bool>>, List<User>> getUsers)
    {
        int ageFromInt = ConfigHelper.UserMinAge; 
        int ageToInt = ConfigHelper.UserMaxAge; 
        if (!string.IsNullOrEmpty(ageFrom))
        {
            if (!System.Int32.TryParse(ageFrom, out ageFromInt)) 
                throw new System.Exception("Unable to convert string parameter 'ageFrom' to integer"); 
        }
        if (!string.IsNullOrEmpty(ageTo))
        {
            if (!System.Int32.TryParse(ageTo, out ageToInt)) 
                throw new System.Exception("Unable to convert string parameter 'ageTo' to integer"); 
        }
        
        IEnumerable<User> result; 
        var dateFrom = System.DateTime.Now.AddYears(-ageToInt); 
        var dateTo = System.DateTime.Now.AddYears(-ageFromInt); 
        if (!string.IsNullOrEmpty(fio) && !string.IsNullOrEmpty(ageFrom) 
            && !string.IsNullOrEmpty(ageTo) && !string.IsNullOrEmpty(gender)
            && !string.IsNullOrEmpty(jobTitle) && !string.IsNullOrEmpty(department))
        {
            result = getUsers(x => 
                x.FIO.Contains(fio) && x.Gender.ToString() == gender
                && x.BirthDate >= dateFrom && x.BirthDate <= dateTo
                && x.JobTitle.ToString() == jobTitle && x.Department.ToString() == department); 
        }
        else 
        {
            result = getUsers(x => 
                (string.IsNullOrEmpty(fio) || x.FIO.Contains(fio)) 
                && (string.IsNullOrEmpty(gender) || x.Gender.ToString() == gender)
                && (x.BirthDate >= dateFrom)
                && (x.BirthDate <= dateTo)
                && (string.IsNullOrEmpty(jobTitle) || x.JobTitle.ToString() == jobTitle)
                && (string.IsNullOrEmpty(department) || x.Department.ToString() == department)); 
        }
        return result; 
    }
}