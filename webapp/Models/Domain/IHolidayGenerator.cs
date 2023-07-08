namespace TekoEmployeesMvc.Models;

public interface IHolidayGenerator
{
    Holiday GenerateHoliday(User user, List<Holiday> holidays, System.Func<System.DateTime, System.DateTime, System.DateTime> generateDate); 
}