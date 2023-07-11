using System.Collections.Generic; 

namespace TekoEmployeesMvc.Models;

public class PipeResult
{
    public PipeParams PipeParams { get; }
    public List<Employee> Employees { get; set; }
    public List<Vacation> Vacations { get; set; }
    
    public PipeResult(PipeParams pipeParams)
    {
        PipeParams = pipeParams; 
        Employees = new List<Employee>(); 
        Vacations = new List<Vacation>(); 
    }
}