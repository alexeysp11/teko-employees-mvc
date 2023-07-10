using Xunit;
using TekoEmployeesMvc.Models;
using TekoEmployeesMvc.Helpers;

namespace Tests.TekoEmployeesMvc;

public class ConfigHelperTest
{
    [Fact]
    public void HolidayQty()
    {
        Assert.True(ConfigHelper.UserQty * ConfigHelper.HolidayIntervals.Length == ConfigHelper.HolidayQty); 
    }
}