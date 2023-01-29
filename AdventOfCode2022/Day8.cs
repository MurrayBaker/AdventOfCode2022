using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day8 : IDay
    {
        public async Task<string> Part1()
        {
            var lines = await File.ReadAllLinesAsync("Day8.txt");

            var width = lines.First().Length;
            var height = lines.Length;
            var numberOfVisibleTrees = 0;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (i == 0 || j == 0 || i == width - 1 || j == height - 1)
                    {
                        numberOfVisibleTrees++;
                        continue;
                    }

                    var treeHeight = lines[j][i];

                    if (lines[j].Take(i).All(t => t < treeHeight))
                    {
                        numberOfVisibleTrees++;
                        continue;
                    }

                    if (lines[j].Skip(i + 1).All(t => t < treeHeight))
                    {
                        numberOfVisibleTrees++;
                        continue;
                    }

                    var verticalSlice = lines.Select(l => l[i]);

                    if (verticalSlice.Take(j).All(t => t < treeHeight))
                    {
                        numberOfVisibleTrees++;
                        continue;
                    }

                    if (verticalSlice.Skip(j + 1).All(t => t < treeHeight))
                    {
                        numberOfVisibleTrees++;
                        continue;
                    }
                }
            }

            return numberOfVisibleTrees.ToString();
        }

        public async Task<string> Part2()
        {
            var lines = await File.ReadAllLinesAsync("Day8.txt");

            var width = lines.First().Length;
            var height = lines.Length;
            var numberOfVisibleTrees = 1;
            var bestTree = 0;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    if (i == 0 || j == 0 || i == width - 1 || j == height - 1)
                    {
                        continue;
                    }

                    var treeHeight = lines[j][i];

                    var slice = lines[j].Take(i).Reverse().ToArray();
                    var visibleCount = 0;
                    for (int k = 0; k < i; k++)
                    {
                        if (slice[k] >= treeHeight)
                        {
                            visibleCount++;
                            break;
                        }

                        visibleCount++;
                    }

                    numberOfVisibleTrees *= visibleCount;

                    slice = lines[j].Skip(i + 1).ToArray();
                    visibleCount = 0;
                    for (int k = 0; k < width - (i + 1); k++)
                    {
                        if (slice[k] >= treeHeight)
                        {
                            visibleCount++;
                            break;
                        }

                        visibleCount++;
                    }

                    numberOfVisibleTrees *= visibleCount;

                    slice = lines.Select(l => l[i]).Take(j).Reverse().ToArray();
                    visibleCount = 0;
                    for (int k = 0; k < j; k++)
                    {
                        if (slice[k] >= treeHeight)
                        {
                            visibleCount++;
                            break;
                        }

                        visibleCount++;
                    }

                    numberOfVisibleTrees *= visibleCount;

                    slice = lines.Select(l => l[i]).Skip(j + 1).ToArray();
                    visibleCount = 0;
                    for (int k = 0; k < height - (j + 1); k++)
                    {
                        if (slice[k] >= treeHeight)
                        { 
                            visibleCount++;
                            break;
                        }

                        visibleCount++;
                    }

                    numberOfVisibleTrees *= visibleCount;

                    bestTree = Math.Max(numberOfVisibleTrees, bestTree);
                    numberOfVisibleTrees = 1;
                }
            }

            return bestTree.ToString();
        }
    }
}
