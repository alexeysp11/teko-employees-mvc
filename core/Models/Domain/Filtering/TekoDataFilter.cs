using System.Linq.Expressions;
using TekoEmployeesMvc.Helpers; 

namespace TekoEmployeesMvc.Models;

public class TekoDataFilter : ITekoDataFilter
{
    public IEnumerable<Employee> FilterEmployees(string fio, string ageMin, string ageMax, string gender, string jobTitle, string department, 
        string filterOptions, Func<Expression<Func<Employee, bool>>, List<Employee>> getEmployees)
    {
        int ageMinInt = ConfigHelper.EmployeeMinAge; 
        int ageMaxInt = ConfigHelper.EmployeeMaxAge; 
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
        return FilterEmployees(fio, ageMinInt, ageMaxInt, gender, jobTitle, department, filterOptions, getEmployees); 
    }

    public IEnumerable<Employee> FilterEmployees(string fio, int ageMin, int ageMax, string gender, string jobTitle, string department, 
        string filterOptions, Func<Expression<Func<Employee, bool>>, List<Employee>> getEmployees)
    {
        var dateMin = System.DateTime.Now.AddYears(-ageMax); 
        var dateMax = System.DateTime.Now.AddYears(-ageMin); 
        return FilterEmployees(fio, dateMin, dateMax, gender, jobTitle, department, filterOptions, getEmployees); 
    }

    public IEnumerable<Employee> FilterEmployees(string fio, System.DateTime dateMin, System.DateTime dateMax, string gender, string jobTitle, string department, 
        string filterOptions, Func<Expression<Func<Employee, bool>>, List<Employee>> getEmployees)
    {
        IEnumerable<Employee> result; 
        if (!string.IsNullOrEmpty(fio) && !string.IsNullOrEmpty(gender)
            && !string.IsNullOrEmpty(jobTitle) && !string.IsNullOrEmpty(department))
        {
            result = getEmployees(x => 
                x.FIO.Contains(fio) 
                && x.Gender.ToString() == gender
                && x.BirthDate >= dateMin 
                && x.BirthDate <= dateMax
                && x.JobTitle.ToString() == jobTitle 
                && x.Department.ToString() == department); 
        }
        else 
        {
            result = getEmployees(x => x.BirthDate >= dateMin && x.BirthDate <= dateMax); 
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