using System.Text.Json;

public static class SetsAndMaps
{
    /// <summary>
    /// The words parameter contains a list of two character 
    /// words (lower case, no duplicates). Using sets, find an O(n) 
    /// solution for returning all symmetric pairs of words.  
    ///
    /// For example, if words was: [am, at, ma, if, fi], we would return :
    ///
    /// ["am & ma", "if & fi"]
    ///
    /// The order of the array does not matter, nor does the order of the specific words in each string in the array.
    /// at would not be returned because ta is not in the list of words.
    ///
    /// As a special case, if the letters are the same (example: 'aa') then
    /// it would not match anything else (remember the assumption above
    /// that there were no duplicates) and therefore should not be returned.
    /// </summary>
    /// <param name="words">An array of 2-character words (lowercase, no duplicates)</param>
    public static string[] FindPairs(string[] words)
    {
        // Build a set for O(1) lookups
        var set = new HashSet<string>(words);
        var results = new List<string>();

        foreach (var w in words)
        {
            // same-letter words do not pair
            if (w.Length == 2 && w[0] == w[1])
            {
                continue;
            }

            var r = new string(new[] { w[1], w[0] });

            // avoid duplicates by enforcing an order
            if (set.Contains(r) && string.Compare(w, r, StringComparison.Ordinal) < 0)
            {
                results.Add($"{w} & {r}");
            }
        }

        return results.ToArray();
    }

    /// <summary>
    /// Read a census file and summarize the degrees (education)
    /// earned by those contained in the file.  The summary
    /// should be stored in a dictionary where the key is the
    /// degree earned and the value is the number of people that 
    /// have earned that degree.  The degree information is in
    /// the 4th column of the file.  There is no header row in the
    /// file.
    /// </summary>
    /// <param name="filename">The name of the file to read</param>
    /// <returns>fixed array of divisors</returns>
    public static Dictionary<string, int> SummarizeDegrees(string filename)
    {
        var degrees = new Dictionary<string, int>();
        foreach (var line in File.ReadLines(filename))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            var fields = line.Split(',', StringSplitOptions.None);
            if (fields.Length <= 3)
            {
                continue;
            }

            var degree = fields[3].Trim();
            if (degree.Length == 0)
            {
                continue;
            }

            if (degrees.ContainsKey(degree))
            {
                degrees[degree] += 1;
            }
            else
            {
                degrees[degree] = 1;
            }
        }

        return degrees;
    }

    /// <summary>
    /// Determine if 'word1' and 'word2' are anagrams.  An anagram
    /// is when the same letters in a word are re-organized into a 
    /// new word.  A dictionary is used to solve the problem.
    /// 
    /// Examples:
    /// is_anagram("CAT","ACT") would return true
    /// is_anagram("DOG","GOOD") would return false because GOOD has 2 O's
    /// 
    /// Important Note: When determining if two words are anagrams, you
    /// should ignore any spaces.  You should also ignore cases.  For 
    /// example, 'Ab' and 'Ba' should be considered anagrams
    /// 
    /// Reminder: You can access a letter by index in a string by 
    /// using the [] notation.
    /// </summary>
    public static bool IsAnagram(string word1, string word2)
    {
        // Use a frequency dictionary, ignoring spaces and case
        var counts = new Dictionary<char, long>();

        foreach (var c in word1)
        {
            if (c == ' ')
            {
                continue;
            }

            var ch = char.ToUpperInvariant(c);
            if (counts.TryGetValue(ch, out var current))
            {
                counts[ch] = current + 1;
            }
            else
            {
                counts[ch] = 1;
            }
        }

        foreach (var c in word2)
        {
            if (c == ' ')
            {
                continue;
            }

            var ch = char.ToUpperInvariant(c);
            if (!counts.TryGetValue(ch, out var current))
            {
                return false;
            }

            current -= 1;
            if (current == 0)
            {
                counts.Remove(ch);
            }
            else
            {
                counts[ch] = current;
            }
        }

        return counts.Count == 0;
    }

    /// <summary>
    /// This function will read JSON (Javascript Object Notation) data from the 
    /// United States Geological Service (USGS) consisting of earthquake data.
    /// The data will include all earthquakes in the current day.
    /// 
    /// JSON data is organized into a dictionary. After reading the data using
    /// the built-in HTTP client library, this function will return a list of all
    /// earthquake locations ('place' attribute) and magnitudes ('mag' attribute).
    /// Additional information about the format of the JSON data can be found 
    /// at this website:  
    /// 
    /// https://earthquake.usgs.gov/earthquakes/feed/v1.0/geojson.php
    /// 
    /// </summary>
    public static string[] EarthquakeDailySummary()
    {
        const string uri = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";
        using var client = new HttpClient();
        using var getRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri);
        using var jsonStream = client.Send(getRequestMessage).Content.ReadAsStream();
        using var reader = new StreamReader(jsonStream);
        var json = reader.ReadToEnd();
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        var featureCollection = JsonSerializer.Deserialize<FeatureCollection>(json, options);

        // TODO Problem 5:
        // 1. Add code in FeatureCollection.cs to describe the JSON using classes and properties 
        // on those classes so that the call to Deserialize above works properly.
        // 2. Add code below to create a string out each place a earthquake has happened today and its magitude.
        // 3. Return an array of these string descriptions.
        if (featureCollection?.Features == null)
        {
            return Array.Empty<string>();
        }

        var result = new List<string>(featureCollection.Features.Count);
        foreach (var feature in featureCollection.Features)
        {
            var place = feature?.Properties?.Place ?? "Unknown";
            string magText;
            var mag = feature?.Properties?.Mag;
            if (mag.HasValue)
            {
                magText = mag.Value.ToString("0.##");
            }
            else
            {
                magText = "N/A";
            }

            result.Add($"{place} - Mag {magText}");
        }

        return result.ToArray();
    }
}