using System.Collections.Generic; 
using System.Linq.Expressions;

namespace TekoEmployeesMvc.Models;

public class UnitOfWork : IUnitOfWork
{
    // Implement one of the following options: 
    // 1) using DI container; 
    // 2) using pipe. 

    // TODO: 
    // 1) Finish GenerateHolidays(): implement all the algorithms for creating a list of holidays; 
    // 2) Create a pipe of functions. 
    // 3) Implement storing data in database. 
    // 4) Test the class.

    private System.Action<PipeResult> GeneratingPipe; 
    
    private GenericRepository<User> userRepository;
    private GenericRepository<Holiday> holidayRepository;
    private FilteredRepository<User> userRepositoryFiltered;
    private FilteredRepository<Holiday> holidayRepositoryFiltered;

    public GenericRepository<User> UserRepository
    {
        get
        {
            if (this.userRepository == null)
            {
                this.userRepository = new GenericRepository<User>();
            }
            return userRepository;
        }
    }
    public GenericRepository<Holiday> HolidayRepository
    {
        get
        {
            if (this.holidayRepository == null)
            {
                this.holidayRepository = new GenericRepository<Holiday>();
            }
            return holidayRepository;
        }
    }
    public FilteredRepository<User> UserRepositoryFiltered
    {
        get
        {
            if (this.userRepositoryFiltered == null)
            {
                this.userRepositoryFiltered = new FilteredRepository<User>();
            }
            return userRepositoryFiltered;
        }
    }
    public FilteredRepository<Holiday> HolidayRepositoryFiltered
    {
        get
        {
            if (this.holidayRepositoryFiltered == null)
            {
                this.holidayRepositoryFiltered = new FilteredRepository<Holiday>();
            }
            return holidayRepositoryFiltered;
        }
    }

    public UnitOfWork()
    {
        int userQty = 100; 
        int[] holidayIntervals = { 14, 7, 7 }; 
        Generate(userQty, holidayIntervals); 
    }

    public void Generate(int userQty, int[] holidayIntervals)
    {
        var result = new PipeResult(userQty, holidayIntervals); 
        GeneratingPipe = new PipeBuilder(InsertIntoRepository)
            .AddGenerating(typeof(UserPipe))
            .AddGenerating(typeof(HolidayPipe))
            .Build(); 
        GeneratingPipe(result); 
    }
    public void FindHolidaysByFIO(string fio)
    {
        // Find user 
        var users = UserRepository.Get(filter: x => x.FIO == fio).ToList(); 
        if (users.Count == 0) 
            return; 
        
        // Find holidays for the specified user 
        var holidays = HolidayRepository.Get(filter: x => x.User.FIO == fio); 
        foreach (var holiday in holidays)
        {
            System.Console.WriteLine($"FIO: {fio}, BeginDate: {holiday.BeginDate}, EndDate: {holiday.EndDate}"); 
        }
    }
    public List<User> GetUsers(Expression<Func<User, bool>> filter = null)
    {
        return UserRepository.Get(filter: filter).ToList(); 
    }
    public List<Holiday> GetHolidays(Expression<Func<Holiday, bool>> filter = null)
    {
        return HolidayRepository.Get(filter: filter).ToList(); 
    }
    public void InsertHoliday(string fio, System.DateTime begin, System.DateTime end)
    {
        System.Console.WriteLine("Insert the holiday for the specified user");

        // Find user 
        var users = UserRepository.Get(filter: x => x.FIO == fio).ToList(); 
        if (users.Count == 0) 
            return; 
        
        // Check if the holidays overlap 
        var holidays = HolidayRepository
            .Get(filter: x => x.User.FIO == fio
                            && (
                                (x.BeginDate <= begin && x.EndDate > begin) 
                                || (x.BeginDate <= end && x.EndDate > end)
                            )).ToList(); 
        if (holidays.Count == 0)
        {
            HolidayRepository.Insert(
                new Holiday
                {
                    BeginDate = begin, 
                    EndDate = end,
                    User = users.First()
                });
        }
    }
    public string InsertFilteredUsers(IEnumerable<User> entities)
    {
        return UserRepositoryFiltered.InsertFiltered(entities); 
    }
    public string InsertFilteredHolidays(IEnumerable<Holiday> entities)
    {
        return HolidayRepositoryFiltered.InsertFiltered(entities); 
    }
    public IEnumerable<User> GetFilteredUsers(string uid)
    {
        return UserRepositoryFiltered.GetFiltered(uid); 
    }
    public IEnumerable<Holiday> GetFilteredHolidays(string uid)
    {
        return HolidayRepositoryFiltered.GetFiltered(uid); 
    }
    private void InsertIntoRepository(PipeResult result)
    {
        System.Console.WriteLine("data added into the repository"); 
        foreach (var user in result.Users)
            UserRepository.Insert(user); 
        foreach (var holiday in result.Holidays)
            HolidayRepository.Insert(holiday);
    }
}
