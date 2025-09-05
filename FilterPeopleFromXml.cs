using System;
using System.Linq;
using System.Xml.Linq;
using System.Text.Json;
using System.Text.Encodings.Web;

public static string FilterPeopleFromXml(string xmlData)
{
    var doc = XDocument.Parse(xmlData);
    var filteredPeople = doc.Descendants("Person")
        .Select(p => new
        {
            Name = (string)p.Element("Name")!,
            Age = (int)p.Element("Age")!,
            Department = (string)p.Element("Department")!,
            Salary = (decimal)p.Element("Salary")!,
            HireDate = DateTime.Parse((string)p.Element("HireDate")!)
        })
        .Where(p => p.Age > 30 &&
                    p.Department == "IT" &&
                    p.Salary > 5000 &&
                    p.HireDate < new DateTime(2019, 1, 1))
        .ToList();

    var result = new
    {
        Names = filteredPeople.Select(p => p.Name).OrderBy(n => n).ToArray(),
        TotalSalary = filteredPeople.Sum(p => p.Salary),
        AverageSalary = filteredPeople.Any() ? Math.Round(filteredPeople.Average(p => p.Salary), 2) : 0,
        MaxSalary = filteredPeople.Any() ? filteredPeople.Max(p => p.Salary) : 0,
        Count = filteredPeople.Count
    };
    var options = new JsonSerializerOptions
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    return JsonSerializer.Serialize(result, options);
}