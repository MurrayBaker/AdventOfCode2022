using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day6 : IDay
    {
        public async Task<string> Part1()
        {
            var input = await File.ReadAllTextAsync("Day6.txt");

            var previousCharacters = input.Take(3).ToList();

            var characterCount = 2;

            while (true)
            {
                characterCount += 1;

                if (!previousCharacters.Contains(input[characterCount]) 
                    && NoDuplicates(previousCharacters))
                {
                    return (characterCount + 1).ToString();
                }

                previousCharacters.RemoveAt(0);
                previousCharacters.Add(input[characterCount]);
            }
        }

        public async Task<string> Part2()
        {
            var input = await File.ReadAllTextAsync("Day6.txt");

            var previousCharacters = input.Take(13).ToList();

            var characterCount = 12;

            while (true)
            {
                characterCount += 1;

                if (!previousCharacters.Contains(input[characterCount])
                    && NoDuplicates(previousCharacters))
                {
                    return (characterCount + 1).ToString();
                }

                previousCharacters.RemoveAt(0);
                previousCharacters.Add(input[characterCount]);
            }
        }

        private static bool NoDuplicates<T>(List<T> input)
            => !input.GroupBy(i => i).Select(g => g.Count()).Any(c => c > 1);
    }
}
