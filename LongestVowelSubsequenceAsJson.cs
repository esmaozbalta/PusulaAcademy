using System.Linq;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Collections.Generic;

public static string LongestVowelSubsequenceAsJson(List<string> words)
{
    var vowels = new HashSet<char> { 'a', 'A', 'e', 'E', 'i', 'I', 'İ', 'ı', 'o', 'O', 'ö', 'Ö', 'u', 'U', 'ü', 'Ü' };
    var results = new List<object>();

    foreach (var word in words)
    {
        string longestSub = "";
        string currentSub = "";

        foreach (char c in word)
        {
            if (vowels.Contains(c))
            {
                currentSub += c;
            }
            else
            {
                if (currentSub.Length > longestSub.Length)
                {
                    longestSub = currentSub;
                }
                currentSub = "";
            }
        }

        if (currentSub.Length > longestSub.Length)
        {
            longestSub = currentSub;
        }

        var result = new
        {
            word = word,
            sequence = longestSub,
            length = longestSub.Length
        };
        results.Add(result);
    }
    var options = new JsonSerializerOptions
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };

    return JsonSerializer.Serialize(results, options);
}