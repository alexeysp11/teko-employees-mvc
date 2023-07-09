using System.Collections.Generic; 

namespace TekoEmployeesMvc.Models;

public class PipeResult
{
    public int UserQty { get; set; }
    public int[] HolidayIntervals { get; set; }
    public List<User> Users { get; set; }
    public List<Holiday> Holidays { get; set; }
    
    public PipeResult(int userQty, int[] holidayIntervals)
    {
        UserQty = userQty; 
        HolidayIntervals = holidayIntervals; 
        Users = new List<User>(); 
        Holidays = new List<Holiday>(); 
    }
}