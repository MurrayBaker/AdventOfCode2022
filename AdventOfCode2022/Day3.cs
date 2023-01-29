using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day3 : IDay
    {
        public async Task<string> Part1()
        {
            var lines = await File.ReadAllLinesAsync("Day3.txt");

            var total = 0;

            foreach (var line in lines)
            {
                var firstCompartment = line.Take(line.Length / 2);
                var secondCompartment = line.Skip(line.Length / 2);

                var overlap = firstCompartment.Intersect(secondCompartment).First();

                total += GetPriority(overlap);
            }

            return total.ToString();
        }

        public async Task<string> Part2()
        {
            var lines = await File.ReadAllLinesAsync("Day3.txt");

            var chunks = lines.Chunk(3);

            var total = 0;

            foreach (var chunk in chunks)
            {
                var overlap = chunk[0].Intersect(chunk[1]).Intersect(chunk[2]).First();

                total += GetPriority(overlap);
            }

            return total.ToString();
        }

        private static int GetPriority(char input)
            => Char.IsLower(input)
                ? input - 96
                : input - 38; // Filthy
    }
}
