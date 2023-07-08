using System.Linq; 
using System.Collections.Generic; 

namespace TekoEmployeesMvc.Models;

public class HolidayPipe : AbstractPipe
{
    public HolidayPipe(System.Action<PipeResult> function) : base(function)
    {
    }
    public List<Holiday> GenerateHolidays(List<User> users, int count)
    {
        var holidays = new List<Holiday>(); 
        foreach (var user in users)
        {
            var userHolidays = GenerateHolidays(user, count); 
            holidays.AddRange(userHolidays); 
        }
        return holidays; 
    }
    private List<Holiday> GenerateHolidays(User user, int count)
    {
        var holidays = new List<Holiday>(); 
        for (int i = 0; i < count; i++)
        {
            // Algorithm for adding holidays could be implemented in 2 different ways:
            // 1) 14 days first; 
            // 2) order doesn't matter.

            // 
            IHolidayGenerator generator = new HolidayGenerator(); 
            var holiday = generator.GenerateHoliday(user, holidays, GenerateDate); 
            holidays.Add(holiday); 
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
        result.Holidays = GenerateHolidays(result.Users, result.HolidayQty); 
        _function(result); 
    }
}