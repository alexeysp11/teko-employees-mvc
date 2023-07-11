using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TekoEmployeesMvc.Helpers; 
using TekoEmployeesMvc.Models;

namespace TekoEmployeesMvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork; 
    private readonly ITekoDataFilter _tekoFilter; 

    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork, ITekoDataFilter tekoFilter)
    {
        _logger = logger;
        _unitOfWork = unitOfWork; 
        _tekoFilter = tekoFilter; 
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Employees()
    {
        var uidObj = TempData[StringHelper.EmployeesUidStr];
        if (uidObj != null && !string.IsNullOrEmpty(uidObj.ToString()))
        {
            var employeesFiltered = _unitOfWork.GetFilteredEmployees(uidObj.ToString()).ToList(); 
            uidObj = string.Empty;
            return View(employeesFiltered);
        }
        TempData[StringHelper.FilterInfoEmployeesStr] = StringHelper.NoFiltersApplied; 
        TempData[StringHelper.FilterOptionsEmployeesStr] = StringHelper.NoFiltersApplied; 
        var employees = _unitOfWork.GetEmployees(); 
        return View(employees);
    }

    public IActionResult Vacations()
    {
        // Restore previously filtered elements 
        var uidObj = TempData[StringHelper.VacationsUidStr];
        if (uidObj != null && !string.IsNullOrEmpty(uidObj.ToString()))
        {
            var vacationsFiltered = _unitOfWork.GetFilteredVacations(uidObj.ToString()).ToList(); 
            if (vacationsFiltered.Count != 0)
            {
                uidObj = string.Empty;
                return View(vacationsFiltered);
            }
        }

        // Set info about filters 
        TempData[StringHelper.FilterInfoVacationsStr] = StringHelper.NoFiltersApplied; 
        TempData[StringHelper.EmployeeInfoVacationsStr] = StringHelper.NoFiltersApplied; 
        TempData[StringHelper.FilterOptionsVacationsStr] = StringHelper.NoFiltersApplied; 

        // Get all elements 
        var holdays = _unitOfWork.GetVacations(); 

        return View(holdays);
    }

    [HttpPost("[action]")]
    [Route("/Home")]
    public IActionResult FilterEmployees(string fio, string ageFrom, string ageTo, string gender, string jobTitle, string department, 
        string filterOptions)
    {
        // Apply filters 
        // Use a class FilterEmployeeParams to avoid duplicating all the variables 
        var employees = _tekoFilter.FilterEmployees(fio, ageFrom, ageTo, gender, jobTitle, department, filterOptions, _unitOfWork.GetEmployees); 
        
        // Save filtered employees 
        string uid = _unitOfWork.InsertFilteredEmployees(employees); 

        // Save info about applied filters 
        TempData[StringHelper.EmployeesUidStr] = uid; 
        TempData[StringHelper.FilterInfoEmployeesStr] = StringHelper.GetFilterOptionsString(fio, ageFrom, ageTo, gender, jobTitle, department); 
        TempData[StringHelper.FilterOptionsEmployeesStr] = filterOptions; 
        
        return RedirectToAction("Employees");
    }

    [HttpPost("[action]")]
    [Route("/Home")]
    public IActionResult FilterVacations(string fio, string ageFrom, string ageTo, string gender, string jobTitle, string department, 
        string currentFio, string currentAgeFrom, string currentAgeTo, string currentGender, string currentJobTitle, string currentDepartment, 
        string filterOptions)
    {
        // Get filtered data 
        var holdays = _unitOfWork.GetVacations(x => string.IsNullOrEmpty(fio) || x.Employee.FIO.Contains(fio)); 
        var currentHoldays = _unitOfWork.GetVacations(x => string.IsNullOrEmpty(currentFio) || x.Employee.FIO.Contains(currentFio)); 
        holdays.AddRange(currentHoldays); 

        // Find intersections if necessary 
        if (!string.IsNullOrEmpty(filterOptions) && filterOptions == StringHelper.FindOnlyIntersections)
        {
            System.Console.WriteLine("Filter: " + filterOptions); 
        }
        
        // Insert filtered data and get UID 
        string uid = _unitOfWork.InsertFilteredVacations(holdays); 

        // Store UID and  in views 
        TempData[StringHelper.VacationsUidStr] = uid; 

        // Store info about filtering 
        TempData[StringHelper.FilterInfoVacationsStr] = StringHelper.GetFilterOptionsString(fio, ageFrom, ageTo, gender, jobTitle, department);  
        TempData[StringHelper.EmployeeInfoVacationsStr] = StringHelper.GetFilterOptionsString(currentFio, currentAgeFrom, currentAgeTo, currentGender, currentJobTitle, currentDepartment);  
        TempData[StringHelper.FilterOptionsVacationsStr] = filterOptions; 

        return RedirectToAction("Vacations");
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
