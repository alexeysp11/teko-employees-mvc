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

    public static string GetFilterOptionsString(string fio, string ageFrom, string ageTo, string gender, string jobTitle, string department)
    {
        // If no filters applied 
        if (string.IsNullOrEmpty(fio) && string.IsNullOrEmpty(ageFrom) 
            && string.IsNullOrEmpty(ageTo) && string.IsNullOrEmpty(gender)
            && string.IsNullOrEmpty(jobTitle) && string.IsNullOrEmpty(department))
        {
            return NoFiltersApplied; 
        }

        // Create a string about a filter
        string result = string.Empty; 
        if (!string.IsNullOrEmpty(fio))
            result += "FIO: " + fio; 
        if (!string.IsNullOrEmpty(ageFrom) || !string.IsNullOrEmpty(ageTo))
        {
            if (!string.IsNullOrEmpty(result))
                result += ", "; 
            string fromString = (string.IsNullOrEmpty(ageFrom) ? "" : (string.IsNullOrEmpty(ageTo) ? "older than " : "from ") + ageFrom + " "); 
            string toString = (string.IsNullOrEmpty(ageTo) ? "" : (string.IsNullOrEmpty(ageFrom) ? "younger than " : "to ") + ageTo); 
            result += "age: " + fromString + toString; 
        }
        if (!string.IsNullOrEmpty(gender))
        {
            if (!string.IsNullOrEmpty(result))
                result += ", "; 
            result += "gender: " + gender; 
        }
        if (!string.IsNullOrEmpty(jobTitle))
        {
            if (!string.IsNullOrEmpty(result))
                result += ", "; 
            result += "job title: " + jobTitle; 
        }
        if (!string.IsNullOrEmpty(department))
        {
            if (!string.IsNullOrEmpty(result))
                result += ", "; 
            result += "department: " + department; 
        }
        return result;
    }
}