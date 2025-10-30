using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{

    /*
     * Complete the 'cookies' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER k
     *  2. INTEGER_ARRAY A
     */

    public static int cookies(int k, List<int> A)
    {
        PriorityQueue<int, int> minHeap = new PriorityQueue<int, int>();
        
        // Add all cookies to the min heap
        foreach (int cookie in A) {
            minHeap.Enqueue(cookie, cookie);
        }
        
        int operations = 0;
        
        while (minHeap.Count >= 2 && minHeap.Peek() < k) {
            int leastSweet = minHeap.Dequeue();
            int secondLeastSweet = minHeap.Dequeue();
            
            int newCookie = leastSweet + 2 * secondLeastSweet;
            minHeap.Enqueue(newCookie, newCookie);
            operations++;
        }
        
        // Check if all cookies meet the sweetness requirement
        return (minHeap.Peek() >= k) ? operations : -1;
    }

    // Alternative implementation using SortedSet if PriorityQueue is not available
    public static int cookiesAlternative(int k, List<int> A) {
        var sortedSet = new SortedSet<(int value, int id)>();
        int idCounter = 0;
        
        foreach (int cookie in A) {
            sortedSet.Add((cookie, idCounter++));
        }
        
        int operations = 0;
        
        while (sortedSet.Count >= 2 && sortedSet.Min.value < k) {
            var min1 = sortedSet.Min;
            sortedSet.Remove(min1);
            
            var min2 = sortedSet.Min;
            sortedSet.Remove(min2);
            
            int newCookie = min1.value + 2 * min2.value;
            sortedSet.Add((newCookie, idCounter++));
            operations++;
        }
        
        return (sortedSet.Min.value >= k) ? operations : -1;
    }

    }

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int n = Convert.ToInt32(firstMultipleInput[0]);

        int k = Convert.ToInt32(firstMultipleInput[1]);

        List<int> A = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(ATemp => Convert.ToInt32(ATemp)).ToList();

        int result = Result.cookies(k, A);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
