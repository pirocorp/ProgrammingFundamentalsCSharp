﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace _02.Anonymous_Cache
{
    class Program
    {
        static void Main()
        {
            var input = string.Empty;

            var dataDict = new Dictionary<string, Dictionary<string, long>>();
            var cacheDict = new Dictionary<string, Dictionary<string, long>>();

            while (input != "thetinggoesskrra")
            {
                input = Console.ReadLine();
                if (input == "thetinggoesskrra") break;
                var tokens = input.Split(new[] { ' ', '-', '>', '|' }, StringSplitOptions.RemoveEmptyEntries);

                if (tokens.Length == 1)
                {
                    var currentDataSet = tokens[0];
                    dataDict[currentDataSet] = new Dictionary<string, long>();
                    foreach (var cashData in cacheDict)
                    {
                        if (dataDict.ContainsKey(cashData.Key))
                        {
                            dataDict[cashData.Key] = cashData.Value;
                            cacheDict.Remove(cashData.Key);
                            break;
                        }
                    }
                    continue;
                }

                var dataKey = tokens[0];
                var dataSize = long.Parse(tokens[1]);
                var dataSet = tokens[2];

                if (!dataDict.ContainsKey(dataSet))
                {
                    if (!cacheDict.ContainsKey(dataSet))
                    {
                        cacheDict[dataSet] = new Dictionary<string, long>();
                    }
                    cacheDict[dataSet].Add(dataKey, dataSize);
                    continue;
                }

                //testList[key[index]].Add(value[index]);
                dataDict[dataSet].Add(dataKey, dataSize);

                foreach (var cashData in cacheDict)
                {
                    if (dataDict.ContainsKey(cashData.Key))
                    {
                        dataDict[cashData.Key] = cashData.Value;
                        cacheDict.Remove(cashData.Key);
                        break;
                    }
                }
            }

            var maxSum = 0l;
            var maxDataSet = string.Empty;
            var maxSet = string.Empty;

            foreach (var data in dataDict)
            {
                var currentSum = 0l;
                var currentString = string.Empty;
                foreach (var kvp in data.Value)
                {
                    currentSum += kvp.Value;
                    currentString += $"$.{kvp.Key}" + " ";
                }

                if (currentSum > maxSum)
                {
                    maxSum = currentSum;
                    maxDataSet = currentString;
                    maxSet = data.Key;
                }
            }

            var result = maxDataSet.Trim().Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries).ToList();
            if (maxSum > 0)
            {
                Console.WriteLine($"Data Set: {maxSet}, Total Size: {maxSum}");
                Console.WriteLine($"{String.Join(Environment.NewLine, result)}");
            }

        }
    }
}
