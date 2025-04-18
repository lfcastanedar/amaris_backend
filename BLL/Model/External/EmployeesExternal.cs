﻿using Newtonsoft.Json;

namespace BLL.Model.External;

public class EmployeesExternal
{
    public int Id { get; set; }
    
    [JsonProperty("employee_name")]
    public string EmployeeName { get; set; }
    
    [JsonProperty("employee_salary")]
    public int EmployeeSalary { get; set; }
    
    [JsonProperty("employee_age")]
    public int EmployeeAge { get; set; }
    
    [JsonProperty("profile_image")]
    public string ProfileImage { get; set; } = string.Empty;

}