using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using TekoEmployeesMvc.Models;

namespace TekoEmployeesMvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork; 
    private readonly string UsersUidStr = "usersUid"; 
    private readonly string HolidaysUidStr = "holidaysUid"; 
    private readonly string FilterInfoHolidaysStr = "filterInfoHolidays"; 
    private readonly string FilterInfoUsersStr = "filterInfoUsers"; 
    private readonly string UserInfoHolidaysStr = "userInfoHolidays"; 

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
        var uidObj = TempData[UsersUidStr];
        if (uidObj != null && !string.IsNullOrEmpty(uidObj.ToString()))
        {
            var usersFiltered = _unitOfWork.GetFilteredUsers(uidObj.ToString()).ToList(); 
            if (usersFiltered.Count != 0)
            {
                uidObj = string.Empty;
                return View(usersFiltered);
            }
        }
        TempData[FilterInfoUsersStr] = "No filters applied"; 
        var users = _unitOfWork.GetUsers(); 
        return View(users);
    }

    public IActionResult Holidays()
    {
        var uidObj = TempData[HolidaysUidStr];
        if (uidObj != null && !string.IsNullOrEmpty(uidObj.ToString()))
        {
            var holidaysFiltered = _unitOfWork.GetFilteredHolidays(uidObj.ToString()).ToList(); 
            if (holidaysFiltered.Count != 0)
            {
                uidObj = string.Empty;
                return View(holidaysFiltered);
            }
        }
        TempData[FilterInfoHolidaysStr] = "No filters applied"; 
        var holdays = _unitOfWork.GetHolidays(); 
        return View(holdays);
    }

    [HttpPost("[action]")]
    [Route("/Home")]
    public IActionResult FilterUsers(string fio, string age, string gender, string jobTitle)
    {
        var users = _unitOfWork.GetUsers(x => string.IsNullOrEmpty(fio) || x.FIO.Contains(fio)); 
        string uid = _unitOfWork.InsertFilteredUsers(users); 
        TempData[UsersUidStr] = uid; 
        TempData[FilterInfoUsersStr] = $"fio: '{fio}', gender: '{gender}', jobTitle: '{jobTitle}'"; 
        
        return RedirectToAction("Users");
    }

    [HttpPost("[action]")]
    [Route("/Home")]
    public IActionResult FilterHolidays(string fio, string age, string gender, string jobTitle, 
        string currentFio, string currentAge, string currentGender, string currentJobTitle, 
        string filterOptions)
    {
        // System.Console.WriteLine($"fio: '{fio}', gender: '{gender}', jobTitle: '{jobTitle}'"); 

        // Get filtered data 
        var holdays = _unitOfWork.GetHolidays(x => string.IsNullOrEmpty(fio) || x.User.FIO.Contains(fio)); 
        var currentHoldays = _unitOfWork.GetHolidays(x => string.IsNullOrEmpty(currentFio) || x.User.FIO.Contains(currentFio)); 
        holdays.AddRange(currentHoldays); 

        // Find intersections if necessary 
        if (!string.IsNullOrEmpty(filterOptions) && filterOptions == "Find only intersections")
        {
            System.Console.WriteLine("findIntersections"); 
        }
        
        // Insert filtered data and get UID 
        string uid = _unitOfWork.InsertFilteredHolidays(holdays); 

        // Store UID and  in views 
        TempData[HolidaysUidStr] = uid; 

        // Store info about filtering 
        TempData[FilterInfoHolidaysStr] = $"fio: '{fio}', gender: '{gender}', jobTitle: '{jobTitle}'"; 
        TempData[UserInfoHolidaysStr] = $"currentFio: '{currentFio}', currentGender: '{currentGender}', currentJobTitle: '{currentJobTitle}'"; 
        
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
