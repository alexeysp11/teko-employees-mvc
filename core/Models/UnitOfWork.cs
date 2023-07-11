using System.Collections.Generic; 
using System.Linq.Expressions;
using TekoEmployeesMvc.Helpers;

namespace TekoEmployeesMvc.Models;

public class UnitOfWork : IUnitOfWork
{
    private GenericRepository<Employee> employeeRepository;
    private GenericRepository<Vacation> vacationRepository;
    private FilteredRepository<Employee> employeeRepositoryFiltered;
    private FilteredRepository<Vacation> vacationRepositoryFiltered;

    public GenericRepository<Employee> EmployeeRepository
    {
        get
        {
            if (this.employeeRepository == null)
            {
                this.employeeRepository = new GenericRepository<Employee>();
            }
            return employeeRepository;
        }
    }
    public GenericRepository<Vacation> VacationRepository
    {
        get
        {
            if (this.vacationRepository == null)
            {
                this.vacationRepository = new GenericRepository<Vacation>();
            }
            return vacationRepository;
        }
    }
    public FilteredRepository<Employee> EmployeeRepositoryFiltered
    {
        get
        {
            if (this.employeeRepositoryFiltered == null)
            {
                this.employeeRepositoryFiltered = new FilteredRepository<Employee>();
            }
            return employeeRepositoryFiltered;
        }
    }
    public FilteredRepository<Vacation> VacationRepositoryFiltered
    {
        get
        {
            if (this.vacationRepositoryFiltered == null)
            {
                this.vacationRepositoryFiltered = new FilteredRepository<Vacation>();
            }
            return vacationRepositoryFiltered;
        }
    }

    public UnitOfWork()
    {
        var pipeParams = new PipeParams(ConfigHelper.EmployeeQty, ConfigHelper.VacationIntervals);
        var result = new PipeResult(pipeParams); 
        
        var generatingPipe = new PipeBuilder(InsertIntoRepository)
            .AddGenerating(typeof(EmployeePipe))
            .AddGenerating(typeof(VacationPipe))
            .Build(); 
        generatingPipe(result); 
    }
    public void FindVacationsByFIO(string fio)
    {
        // Find employee 
        var employees = EmployeeRepository.Get(filter: x => x.FIO == fio).ToList(); 
        if (employees.Count == 0) 
            return; 
        
        // Find vacations for the specified employee 
        var vacations = VacationRepository.Get(filter: x => x.Employee.FIO == fio); 
        foreach (var vacation in vacations)
        {
            System.Console.WriteLine($"FIO: {fio}, BeginDate: {vacation.BeginDate}, EndDate: {vacation.EndDate}"); 
        }
    }
    public List<Employee> GetEmployees(Expression<Func<Employee, bool>> filter = null)
    {
        return EmployeeRepository.Get(filter: filter).ToList(); 
    }
    public List<Vacation> GetVacations(Expression<Func<Vacation, bool>> filter = null)
    {
        return VacationRepository.Get(filter: filter).ToList(); 
    }
    public void InsertVacation(string fio, System.DateTime begin, System.DateTime end)
    {
        System.Console.WriteLine("Insert the vacation for the specified employee");

        // Find employee 
        var employees = EmployeeRepository.Get(filter: x => x.FIO == fio).ToList(); 
        if (employees.Count == 0) 
            return; 
        
        // Check if the vacations overlap 
        var vacations = VacationRepository
            .Get(filter: x => x.Employee.FIO == fio
                            && (
                                (x.BeginDate <= begin && x.EndDate > begin) 
                                || (x.BeginDate <= end && x.EndDate > end)
                            )).ToList(); 
        if (vacations.Count == 0)
        {
            VacationRepository.Insert(
                new Vacation
                {
                    BeginDate = begin, 
                    EndDate = end,
                    Employee = employees.First()
                });
        }
    }
    public string InsertFilteredEmployees(IEnumerable<Employee> entities)
    {
        return EmployeeRepositoryFiltered.InsertFiltered(entities); 
    }
    public string InsertFilteredVacations(IEnumerable<Vacation> entities)
    {
        return VacationRepositoryFiltered.InsertFiltered(entities); 
    }
    public IEnumerable<Employee> GetFilteredEmployees(string uid)
    {
        return EmployeeRepositoryFiltered.GetFiltered(uid); 
    }
    public IEnumerable<Vacation> GetFilteredVacations(string uid)
    {
        return VacationRepositoryFiltered.GetFiltered(uid); 
    }
    private void InsertIntoRepository(PipeResult result)
    {
        System.Console.WriteLine("data added into the repository"); 
        foreach (var employee in result.Employees)
            EmployeeRepository.Insert(employee); 
        foreach (var vacation in result.Vacations)
            VacationRepository.Insert(vacation);
    }
}
