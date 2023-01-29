using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day4 : IDay
    {
        public async Task<string> Part1()
        {
            var lines = await File.ReadAllLinesAsync("Day4.txt");

            var total = 0;

            foreach (var line in lines)
            {
                var elfRanges = line.Split(",");
                var firstRange = elfRanges[0];
                var secondRange = elfRanges[1];

                var (FirstRangeStart, FirstRangeEnd) = GetLimits(firstRange);
                var (SecondRangeStart, SecondRangeEnd) = GetLimits(secondRange);
                
                if ((FirstRangeStart >= SecondRangeStart && FirstRangeEnd <= SecondRangeEnd)
                    || (SecondRangeStart >= FirstRangeStart && SecondRangeEnd <= FirstRangeEnd))
                {
                    total += 1;
                }
            }

            return total.ToString();
        }

        public async Task<string> Part2()
        {
            var lines = await File.ReadAllLinesAsync("Day4.txt");

            var total = 0;

            foreach (var line in lines)
            {
                var elfRanges = line.Split(",");
                var firstRange = elfRanges[0];
                var secondRange = elfRanges[1];

                var (FirstRangeStart, FirstRangeEnd) = GetLimits(firstRange);
                var (SecondRangeStart, SecondRangeEnd) = GetLimits(secondRange);

                if ((FirstRangeStart >= SecondRangeStart && FirstRangeStart <= SecondRangeEnd)
                    || (SecondRangeStart >= FirstRangeStart && SecondRangeStart <= FirstRangeEnd))
                {
                    total += 1;
                }
            }

            return total.ToString();
        }

        private static (int Start, int End) GetLimits(string range)
        {
            var parts = range.Split("-");

            return (int.Parse(parts[0]), int.Parse(parts[1]));
        }
    }
}
