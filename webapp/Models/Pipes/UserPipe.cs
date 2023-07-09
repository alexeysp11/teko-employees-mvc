using System.Collections.Generic; 
using TekoEmployeesMvc.Helpers; 

namespace TekoEmployeesMvc.Models;

public class UserPipe : AbstractPipe
{
    public UserPipe(System.Action<PipeResult> function) : base(function)
    {
    }
    public List<User> GenerateUsers(int count)
    {
        var users = new List<User>(); 
        for (int i = 0; i < count; i++)
        {
            var user = new User 
            {
                FIO = GenerateFIO(),
                Gender = GenerateGender(),
                JobTitle = GenerateJobTitle(),
                Department = GenerateDepartment(),
                BirthDate = GenerateBirthDate()
            };
            users.Add(user); 
        }
        return users; 
    }
    private string GenerateFIO()
    {
        var finalString = string.Empty; 
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        var stringChars = new char[ConfigHelper.UserFioLength];
        var random = new Random();
        for (int i = 0; i < ConfigHelper.UserFioWordsNumber; i++)
        {
            for (int j = 0; j < stringChars.Length; j++)
                stringChars[j] = chars[random.Next(chars.Length)];
            var tmpStr = new String(stringChars); 
            finalString += tmpStr.First().ToString().ToUpper() + tmpStr.Substring(1).ToLower() + " "; 
        }
        return finalString.Last() == ' ' ? finalString.Remove(finalString.Length - 1) : finalString; 
    }
    private Gender GenerateGender()
    {
        var length = System.Enum.GetNames(typeof(Gender)).Length; 
        return (Gender) new Random().Next(1, length + 1); 
    }
    private JobTitle GenerateJobTitle()
    {
        var length = System.Enum.GetNames(typeof(JobTitle)).Length; 
        return (JobTitle) new Random().Next(1, length + 1); 
    }
    private Department GenerateDepartment()
    {
        var length = System.Enum.GetNames(typeof(Department)).Length; 
        return (Department) new Random().Next(1, length + 1); 
    }
    private System.DateTime GenerateBirthDate()
    {
        System.DateTime start = new System.DateTime(System.DateTime.Now.Year - ConfigHelper.UserMaxAge, 1, 1); 
        System.DateTime end = new System.DateTime(System.DateTime.Now.Year - ConfigHelper.UserMinAge, 1, 1); 
        return base.GenerateDate(start, end); 
    }
    public override void Handle(PipeResult result)
    {
        result.Users = GenerateUsers(result.PipeParams.UserQty); 
        _function(result); 
    }
}