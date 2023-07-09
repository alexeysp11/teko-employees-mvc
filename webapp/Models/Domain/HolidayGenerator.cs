namespace TekoEmployeesMvc.Models;

public class HolidayGenerator : IHolidayGenerator
{
    public List<Holiday> GenerateHolidays(User user, int[] holidayIntervals, System.Func<System.DateTime, System.DateTime, System.DateTime> generateDate)
    {
        var result = new List<Holiday>(); 
        foreach (var interval in holidayIntervals)
        {
            // 
            System.DateTime start; 
            System.DateTime end; 
            do 
            {
                start = generateDate(new System.DateTime(2023, 1, 1), new System.DateTime(2023, 12, 31)); 
                end = start.AddDays(interval); 
            }
            while (end.Year != 2023); 
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
    public Holiday GenerateHoliday(User user, List<Holiday> holidays, System.Func<System.DateTime, System.DateTime, System.DateTime> generateDate)
    {
        // Get previously added holidays 
        // Get valid ranges of dates in the current year (those that are bigger than 7 or 14 days)
        System.DateTime start; 
        System.DateTime end; 
        if (holidays.Where(x => (x.BeginDate - x.EndDate).Days == 14).ToList().Count == 0)
        {
            start = generateDate(new System.DateTime(2023, 1, 1), new System.DateTime(2023, 12, 31)); 
            end = start.AddDays(14); 
        }
        else 
        {
            start = generateDate(new System.DateTime(2023, 1, 1), new System.DateTime(2023, 12, 31)); 
            end = start.AddDays(7); 
        }

        // 
        return new Holiday 
        {
            BeginDate = start, 
            EndDate = end, 
            User = user
        };
    }
}