using Xunit;
using TekoEmployeesMvc.Models;
using TekoEmployeesMvc.Helpers;

namespace Tests.TekoEmployeesMvc;

public class ConfigHelperTest
{
    [Fact]
    public void VacationQty()
    {
        Assert.True(ConfigHelper.EmployeeQty * ConfigHelper.VacationIntervals.Length == ConfigHelper.VacationQty); 
    }
}