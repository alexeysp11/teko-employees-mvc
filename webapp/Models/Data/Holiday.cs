namespace TekoEmployeesMvc.Models;

public class Holiday
{
    public System.DateTime BeginDate { get; set; }
    public System.DateTime EndDate { get; set; }
    public User User { get; set; }
}