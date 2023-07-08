namespace TekoEmployeesMvc.Models;

public class HolidayGenerator14First : IHolidayGenerator
{
    public Holiday GenerateHoliday(User user, List<Holiday> holidays, System.Func<System.DateTime, System.DateTime, System.DateTime> generateDate)
    {
        return new Holiday 
        {
            BeginDate = new System.DateTime(2023, 1, 1), 
            EndDate = new System.DateTime(2023, 1, 14), 
            User = user
        };
    }
}