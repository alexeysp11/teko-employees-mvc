namespace TekoEmployeesMvc.Models;

public interface IHolidayGenerator
{
    List<Holiday> GenerateHolidays(
        User user, 
        int[] holidayIntervals, 
        System.Func<System.DateTime, System.DateTime, System.DateTime> generateDate); 
}