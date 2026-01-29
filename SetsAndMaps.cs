using System.Text.Json;
using System.Collections.Generic;

public static class SetsAndMaps
{
    /// <summary>
    /// Problem 1 - Find symmetric pairs using a set
    /// </summary>
    public static string[] FindPairs(string[] words)
    {
        var set = new HashSet<string>(words);
        var result = new List<string>();

        foreach (var word in words)
        {
            var reversed = new string(new char[] { word[1], word[0] });

            // Skip same-letter words like "aa"
            if (word == reversed)
                continue;

            if (set.Contains(reversed) && string.Compare(word, reversed) < 0)
            {
                result.Add($"{word} & {reversed}");
            }
        }

        return result.ToArray();
    }

    /// <summary>
    /// Problem 2 - Summarize degrees
    /// </summary>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();

        foreach (var line in File.ReadLines(filename))
        {
            var fields = line.Split(",");
            var degree = fields[3].Trim();

            if (!degrees.ContainsKey(degree))
                degrees[degree] = 0;

            degrees[degree]++;
        }

        return degrees;
    }

    /// <summary>
    /// Problem 3 - Check if two words are anagrams
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        word1 = word1.Replace(" ", "").ToLower();
        word2 = word2.Replace(" ", "").ToLower();

        if (word1.Length != word2.Length)
            return false;

        var counts = new Dictionary<char, int>();

        foreach (var c in word1)
        {
            if (!counts.ContainsKey(c))
                counts[c] = 0;
            counts[c]++;
        }

        foreach (var c in word2)
        {
            if (!counts.ContainsKey(c))
                return false;

            counts[c]--;

            if (counts[c] < 0)
                return false;
        }

        return true;
    }

    /// <summary>
    /// Problem 5 - Earthquake Daily Summary
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";

        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();

        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        var results = new List<string>();

        foreach (var feature in featureCollection.Features)
        {
            var place = feature.Properties.Place;
            var mag = feature.Properties.Mag;
            results.Add($"{place} - Mag {mag}");
        }

        return results.ToArray();
    }
}
