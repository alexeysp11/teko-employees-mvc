using System.Collections.Generic; 

namespace TekoEmployeesMvc.Models;

public abstract class AbstractPipe
{
    protected System.Action<PipeResult> _function; 

    public AbstractPipe(System.Action<PipeResult> function)
    {
        _function = function; 
    }

    protected System.DateTime GenerateDate(System.DateTime start, System.DateTime end)
    {
        var range = (end - start).Days;
        return start.AddDays(new Random().Next(range)); 
    }

    public abstract void Handle(PipeResult result); 
}