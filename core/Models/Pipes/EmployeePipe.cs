using System.Collections.Generic; 
using TekoEmployeesMvc.Helpers; 

namespace TekoEmployeesMvc.Models;

public class EmployeePipe : AbstractPipe
{
    public EmployeePipe(System.Action<PipeResult> function) : base(function)
    {
    }
    public override void Handle(PipeResult result)
    {
        IEmployeeGenerator generator = new EmployeeGenerator(); 
        result.Employees = generator.GenerateEmployees(result.PipeParams.EmployeeQty, base.GenerateDate); 
        _function(result); 
    }
}