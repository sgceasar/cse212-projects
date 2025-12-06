using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Node
{
    public int Data { get; set; }
    public Node Left { get; private set; }
    public Node Right { get; private set; }

    public Node(int value)
    {
        Data = value;
    }

    // Problem 1
    // Returns true if the value was inserted, false if it was a duplicate.
    public bool Insert(int value)
    {
        if (value == Data)
        {
            return false; // Duplicate found, do not insert (Problem 1)
        }

        if (value < Data)
        {
            if (Left is null)
            {
                Left = new Node(value);
                return true;
            }
            return Left.Insert(value);
        }
        else // value > Data
        {
            if (Right is null)
            {
                Right = new Node(value);
                return true;
            }
            return Right.Insert(value);
        }
    }

    // Problem 2: Contains
    // Implements the recursive logic to search for a value in the tree.
    public bool Contains(int value)
    {
        if (value == Data)
        {
            return true;
        }
        else if (value < Data)
        {
            // If less, check left subtree
            return Left is not null && Left.Contains(value);
        }
        else // value > Data
        {
            // If greater, check right subtree
            return Right is not null && Right.Contains(value);
        }
    }

    // Problem 4
    public int GetHeight()
    {
        // Use the null conditional operator
        int leftHeight = Left?.GetHeight() ?? 0;
        int rightHeight = Right?.GetHeight() ?? 0;
        
        // The height of the current node is 1 plus the height of the taller child.
        return 1 + Math.Max(leftHeight, rightHeight);
    }
    
    // Helper for standard in-order traversal
    public IEnumerable<int> TraverseForward()
    {
        if (Left is not null)
        {
            foreach (var item in Left.TraverseForward())
            {
                yield return item;
            }
        }

        yield return Data;

        if (Right is not null)
        {
            foreach (var item in Right.TraverseForward())
            {
                yield return item;
            }
        }
    }
    // Problem 3 Helper: Traverse Backwards (Reverse In-Order Traversal)
    // Traverses Right -> Root -> Left to get values from largest to smallest.
    public IEnumerable<int> TraverseBackward()
    {
        if (Right is not null)
        {
            foreach (var item in Right.TraverseBackward())
            {
                yield return item;
            }
        }

        yield return Data;

        if (Left is not null)
        {
            foreach (var item in Left.TraverseBackward())
            {
                yield return item;
            }
        }
    }
}

public class BinarySearchTree : IEnumerable<int>
{
    public Node Root { get; private set; }

    public void Insert(int value)
    {
        if (Root is null)
        {
            Root = new Node(value);
        }
        else
        {
            // Problem 1
            Root.Insert(value);
        }
    }
    
    // Problem 2
    public bool Contains(int value)
    {
        return Root is not null && Root.Contains(value);
    }

    // Problem 4
    public int GetHeight()
    {
        if (Root is null)
            return 0; // The height of an empty tree is 0
        
        return Root.GetHeight(); // Problem 4 logic is in Node.GetHeight
    }

    // Problem 3: Traverse Backwards
    // Returns an IEnumerable of the tree's values in reverse order (largest to smallest).
    public IEnumerable<int> Reversed()
    {
        if (Root is not null)
        {
            // Calls the reverse in-order traversal logic in Node
            foreach (var item in Root.TraverseBackward())
            {
                yield return item;
            }
        }
    }

    public IEnumerator<int> GetEnumerator()
    {
        if (Root is not null)
        {
            return Root.TraverseForward().GetEnumerator();
        }
        return Enumerable.Empty<int>().GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

// Problem 5: Create Tree from Sorted List
// The required static class structure is used here.
public static class Trees
{
    /// <summary>
    /// Given a sorted list (sorted_list), create a balanced BST. 
    /// </summary>
    public static BinarySearchTree CreateTreeFromSortedList(int[] sortedNumbers)
    {
        var bst = new BinarySearchTree(); // Create an empty BST to start with 
        InsertMiddle(sortedNumbers, 0, sortedNumbers.Length - 1, bst);
        return bst;
    }

    /// <summary>
    /// This function will atempt to insert the item in the middle of 'sortedNumbers' into the 'bst' tree. The middle is determined by using indices represented by 'first' and 'last'.
    /// </summary>
    private static void InsertMiddle(int[] sortedNumbers, int first, int last, BinarySearchTree bst)
    {
        // Base case: If the range is invalid or empty, stopp the recursion
        if (first > last)
        {
            return;
        }

        // 1. Find the midle index of the current range
        // Using integer division to find the middle index.
        int middle = (first + last) / 2;

        // 2. Insert the midle value into the BST (This ensures balance)
        bst.Insert(sortedNumbers[middle]);

        // 3. Recursively call InsertMiddle for the left half (first to middle - 1)
        InsertMiddle(sortedNumbers, first, middle - 1, bst);

        // 4. Recursively call InsertMiddle for the right half (middle + 1 to last)
        InsertMiddle(sortedNumbers, middle + 1, last, bst);
    }
}
