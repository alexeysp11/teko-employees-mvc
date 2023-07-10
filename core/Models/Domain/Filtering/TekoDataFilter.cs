using System.Linq.Expressions;
using TekoEmployeesMvc.Helpers; 

namespace TekoEmployeesMvc.Models;

public class TekoDataFilter : ITekoDataFilter
{
    public IEnumerable<User> FilterUsers(string fio, string ageMin, string ageMax, string gender, string jobTitle, string department, 
        string filterOptions, Func<Expression<Func<User, bool>>, List<User>> getUsers)
    {
        int ageMinInt = ConfigHelper.UserMinAge; 
        int ageMaxInt = ConfigHelper.UserMaxAge; 
        if (!string.IsNullOrEmpty(ageMin))
        {
            if (!System.Int32.TryParse(ageMin, out ageMinInt)) 
                throw new System.Exception("Unable to convert string parameter 'ageMin' to integer"); 
        }
        if (!string.IsNullOrEmpty(ageMax))
        {
            if (!System.Int32.TryParse(ageMax, out ageMaxInt)) 
                throw new System.Exception("Unable to convert string parameter 'ageMax' to integer"); 
        }
        return FilterUsers(fio, ageMinInt, ageMaxInt, gender, jobTitle, department, filterOptions, getUsers); 
    }

    public IEnumerable<User> FilterUsers(string fio, int ageMin, int ageMax, string gender, string jobTitle, string department, 
        string filterOptions, Func<Expression<Func<User, bool>>, List<User>> getUsers)
    {
        var dateMin = System.DateTime.Now.AddYears(-ageMax); 
        var dateMax = System.DateTime.Now.AddYears(-ageMin); 
        return FilterUsers(fio, dateMin, dateMax, gender, jobTitle, department, filterOptions, getUsers); 
    }

    public IEnumerable<User> FilterUsers(string fio, System.DateTime dateMin, System.DateTime dateMax, string gender, string jobTitle, string department, 
        string filterOptions, Func<Expression<Func<User, bool>>, List<User>> getUsers)
    {
        IEnumerable<User> result; 
        if (!string.IsNullOrEmpty(fio) && !string.IsNullOrEmpty(gender)
            && !string.IsNullOrEmpty(jobTitle) && !string.IsNullOrEmpty(department))
        {
            result = getUsers(x => 
                x.FIO.Contains(fio) 
                && x.Gender.ToString() == gender
                && x.BirthDate >= dateMin 
                && x.BirthDate <= dateMax
                && x.JobTitle.ToString() == jobTitle 
                && x.Department.ToString() == department); 
        }
        else 
        {
            result = getUsers(x => x.BirthDate >= dateMin && x.BirthDate <= dateMax); 
            if (!string.IsNullOrEmpty(fio))
                result = result.Where(x => x.FIO.Contains(fio)); 
            if (!string.IsNullOrEmpty(gender))
                result = result.Where(x => x.Gender.ToString() == gender); 
            if (!string.IsNullOrEmpty(jobTitle))
                result = result.Where(x => x.JobTitle.ToString() == jobTitle); 
            if (!string.IsNullOrEmpty(department))
                result = result.Where(x => x.Department.ToString() == department); 
        }
        return result; 
    }
}