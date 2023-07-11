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

    #region FilterEmployees
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
    public void FilterEmployees_OnlyFioSpecified()
    {
        // Arrange
        UnitOfWork = new UnitOfWork(); 
        TekoDataFilter = new TekoDataFilter(); 
        var ageMin = ""; 
        var ageMax = ""; 
        var gender = ""; 
        var jobTitle = ""; 
        var department = ""; 
        var filterOptions = ""; 

        // Act 
        var employees = UnitOfWork.GetEmployees().ToList(); 
        var fio = employees.FirstOrDefault().FIO; 
        var referenceList = employees.Where(x => x.FIO == fio).ToList(); 
        var filtered = TekoDataFilter.FilterEmployees(fio, ageMin, ageMax, gender, jobTitle, department, filterOptions, UnitOfWork.GetEmployees).ToList(); 
        var identical = CompareLists(referenceList, filtered);

        // Assert 
        Assert.True(filtered.Count == referenceList.Count); 
        Assert.True(identical); 
    }

    [Fact]
    public void FilterEmployees_OnlyAgeMinSpecified()
    {
        // Arrange
        UnitOfWork = new UnitOfWork(); 
        TekoDataFilter = new TekoDataFilter(); 
        var fio = ""; 
        var ageMin = 40; 
        var ageMax = ""; 
        var gender = ""; 
        var jobTitle = ""; 
        var department = ""; 
        var filterOptions = ""; 

        // Act 
        var employees = UnitOfWork.GetEmployees().ToList(); 
        var referenceList = employees.Where(x => x.BirthDate <= System.DateTime.Now.AddYears(-ageMin)).ToList(); 
        var filtered = TekoDataFilter.FilterEmployees(fio, ageMin.ToString(), ageMax, gender, jobTitle, department, filterOptions, UnitOfWork.GetEmployees).ToList(); 
        var identical = CompareLists(referenceList, filtered);

        // Assert 
        Assert.True(filtered.Count == referenceList.Count); 
        Assert.True(identical); 
    }

    [Fact]
    public void FilterEmployees_OnlyAgeMaxSpecified()
    {
        // Arrange
        UnitOfWork = new UnitOfWork(); 
        TekoDataFilter = new TekoDataFilter(); 
        var fio = ""; 
        var ageMin = ""; 
        var ageMax = 50; 
        var gender = ""; 
        var jobTitle = ""; 
        var department = ""; 
        var filterOptions = ""; 

        // Act 
        var employees = UnitOfWork.GetEmployees().ToList(); 
        var referenceList = employees.Where(x => x.BirthDate >= System.DateTime.Now.AddYears(-ageMax)).ToList(); 
        var filtered = TekoDataFilter.FilterEmployees(fio, ageMin, ageMax.ToString(), gender, jobTitle, department, filterOptions, UnitOfWork.GetEmployees).ToList(); 
        var identical = CompareLists(referenceList, filtered);

        // Assert 
        Assert.True(filtered.Count == referenceList.Count); 
        Assert.True(identical); 
    }

    [Fact]
    public void FilterEmployees_OnlyAgeSpecified()
    {
        // Arrange
        UnitOfWork = new UnitOfWork(); 
        TekoDataFilter = new TekoDataFilter(); 
        var fio = ""; 
        var ageMin = 30; 
        var ageMax = 50; 
        var gender = ""; 
        var jobTitle = ""; 
        var department = ""; 
        var filterOptions = ""; 

        // Act 
        var employees = UnitOfWork.GetEmployees().ToList(); 
        var referenceList = employees.Where(x => 
            x.BirthDate <= System.DateTime.Now.AddYears(-ageMin)
            && x.BirthDate >= System.DateTime.Now.AddYears(-ageMax)).ToList(); 
        var filtered = TekoDataFilter.FilterEmployees(fio, ageMin.ToString(), ageMax.ToString(), gender, jobTitle, department, filterOptions, UnitOfWork.GetEmployees).ToList(); 
        var identical = CompareLists(referenceList, filtered);

        // Assert 
        Assert.True(filtered.Count == referenceList.Count); 
        Assert.True(identical); 
    }

    [Fact]
    public void FilterEmployees_OnlyGenderSpecified()
    {
        // Arrange
        UnitOfWork = new UnitOfWork(); 
        TekoDataFilter = new TekoDataFilter(); 
        var fio = ""; 
        var ageMin = ""; 
        var ageMax = ""; 
        var gender = Gender.Male.ToString(); 
        var jobTitle = ""; 
        var department = ""; 
        var filterOptions = ""; 

        // Act 
        var employees = UnitOfWork.GetEmployees().ToList(); 
        var referenceList = employees.Where(x => x.Gender.ToString() == gender).ToList(); 
        var filtered = TekoDataFilter.FilterEmployees(fio, ageMin, ageMax, gender, jobTitle, department, filterOptions, UnitOfWork.GetEmployees).ToList(); 
        var identical = CompareLists(referenceList, filtered);

        // Assert 
        Assert.True(filtered.Count == referenceList.Count); 
        Assert.True(identical); 
    }

    [Fact]
    public void FilterEmployees_OnlyJobTitleSpecified()
    {
        // Arrange
        UnitOfWork = new UnitOfWork(); 
        TekoDataFilter = new TekoDataFilter(); 
        var fio = ""; 
        var ageMin = ""; 
        var ageMax = ""; 
        var gender = ""; 
        var jobTitle = JobTitle.DevelopmentLead.ToString(); 
        var department = ""; 
        var filterOptions = ""; 

        // Act 
        var employees = UnitOfWork.GetEmployees().ToList(); 
        var referenceList = employees.Where(x => x.JobTitle.ToString() == jobTitle).ToList(); 
        var filtered = TekoDataFilter.FilterEmployees(fio, ageMin, ageMax, gender, jobTitle, department, filterOptions, UnitOfWork.GetEmployees).ToList(); 
        var identical = CompareLists(referenceList, filtered);

        // Assert 
        Assert.True(filtered.Count == referenceList.Count); 
        Assert.True(identical); 
    }

    [Fact]
    public void FilterEmployees_OnlyDepartmentSpecified()
    {
        // Arrange
        UnitOfWork = new UnitOfWork(); 
        TekoDataFilter = new TekoDataFilter(); 
        var fio = ""; 
        var ageMin = ""; 
        var ageMax = ""; 
        var gender = ""; 
        var jobTitle = ""; 
        var department = Department.CRMDevelopment.ToString(); 
        var filterOptions = ""; 

        // Act 
        var employees = UnitOfWork.GetEmployees().ToList(); 
        var referenceList = employees.Where(x => x.Department.ToString() == department).ToList(); 
        var filtered = TekoDataFilter.FilterEmployees(fio, ageMin, ageMax, gender, jobTitle, department, filterOptions, UnitOfWork.GetEmployees).ToList(); 
        var identical = CompareLists(referenceList, filtered);

        // Assert 
        Assert.True(filtered.Count == referenceList.Count); 
        Assert.True(identical); 
    }

    [Fact]
    public void FilterEmployees_JobTitleAndDepartmentSpecified()
    {
        // Arrange
        UnitOfWork = new UnitOfWork(); 
        TekoDataFilter = new TekoDataFilter(); 
        var fio = ""; 
        var ageMin = ""; 
        var ageMax = ""; 
        var gender = ""; 
        var jobTitle = JobTitle.DevelopmentLead.ToString(); 
        var department = Department.CRMDevelopment.ToString(); 
        var filterOptions = ""; 

        // Act 
        var employees = UnitOfWork.GetEmployees().ToList(); 
        var referenceList = employees.Where(x => 
            x.JobTitle.ToString() == jobTitle
            && x.Department.ToString() == department).ToList(); 
        var filtered = TekoDataFilter.FilterEmployees(fio, ageMin, ageMax, gender, jobTitle, department, filterOptions, UnitOfWork.GetEmployees).ToList(); 
        var identical = CompareLists(referenceList, filtered);

        // Assert 
        Assert.True(filtered.Count == referenceList.Count); 
        Assert.True(identical); 
    }

    [Fact]
    public void FilterEmployees_GenderAndJobTitleSpecified()
    {
        // Arrange
        UnitOfWork = new UnitOfWork(); 
        TekoDataFilter = new TekoDataFilter(); 
        var fio = ""; 
        var ageMin = ""; 
        var ageMax = ""; 
        var gender = Gender.Female.ToString(); 
        var jobTitle = JobTitle.DevelopmentLead.ToString(); 
        var department = ""; 
        var filterOptions = ""; 

        // Act 
        var employees = UnitOfWork.GetEmployees().ToList(); 
        var referenceList = employees.Where(x => 
            x.Gender.ToString() == gender
            && x.JobTitle.ToString() == jobTitle).ToList(); 
        var filtered = TekoDataFilter.FilterEmployees(fio, ageMin, ageMax, gender, jobTitle, department, filterOptions, UnitOfWork.GetEmployees).ToList(); 
        var identical = CompareLists(referenceList, filtered);

        // Assert 
        Assert.True(filtered.Count == referenceList.Count); 
        Assert.True(identical); 
    }

    [Fact]
    public void FilterEmployees_GenderJobTitleAndDepartmentSpecified()
    {
        // Arrange
        UnitOfWork = new UnitOfWork(); 
        TekoDataFilter = new TekoDataFilter(); 
        var fio = ""; 
        var ageMin = ""; 
        var ageMax = ""; 
        var gender = Gender.Female.ToString(); 
        var jobTitle = JobTitle.DevelopmentLead.ToString(); 
        var department = Department.Administration.ToString(); 
        var filterOptions = ""; 

        // Act 
        var employees = UnitOfWork.GetEmployees().ToList(); 
        var referenceList = employees.Where(x => 
            x.Gender.ToString() == gender
            && x.JobTitle.ToString() == jobTitle
            && x.Department.ToString() == department).ToList(); 
        var filtered = TekoDataFilter.FilterEmployees(fio, ageMin, ageMax, gender, jobTitle, department, filterOptions, UnitOfWork.GetEmployees).ToList(); 
        var identical = CompareLists(referenceList, filtered);

        // Assert 
        Assert.True(filtered.Count == referenceList.Count); 
        Assert.True(identical); 
    }

    [Fact]
    public void FilterEmployees_AgeGenderJobTitleAndDepartmentSpecified()
    {
        // Arrange
        UnitOfWork = new UnitOfWork(); 
        TekoDataFilter = new TekoDataFilter(); 
        var fio = ""; 
        var ageMin = 34; 
        var ageMax = 55; 
        var gender = Gender.Male.ToString(); 
        var jobTitle = JobTitle.DevelopmentLead.ToString(); 
        var department = Department.Administration.ToString(); 
        var filterOptions = ""; 

        // Act 
        var employees = UnitOfWork.GetEmployees().ToList(); 
        var referenceList = employees.Where(x => 
            x.BirthDate <= System.DateTime.Now.AddYears(-ageMin)
            && x.BirthDate >= System.DateTime.Now.AddYears(-ageMax)
            && x.Gender.ToString() == gender
            && x.JobTitle.ToString() == jobTitle
            && x.Department.ToString() == department).ToList(); 
        var filtered = TekoDataFilter.FilterEmployees(fio, ageMin.ToString(), ageMax.ToString(), gender, jobTitle, department, filterOptions, UnitOfWork.GetEmployees).ToList(); 
        var identical = CompareLists(referenceList, filtered);

        // Assert 
        Assert.True(filtered.Count == referenceList.Count); 
        Assert.True(identical); 
    }
    #endregion  // FilterEmployees

    #region FilterVacations
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
    #endregion  // FilterVacations

    #region Private methods 
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
    #endregion  // Private methods 
}