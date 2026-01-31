using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// Problem 1: Find all symmetric pairs of two-letter words (O(n))
    /// </summary>
    public static string[] FindPairs(string[] words)
    {
        var set = new HashSet<string>(words);
        var used = new HashSet<string>();
        var result = new List<string>();

        foreach (var word in words)
        {
            if (used.Contains(word)) continue;

            char a = word[0];
            char b = word[1];
            if (a == b) continue;

            // FAST reverse (no LINQ)
            string reversed = $"{b}{a}";

            if (set.Contains(reversed) && !used.Contains(reversed))
            {
                result.Add($"{word} & {reversed}");
                used.Add(word);
                used.Add(reversed);
            }
        }

        return result.ToArray();
    }

    /// <summary>
    /// Problem 2: Summarize degrees from a CSV file
    /// </summary>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();

        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(',');
            if (fields.Length < 4) continue;

            string degree = fields[3].Trim();
            degrees[degree] = degrees.GetValueOrDefault(degree, 0) + 1;
        }

        return degrees;
    }

    /// <summary>
    /// Problem 3: Check if two words are anagrams
    /// </summary>
   public static bool IsAnagram(string word1, string word2)
{
    string Clean(string w) =>
        new string(w.ToLower().Where(c => c != ' ').ToArray());

    var w1 = Clean(word1);
    var w2 = Clean(word2);

    if (w1.Length != w2.Length) return false;

    var counts = new Dictionary<char, int>();

    foreach (char c in w1)
        counts[c] = counts.GetValueOrDefault(c, 0) + 1;

    foreach (char c in w2)
    {
        if (!counts.ContainsKey(c)) return false;
        counts[c]--;
        if (counts[c] < 0) return false;
    }

    return true;
}


    /// <summary>
    /// Problem 5: Earthquake summary from USGS JSON feed
    /// </summary>
   public static string[] EarthquakeDailySummary()
{
    const string uri =
        "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";

    using var client = new HttpClient();
    var json = client.GetStringAsync(uri).Result;

    var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    var data = JsonSerializer.Deserialize<FeatureCollection>(json, options);

    var result = new List<string>();

    if (data?.Features != null)
    {
        foreach (var f in data.Features)
        {
            string place = f.Properties?.Place ?? "Unknown location";
            double mag = f.Properties?.Mag ?? 0.0;

            // 🔑 MUST contain lowercase "magnitude"
            result.Add($"location: {place}, magnitude {mag}");
        }
    }

    return result.ToArray();
}




}
