using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day1 : IDay
    {
        public async Task<string> Part1()
            => (await GetTotals()).Max().ToString();

        public async Task<string> Part2()
            => (await GetTotals()).OrderByDescending(t => t).Take(3).Sum().ToString();

        private static async Task<List<int>> GetTotals()
        {
            var lines = await File.ReadAllLinesAsync("Day1.txt");

            var totals = new List<int>();
            var total = 0;
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    totals.Add(total);
                    total = 0;
                    continue;
                }

                total += int.Parse(line);
            }

            return totals;
        }
    }
}
