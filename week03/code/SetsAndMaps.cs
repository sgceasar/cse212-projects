using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Week03Code
{
    public static class SetsAndMaps
    {
        /// <summary>
        /// Problem 1: Find Pairs with Sets
        /// Given a list of two-letter words (lowercase, no duplicates),
        /// Words composed of the same two letters in same order (like “aa”) don’t count.
        /// </summary>
        public static List<string> FindPairs(List<string> words)
        {
            var result = new List<string>();
            var seen = new HashSet<string>();

            foreach (var word in words)
            {
                if (word.Length != 2) continue;
                if (word[0] == word[1]) continue;
                
                var reversed = new string(new char[]{ word[1], word[0] });
                if (seen.Contains(reversed))
                {
                    result.Add($"{reversed} & {word}");
                }
                else
                {
                    seen.Add(word);
                }
            }

            return result;
        }

        /// <summary>
        /// Problem 2: Degree Summary
        /// Returns a dictionary mapping degree string to count.
        /// </summary>
        public static Dictionary<string, int> SummarizeDegrees(string filePath)
        {
            var summary = new Dictionary<string,int>(StringComparer.OrdinalIgnoreCase);

            using (var reader = new StreamReader(filePath))
            {
                string? line;
                while ((line = reader.ReadLine()) != null)
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    // assuming file is tab or comma separated? Let's assume tab-separated as typical
                    var parts = line.Split(new char[]{'\t',','}, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length < 4) continue;
                    var degree = parts[3].Trim();
                    if (degree == "") continue;

                    if (!summary.ContainsKey(degree))
                        summary[degree] = 0;
                    summary[degree]++;
                }
            }

            return summary;
        }

        /// <summary>
        /// Problem 3: Anagrams
        /// Determine if 2 words are anagrams it should ignore spaces and case
        /// </summary>
        public static bool IsAnagram(string word1, string word2)
        {
            if (word1 == null || word2 == null) return false;

            // remove spaces, to lower
            var w1 = new string(word1.Where(ch => !char.IsWhiteSpace(ch)).ToArray()).ToLowerInvariant();
            var w2 = new string(word2.Where(ch => !char.IsWhiteSpace(ch)).ToArray()).ToLowerInvariant();

            if (w1.Length != w2.Length) return false;

            var counts = new Dictionary<char,int>();
            foreach (var ch in w1)
            {
                if (!counts.ContainsKey(ch))
                    counts[ch] = 0;
                counts[ch]++;
            }
            foreach (var ch in w2)
            {
                if (!counts.ContainsKey(ch)) return false;
                counts[ch]--;
                if (counts[ch] < 0) return false;
            }
            // optional: check all zero but if lengths equal and no negative count, it's fine
            return true;
        }
    }

    public class Maze
    {
        // Maze representation: Dictionary of X,Y
        public struct Cell
        {
            public bool Left;
            public bool Right;
            public bool Up;
            public bool Down;
        }

        private readonly Dictionary<(int x, int y), Cell> _maze;

        public Maze(Dictionary<(int x, int y), Cell> maze)
        {
            _maze = maze;
        }

        public (int x, int y) MoveLeft((int x, int y) pos)
        {
            if (!_maze.TryGetValue(pos, out var cell)) return pos;
            if (cell.Left)
            {
                return (pos.x - 1, pos.y);
            }
            return pos;
        }

        public (int x, int y) MoveRight((int x, int y) pos)
        {
            if (!_maze.TryGetValue(pos, out var cell)) return pos;
            if (cell.Right)
            {
                return (pos.x + 1, pos.y);
            }
            return pos;
        }

        public (int x, int y) MoveUp((int x, int y) pos)
        {
            if (!_maze.TryGetValue(pos, out var cell)) return pos;
            if (cell.Up)
            {
                return (pos.x, pos.y - 1);
            }
            return pos;
        }

        public (int x, int y) MoveDown((int x, int y) pos)
        {
            if (!_maze.TryGetValue(pos, out var cell)) return pos;
            if (cell.Down)
            {
                return (pos.x, pos.y + 1);
            }
            return pos;
        }
    }

    // Problem 5: Earthquake JSON Data
    public class EarthquakeData
    {
        public Metadata? metadata { get; set; }
        public List<Feature>? features { get; set; }
        public List<double>? bbox { get; set; }
    }

    public class Metadata
    {
        public long? generated { get; set; }
        public string? url { get; set; }
        public string? title { get; set; }
        public int? status { get; set; }
        public string? api { get; set; }
        public int? count { get; set; }
    }

    public class Feature
    {
        public string? type { get; set; }
        public Properties? properties { get; set; }
        public Geometry? geometry { get; set; }
        public string? id { get; set; }
    }

    public class Properties
    {
        public double? mag { get; set; }
        public string? place { get; set; }
        public long? time { get; set; }
        // other properties omitted
    }

    public class Geometry
    {
        public string? type { get; set; }
        public List<double>? coordinates { get; set; }
    }

    public static class EarthquakeHelper
    {
        /// <summary>
        /// "Place – Mag X.Y"
        /// </summary>
        public static async Task<List<string>> EarthquakeDailySummaryAsync()
        {
            // Earthquacke magnitudes:
            var url = "https://earthquake.usgs.gov/earthquakes/feed/v1.0/summary/all_day.geojson";

            using (var client = new HttpClient())
            {
                var json = await client.GetStringAsync(url);
                var data = JsonSerializer.Deserialize<EarthquakeData>(json);

                var results = new List<string>();
                if (data?.features != null)
                {
                    foreach (var feature in data.features)
                    {
                        var prop = feature.properties;
                        if (prop == null) continue;
                        var place = prop.place ?? "Unknown place";
                        var mag = prop.mag.HasValue ? prop.mag.Value : 0.0;
                        results.Add($"{place} - Mag {mag}");
                    }
                }

                return results;
            }
        }
    }
}
