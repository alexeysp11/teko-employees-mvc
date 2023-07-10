using System.Collections.Generic; 

namespace TekoEmployeesMvc.Models;

public class PipeResult
{
    public PipeParams PipeParams { get; }
    public List<User> Users { get; set; }
    public List<Holiday> Holidays { get; set; }
    
    public PipeResult(PipeParams pipeParams)
    {
        PipeParams = pipeParams; 
        Users = new List<User>(); 
        Holidays = new List<Holiday>(); 
    }
}