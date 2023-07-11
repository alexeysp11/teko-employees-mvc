using System.Collections.Generic; 

namespace TekoEmployeesMvc.Models;

public class FilterEmployeesParams
{
    public int EmployeeQty { get; set; }
    public int[] VacationIntervals { get; set; }
}