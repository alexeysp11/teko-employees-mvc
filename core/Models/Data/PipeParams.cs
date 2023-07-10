using System.Collections.Generic; 

namespace TekoEmployeesMvc.Models;

public class PipeParams
{
    public int UserQty { get; }
    public int[] HolidayIntervals { get; }
    
    public PipeParams(int userQty, int[] holidayIntervals)
    {
        UserQty = userQty; 
        HolidayIntervals = holidayIntervals; 
    }
}