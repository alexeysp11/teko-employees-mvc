﻿namespace TekoEmployeesMvc.Models;

public class User
{
    public string FIO { get; set; }
    public Gender Gender { get; set; }
    public JobTitle JobTitle { get; set; }
    public System.DateTime BirthDate { get; set; }
}
