using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TekoEmployeesMvc.Helpers; 
using TekoEmployeesMvc.Models;

namespace TekoEmployeesMvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork; 

    public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
    {
        _logger = logger;
        _unitOfWork = unitOfWork; 
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Users()
    {
        var uidObj = TempData[StringHelper.UsersUidStr];
        if (uidObj != null && !string.IsNullOrEmpty(uidObj.ToString()))
        {
            var usersFiltered = _unitOfWork.GetFilteredUsers(uidObj.ToString()).ToList(); 
            if (usersFiltered.Count != 0)
            {
                uidObj = string.Empty;
                return View(usersFiltered);
            }
        }
        TempData[StringHelper.FilterInfoUsersStr] = StringHelper.NoFiltersApplied; 
        TempData[StringHelper.FilterOptionsUsersStr] = StringHelper.NoFiltersApplied; 
        var users = _unitOfWork.GetUsers(); 
        return View(users);
    }

    public IActionResult Holidays()
    {
        var uidObj = TempData[StringHelper.HolidaysUidStr];
        if (uidObj != null && !string.IsNullOrEmpty(uidObj.ToString()))
        {
            var holidaysFiltered = _unitOfWork.GetFilteredHolidays(uidObj.ToString()).ToList(); 
            if (holidaysFiltered.Count != 0)
            {
                uidObj = string.Empty;
                return View(holidaysFiltered);
            }
        }
        TempData[StringHelper.FilterInfoHolidaysStr] = StringHelper.NoFiltersApplied; 
        TempData[StringHelper.UserInfoHolidaysStr] = StringHelper.NoFiltersApplied; 
        TempData[StringHelper.FilterOptionsHolidaysStr] = StringHelper.NoFiltersApplied; 
        var holdays = _unitOfWork.GetHolidays(); 
        return View(holdays);
    }

    [HttpPost("[action]")]
    [Route("/Home")]
    public IActionResult FilterUsers(string fio, string ageFrom, string ageTo, string gender, string jobTitle, string filterOptions)
    {
        var users = _unitOfWork.GetUsers(x => string.IsNullOrEmpty(fio) || x.FIO.Contains(fio)); 
        string uid = _unitOfWork.InsertFilteredUsers(users); 
        TempData[StringHelper.UsersUidStr] = uid; 
        TempData[StringHelper.FilterInfoUsersStr] = StringHelper.GetFilterOptionsString(fio, ageFrom, ageTo, gender, jobTitle); 
        TempData[StringHelper.FilterOptionsUsersStr] = filterOptions; 
        
        return RedirectToAction("Users");
    }

    [HttpPost("[action]")]
    [Route("/Home")]
    public IActionResult FilterHolidays(string fio, string ageFrom, string ageTo, string gender, string jobTitle, 
        string currentFio, string currentAgeFrom, string currentAgeTo, string currentGender, string currentJobTitle, 
        string filterOptions)
    {
        // Get filtered data 
        var holdays = _unitOfWork.GetHolidays(x => string.IsNullOrEmpty(fio) || x.User.FIO.Contains(fio)); 
        var currentHoldays = _unitOfWork.GetHolidays(x => string.IsNullOrEmpty(currentFio) || x.User.FIO.Contains(currentFio)); 
        holdays.AddRange(currentHoldays); 

        // Find intersections if necessary 
        if (!string.IsNullOrEmpty(filterOptions) && filterOptions == StringHelper.FindOnlyIntersections)
        {
            System.Console.WriteLine("Filter: " + filterOptions); 
        }
        
        // Insert filtered data and get UID 
        string uid = _unitOfWork.InsertFilteredHolidays(holdays); 

        // Store UID and  in views 
        TempData[StringHelper.HolidaysUidStr] = uid; 

        // Store info about filtering 
        TempData[StringHelper.FilterInfoHolidaysStr] = StringHelper.GetFilterOptionsString(fio, ageFrom, ageTo, gender, jobTitle);  
        TempData[StringHelper.UserInfoHolidaysStr] = StringHelper.GetFilterOptionsString(currentFio, currentAgeFrom, currentAgeTo, currentGender, currentJobTitle);  
        TempData[StringHelper.FilterOptionsHolidaysStr] = filterOptions; 

        return RedirectToAction("Holidays");
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
