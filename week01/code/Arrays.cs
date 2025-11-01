using System.Collections.Generic;
using System.Linq;


public static class Arrays
{
    /// <param name="start">The starting number (the first multiple).</param>
    /// <param name="count">The number of multiples to generate.</param>
    /// <returns>A new array containing the requested multiples.</returns>
    public static double[] MultiplesOf(double start, int count)
    {
        // 1# Plan: Multiples Of
        // 1- Create a new double array named 'result' with the size specified by 'count'.
        // 2- Loop through the array from index 0 up to 'count - 1'. This loop structure ensures we populate every element of the new array.
        // 3- For each index 'i', calculate the corresponding multiple. Since 'i' starts at 0, the multiple number needed is (i + 1).
        // 4- The calculation for the value is: start * (i + 1).
        // 5- Store the calculated value in result[i].
        // 6- Return the completed 'result' array.
        
        double[] result = new double[count];

        for (int i = 0; i < count; i++)
        {
            result[i] = start * (i + 1);
        }
        
        return result;
    }

    /// <param name="data">The list of integers to rotate.</param>
    /// <param name="amount">The number of positions to rotate right.</param>
    /// <returns>The modified list (for convenience).</returns>
    public static List<int> RotateListRight(List<int> data, int amount)
    {
        // 2# Plan: Rotate Right
        // 1- Handle edge cases: If the list is null, empty, or the amount is 0, return the list.
        // 2- Determine the starting index for the sub-list (the 'tail') that will move to the front. This index is data.Count - amount.
        // 3- Remove the 'tail' section from the end of the original 'data' list using List<T>.RemoveRange(startIndex, amount).
        // 4- Insert the stored 'tailToMove' list at the beginning of 'data' (index 0) using List<T>.InsertRange(0, tailToMove).
        // 5- Return the modified 'data' list.

        if (data == null || data.Count == 0 || amount <= 0)
        {
            return data;
        }

        int count = data.Count;
        
        int effectiveAmount = amount % count;
        if (effectiveAmount == 0)
            return data;

        int startIndex = count - effectiveAmount;

        List<int> tailToMove = data.GetRange(startIndex, effectiveAmount);

        data.RemoveRange(startIndex, effectiveAmount);

        data.InsertRange(0, tailToMove);

        return data;
    }
}
