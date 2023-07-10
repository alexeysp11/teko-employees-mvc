using System.Linq; 
using System.Collections.Generic; 

namespace TekoEmployeesMvc.Models;

public class HolidayPipe : AbstractPipe
{
    public HolidayPipe(System.Action<PipeResult> function) : base(function)
    {
    }
    private List<Holiday> GenerateHolidays(List<User> users, int[] holidayIntervals)
    {
        var holidays = new List<Holiday>(); 
        foreach (var user in users)
        {
            IHolidayGenerator generator = new HolidayGenerator(); 
            var userHolidays = generator.GenerateHolidays(user, holidayIntervals, GenerateDate); 
            holidays.AddRange(userHolidays); 
        }
        return holidays; 
    }
    public static void AddHoliday(PipeResult result, string fio, System.DateTime begin, System.DateTime end)
    {
        // Get available slots for the user 
        var slots = result.Holidays
            .Where(x => x.User.FIO == fio 
                        && x.BeginDate <= begin 
                        && x.EndDate > begin 
                        && x.BeginDate < end 
                        && x.EndDate >= end).ToList(); 
        if (slots.Count == 0)
            return; 

        // Generate for the user 
        var user = result.Users.FirstOrDefault(x => x.FIO == fio);
        var holiday = new Holiday 
        {
            BeginDate = begin, 
            EndDate = end, 
            User = user
        };
        result.Holidays.Add(holiday); 
    }
    public override void Handle(PipeResult result)
    {
        result.Holidays = GenerateHolidays(result.Users, result.PipeParams.HolidayIntervals); 
        _function(result); 
    }
}