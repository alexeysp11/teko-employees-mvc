namespace TekoEmployeesMvc.Helpers;

public static class ConfigHelper
{
    public readonly static int DbSetCollectorInterval = 20000; 

    public readonly static int UserQty = 100; 
    public readonly static int[] HolidayIntervals = { 14, 7, 7 }; 
    public static int HolidayQty 
    {
        get 
        {
            return UserQty * HolidayIntervals.Length; 
        }
    } 

    public readonly static int UserMaxAge = 70; 
    public readonly static int UserMinAge = 18; 

    public readonly static int UserFioLength = 10; 
    public readonly static int UserFioWordsNumber = 3; 
}