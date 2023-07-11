using System.Collections.Generic; 

namespace TekoEmployeesMvc.Models;

public class PipeParams
{
    public int EmployeeQty { get; }
    public int[] VacationIntervals { get; }
    
    public PipeParams(int employeeQty, int[] vacationIntervals)
    {
        EmployeeQty = employeeQty; 
        VacationIntervals = vacationIntervals; 
    }
}