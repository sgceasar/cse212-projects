using System.Collections;
using System.Text;

public static class Recursion
{
    // Problem 1: Sum Squares
    public static int SumSquaresRecursive(int n)
    {
        if (n <= 0)
            return 0;

        return n * n + SumSquaresRecursive(n - 1);
    }
    // Problem 2: Permutations
    public static void PermutationsChoose(List<string> results, string letters, int size, string word = "")
    {
        // Base case: if the word is of required length, add to results
        if (word.Length == size)
        {
            results.Add(word);
            return;
        }

        // Recursive case: choose each possible letter
        for (int i = 0; i < letters.Length; i++)
        {
            char letter = letters[i];
            string remaining = letters.Remove(i, 1);
            PermutationsChoose(results, remaining, size, word + letter);
        }
    }
    // Problem 3: Climbing
    public static decimal CountWaysToClimb(int s, Dictionary<int, decimal>? remember = null)
    {
        if (remember == null)
            remember = new Dictionary<int, decimal>();

        // Base cases
        if (s == 0) return 0;
        if (s == 1) return 1;
        if (s == 2) return 2;
        if (s == 3) return 4;

        // Memoization check
        if (remember.ContainsKey(s))
            return remember[s];

        // Compute recursively with memoization
        decimal ways =
            CountWaysToClimb(s - 1, remember) +
            CountWaysToClimb(s - 2, remember) +
            CountWaysToClimb(s - 3, remember);

        remember[s] = ways;
        return ways;
    }

    // Problem 4: Wildcards
    public static void WildcardBinary(string pattern, List<string> results)
    {
        int index = pattern.IndexOf('*');

        // Base case: no wildcard, add final result
        if (index == -1)
        {
            results.Add(pattern);
            return;
        }

        // Replace * with 0
        string option0 = pattern.Substring(0, index) + "0" + pattern[(index + 1)..];
        WildcardBinary(option0, results);

        // Replace * with 1
        string option1 = pattern.Substring(0, index) + "1" + pattern[(index + 1)..];
        WildcardBinary(option1, results);
    }
    // # Problem 5: Maze Solver
    public static void SolveMaze(List<string> results, Maze maze, int x = 0, int y = 0, List<ValueTuple<int, int>>? currPath = null)
    {
        if (currPath == null)
            currPath = new List<(int, int)>();

        // Out of bounds or wall
        if (!maze.InBounds(x, y) || maze.IsWall(x, y))
            return;

        // Already visited
        if (currPath.Contains((x, y)))
            return;

        // Add current position
        currPath.Add((x, y));

        // Check goal
        if (maze.IsEnd(x, y))
        {
            results.Add(currPath.AsString());
            currPath.RemoveAt(currPath.Count - 1);
            return;
        }

        // Explore using up, down, left, right
        SolveMaze(results, maze, x + 1, y, currPath);
        SolveMaze(results, maze, x - 1, y, currPath);
        SolveMaze(results, maze, x, y + 1, currPath);
        SolveMaze(results, maze, x, y - 1, currPath);

        // Backtrack
        currPath.RemoveAt(currPath.Count - 1);
    }
}

// Helper Extension 
public static class PathExtensions
{
    public static string AsString(this List<(int, int)> path)
    {
        StringBuilder sb = new StringBuilder();
        foreach (var p in path)
            sb.Append($"({p.Item1},{p.Item2}) ");
        return sb.ToString().Trim();
    }
}
