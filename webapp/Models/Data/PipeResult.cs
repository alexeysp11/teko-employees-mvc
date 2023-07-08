using System.Collections.Generic; 

namespace TekoEmployeesMvc.Models;

public class PipeResult
{
    public int UserQty { get; set; }
    public int HolidayQty { get; set; }
    public List<User> Users { get; set; }
    public List<Holiday> Holidays { get; set; }
    
    public PipeResult(int userQty, int holidayQty)
    {
        UserQty = userQty; 
        HolidayQty = holidayQty; 
        Users = new List<User>(); 
        Holidays = new List<Holiday>(); 
    }
}