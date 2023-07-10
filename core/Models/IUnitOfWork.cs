using System.Collections.Generic; 
using System.Linq.Expressions; 

namespace TekoEmployeesMvc.Models;

public interface IUnitOfWork
{
    FilteredRepository<User> UserRepositoryFiltered { get; }
    FilteredRepository<Holiday> HolidayRepositoryFiltered { get; }

    // void Generate();
    
    void InsertHoliday(string fio, System.DateTime start, System.DateTime end); 

    void FindHolidaysByFIO(string fio); 
    List<User> GetUsers(Expression<Func<User, bool>> filter = null); 
    List<Holiday> GetHolidays(Expression<Func<Holiday, bool>> filter = null); 

    string InsertFilteredUsers(IEnumerable<User> entities);
    string InsertFilteredHolidays(IEnumerable<Holiday> entities); 
    IEnumerable<User> GetFilteredUsers(string uid); 
    IEnumerable<Holiday> GetFilteredHolidays(string uid); 
}