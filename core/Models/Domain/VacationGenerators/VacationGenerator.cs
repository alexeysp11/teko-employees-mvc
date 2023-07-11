namespace TekoEmployeesMvc.Models;

public class VacationGenerator : IVacationGenerator
{
    public List<Vacation> GenerateVacations(
        Employee employee, 
        int[] vacationIntervals, 
        System.Func<System.DateTime, System.DateTime, System.DateTime> generateDate)
    {
        var result = new List<Vacation>(); 
        var year = System.DateTime.Now.Year; 
        foreach (var interval in vacationIntervals)
        {
            System.DateTime start; 
            System.DateTime end; 
            do 
            {
                start = generateDate(new System.DateTime(year, 1, 1), new System.DateTime(year, 12, 31)); 
                end = start.AddDays(interval); 
            }
            while (end.Year != year); 
            var vacation = new Vacation 
            {
                BeginDate = start, 
                EndDate = end, 
                Employee = employee
            };
            result.Add(vacation); 
        }
        return result; 
    }
}