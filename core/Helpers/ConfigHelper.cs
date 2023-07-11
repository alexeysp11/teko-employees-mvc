namespace TekoEmployeesMvc.Helpers;

public static class ConfigHelper
{
    public readonly static int DbSetCollectorInterval = 20000; 

    public readonly static int EmployeeQty = 100; 
    public readonly static int[] VacationIntervals = { 14, 7, 7 }; 
    public static int VacationQty 
    {
        get 
        {
            return EmployeeQty * VacationIntervals.Length; 
        }
    } 

    public readonly static int EmployeeMaxAge = 70; 
    public readonly static int EmployeeMinAge = 18; 

    public readonly static int EmployeeFioLength = 10; 
    public readonly static int EmployeeFioWordsNumber = 3; 
}