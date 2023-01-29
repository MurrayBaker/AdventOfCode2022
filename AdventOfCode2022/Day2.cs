using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day2 : IDay
    {
        public async Task<string> Part1()
        {
            var lines = await File.ReadAllLinesAsync("Day2.txt");

            var totalScore = 0;

            foreach (var line in lines)
            {
                var glyphs = line.Split(" ");
                var theirThrow = NormaliseGlyph(glyphs[0]);
                var ourThrow = NormaliseGlyph(glyphs[1]);

                totalScore += GetThrowScore(ourThrow);
                totalScore += GetGameScore(ourThrow, theirThrow);
            }

            return totalScore.ToString();
        }

        public async Task<string> Part2()
        {
            var lines = await File.ReadAllLinesAsync("Day2.txt");

            var totalScore = 0;

            foreach (var line in lines)
            {
                var glyphs = line.Split(" ");
                var theirThrow = NormaliseGlyph(glyphs[0]);
                var desiredOutcome = glyphs[1];

                var ourThrow = desiredOutcome switch
                {
                    "X" => LoseGame(theirThrow),
                    "Y" => theirThrow,
                    "Z" => WinGame(theirThrow),
                    _ => throw new ArgumentOutOfRangeException(nameof(desiredOutcome)),
                };

                totalScore += GetThrowScore(ourThrow);
                totalScore += GetGameScore(ourThrow, theirThrow);
            }

            return totalScore.ToString();
        }

        private static char LoseGame(char input)
            => input switch
            {
                'R' => 'S',
                'P' => 'R',
                'S' => 'P',
                _ => throw new ArgumentOutOfRangeException(nameof(input)),
            };

        private static char WinGame(char input)
            => input switch
            {
                'R' => 'P',
                'P' => 'S',
                'S' => 'R',
                _ => throw new ArgumentOutOfRangeException(nameof(input)),
            };

        private static char NormaliseGlyph(string input)
            => input switch
            {
                "A" or "X" => 'R',
                "B" or "Y" => 'P',
                "C" or "Z" => 'S',
                _ => throw new ArgumentOutOfRangeException(nameof(input)),
            };

        private static int GetThrowScore(char input)
            => input switch
            {
                'R' => 1,
                'P' => 2,
                'S' => 3,
                _ => throw new ArgumentOutOfRangeException(nameof(input)),
            };

        private static int GetGameScore(char ourThrow, char theirThrow)
        {
            if (ourThrow == theirThrow)
            {
                return 3;
            }

            return ourThrow switch
            {
                'R' => theirThrow == 'P' ? 0 : 6,
                'P' => theirThrow == 'S' ? 0 : 6,
                'S' => theirThrow == 'R' ? 0 : 6,
                _ => throw new ArgumentOutOfRangeException(nameof(ourThrow)),
            };
        }
    }
}
