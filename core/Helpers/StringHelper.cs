namespace TekoEmployeesMvc.Helpers;

public static class StringHelper
{
    #region TempData UID
    public readonly static string UsersUidStr = "usersUid"; 
    public readonly static string HolidaysUidStr = "holidaysUid"; 
    public readonly static string FilterInfoUsersStr = "filterInfoUsers"; 
    public readonly static string FilterInfoHolidaysStr = "filterInfoHolidays"; 
    public readonly static string UserInfoHolidaysStr = "userInfoHolidays"; 
    public readonly static string FilterOptionsHolidaysStr = "filterOptionsHolidays"; 
    public readonly static string FilterOptionsUsersStr = "filterOptionsUsers"; 
    #endregion  // TempData UID

    #region Filter options
    public readonly static string NoFiltersApplied = "No filters applied"; 
    public readonly static string FindOnlyIntersections = "Find only intersections"; 
    #endregion  // Filter options

    public static string GetFilterOptionsString(string fio, string ageFrom, string ageTo, string gender, string jobTitle)
    {
        return $"fio: '{fio}', age: '{ageFrom}' to '{ageTo}', gender: '{gender}', jobTitle: '{jobTitle}'";
    }
}