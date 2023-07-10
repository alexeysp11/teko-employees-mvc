namespace TekoEmployeesMvc.Models;

public class HolidayGenerator : IHolidayGenerator
{
    public List<Holiday> GenerateHolidays(
        User user, 
        int[] holidayIntervals, 
        System.Func<System.DateTime, System.DateTime, System.DateTime> generateDate)
    {
        var result = new List<Holiday>(); 
        var year = System.DateTime.Now.Year; 
        foreach (var interval in holidayIntervals)
        {
            System.DateTime start; 
            System.DateTime end; 
            do 
            {
                start = generateDate(new System.DateTime(year, 1, 1), new System.DateTime(year, 12, 31)); 
                end = start.AddDays(interval); 
            }
            while (end.Year != year); 
            var holiday = new Holiday 
            {
                BeginDate = start, 
                EndDate = end, 
                User = user
            };
            result.Add(holiday); 
        }
        return result; 
    }
}