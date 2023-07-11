using System.Collections.Generic; 
using System.Linq;
using System.Linq.Expressions;
using Xunit;
using TekoEmployeesMvc.Models;
using TekoEmployeesMvc.Helpers;

namespace Tests.TekoEmployeesMvc;

public class TekoDataFilterTest
{
    private IUnitOfWork UnitOfWork; 
    private ITekoDataFilter TekoDataFilter; 

    [Fact]
    public void FilterEmployees_EmptyParameters_ReturnsIdenticalDataset()
    {
        // Arrange
        UnitOfWork = new UnitOfWork(); 
        TekoDataFilter = new TekoDataFilter(); 
        var fio = ""; 
        var ageMin = ""; 
        var ageMax = ""; 
        var gender = ""; 
        var jobTitle = ""; 
        var department = ""; 
        var filterOptions = ""; 

        // Act 
        var employees = UnitOfWork.GetEmployees().ToList(); 
        var filtered = TekoDataFilter.FilterEmployees(fio, ageMin, ageMax, gender, jobTitle, department, filterOptions, UnitOfWork.GetEmployees).ToList(); 
        var identical = CompareLists(employees, filtered);

        // Assert 
        Assert.True(employees.Count == ConfigHelper.EmployeeQty); 
        Assert.True(employees.Count == filtered.Count); 
        Assert.True(identical); 
    }

    [Fact]
    public void FilterVacations_EmptyParameters_ReturnsEntireDataset()
    {
        // Arrange
        UnitOfWork = new UnitOfWork(); 
        TekoDataFilter = new TekoDataFilter(); 
        var fio = ""; 
        var ageMin = ""; 
        var ageMax = ""; 
        var gender = ""; 
        var jobTitle = ""; 
        var department = ""; 
        var currentFio = ""; 
        var filterOptions = ""; 

        // Act 
        var vacations = UnitOfWork.GetVacations().ToList(); 
        var filtered = TekoDataFilter.FilterVacations(fio, ageMin, ageMax, gender, jobTitle, department, currentFio, filterOptions, UnitOfWork.GetEmployees, UnitOfWork.GetVacations).ToList(); 
        var identical = CompareLists(vacations, filtered);

        // Assert 
        Assert.True(vacations.Count == ConfigHelper.VacationQty); 
        Assert.True(vacations.Count == filtered.Count); 
        Assert.True(identical); 
    }

    private bool CompareLists(List<Employee> list1, List<Employee> list2)
    {
        if (list1.Count != list2.Count) 
            return false; 
        
        foreach (var item in list1)
        {
            if (list2.Where(x => 
                x.FIO == item.FIO 
                && x.Gender == item.Gender
                && x.JobTitle == item.JobTitle
                && x.Department == item.Department
                && x.BirthDate == item.BirthDate).ToList().Count != 1) 
            {
                return false; 
            }
        }
        foreach (var item in list2)
        {
            if (list1.Where(x => 
                x.FIO == item.FIO 
                && x.Gender == item.Gender
                && x.JobTitle == item.JobTitle
                && x.Department == item.Department
                && x.BirthDate == item.BirthDate).ToList().Count != 1) 
            {
                return false; 
            }
        }
        return true; 
    }
    private bool CompareLists(List<Vacation> list1, List<Vacation> list2)
    {
        if (list1.Count != list2.Count) 
            return false; 
        
        foreach (var item in list1)
        {
            if (list2.Where(x => 
                x.Employee.FIO == item.Employee.FIO 
                && x.Employee.Gender == item.Employee.Gender
                && x.Employee.JobTitle == item.Employee.JobTitle
                && x.Employee.Department == item.Employee.Department
                && x.Employee.BirthDate == item.Employee.BirthDate
                && x.BeginDate == item.BeginDate
                && x.EndDate == item.EndDate).ToList().Count != 1) 
            {
                return false; 
            }
        }
        foreach (var item in list2)
        {
            if (list1.Where(x => 
                x.Employee.FIO == item.Employee.FIO 
                && x.Employee.Gender == item.Employee.Gender
                && x.Employee.JobTitle == item.Employee.JobTitle
                && x.Employee.Department == item.Employee.Department
                && x.Employee.BirthDate == item.Employee.BirthDate
                && x.BeginDate == item.BeginDate
                && x.EndDate == item.EndDate).ToList().Count != 1) 
            {
                return false; 
            }
        }
        return true; 
    }
}