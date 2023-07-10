using System.Collections.Generic; 
using TekoEmployeesMvc.Helpers; 

namespace TekoEmployeesMvc.Models;

public class UserPipe : AbstractPipe
{
    public UserPipe(System.Action<PipeResult> function) : base(function)
    {
    }
    public override void Handle(PipeResult result)
    {
        IUserGenerator generator = new UserGenerator(); 
        result.Users = generator.GenerateUsers(result.PipeParams.UserQty, base.GenerateDate); 
        _function(result); 
    }
}