namespace TekoEmployeesMvc.Models;

public class Vacation
{
    public System.DateTime BeginDate { get; set; }
    public System.DateTime EndDate { get; set; }
    public Employee Employee { get; set; }
}