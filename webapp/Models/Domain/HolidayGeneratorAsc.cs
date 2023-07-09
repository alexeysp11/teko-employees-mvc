namespace TekoEmployeesMvc.Models;

public class HolidayGeneratorAsc : IHolidayGenerator
{
    public List<Holiday> GenerateHolidays(
        User user, 
        int[] holidayIntervals, 
        System.Func<System.DateTime, System.DateTime, System.DateTime> generateDate)
    {
        return new List<Holiday>(); 
    }
}