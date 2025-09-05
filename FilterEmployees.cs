using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Encodings.Web;

public static string FilterEmployees(IEnumerable<(string Name, int Age, string Department, decimal Salary, DateTime HireDate)> employees)
{
    var filteredEmployees = employees
        .Where(e => e.Age >= 25 && e.Age <= 40 &&
                   (e.Department == "IT" || e.Department == "Finance") &&
                   e.Salary >= 5000 && e.Salary <= 9000 &&
                   e.HireDate > new DateTime(2017, 12, 31))
        .ToList();

    var sortedNames = filteredEmployees
        .OrderByDescending(e => e.Name.Length)
        .ThenBy(e => e.Name)
        .Select(e => e.Name)
        .ToArray();

    var result = new
    {
        Names = sortedNames,
        TotalSalary = filteredEmployees.Sum(e => e.Salary),
        AverageSalary = filteredEmployees.Any() ? Math.Round(filteredEmployees.Average(e => e.Salary), 2) : 0,
        MinSalary = filteredEmployees.Any() ? filteredEmployees.Min(e => e.Salary) : 0,
        MaxSalary = filteredEmployees.Any() ? filteredEmployees.Max(e => e.Salary) : 0,
        Count = filteredEmployees.Count
    };

    var options = new JsonSerializerOptions
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    return JsonSerializer.Serialize(result, options);
}