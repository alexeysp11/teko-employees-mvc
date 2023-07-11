namespace TekoEmployeesMvc.Models;

public class Employee
{
    public string FIO { get; set; }
    public Gender Gender { get; set; }
    public JobTitle JobTitle { get; set; }
    public Department Department { get; set; }
    public System.DateTime BirthDate { get; set; }
}
