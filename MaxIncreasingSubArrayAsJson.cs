using System.Linq;
using System.Text.Json;
using System.Collections.Generic;

public static string MaxIncreasingSubArrayAsJson(List<int> numbers)
{
    List<int> maxSub = new List<int>();
    int maxTotal = int.MinValue;
    int i = 0;

    if (numbers == null || numbers.Count == 0)
        return JsonSerializer.Serialize(new List<int>());

    while (i < numbers.Count)
    {
        List<int> currentSub = new List<int> { numbers[i] };
        int total = numbers[i];
        i++;

        while (i < numbers.Count && numbers[i] > numbers[i - 1])
        {
            currentSub.Add(numbers[i]);
            total += numbers[i];
            i++;
        }
        if (total > maxTotal)
        {
            maxTotal = total;
            maxSub = currentSub;
        }
    }
    return JsonSerializer.Serialize(maxSub);
}