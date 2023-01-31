using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day10 : IDay
    {
        public async Task<string> Part1()
        {
            var lines = await File.ReadAllLinesAsync("Day10.txt");

            var registerValues = ComputeRegisterValues(lines);

            var numberOfRelevantCycles = (registerValues.Count() - 20) / 40;

            var relevantCycles = new List<int> { 19 };

            for (int i = 0; i < numberOfRelevantCycles; i++)
            {
                relevantCycles.Add(19 + 40 * (i + 1));
            }

            return relevantCycles.Select(c => GetSignalStrength(registerValues, c)).Sum().ToString();
        }

        public async Task<string> Part2()
        {
            var lines = await File.ReadAllLinesAsync("Day10.txt");

            var registerValues = ComputeRegisterValues(lines).ToList();
            var numberOfRegisterValues = registerValues.Count();

            var builder = new StringBuilder();

            for (int i = 0; i < numberOfRegisterValues; i++)
            {
                builder.Append(SpriteIsVisible(registerValues[i], i % 40) ? "#" : ".");
            }

            var result = builder.ToString();

            builder.Clear();
            for (int i = 0; i < numberOfRegisterValues / 40; i++)
            {
                builder.AppendLine(new string(result.Skip(40 * i).Take(40).ToArray()));
            }

            return builder.ToString();
        }

        private bool SpriteIsVisible((int during, int _) registerValues, int cycleNumber)
            => Math.Abs(registerValues.during - cycleNumber) < 2;

        private IEnumerable<(int duringValue, int finishedValue)> ComputeRegisterValues(string[] instructions)
        {
            var duringValue = 1;
            var finishedValue = 1;

            foreach (var instruction in instructions)
            {
                if (instruction == "noop")
                {
                    yield return (duringValue, finishedValue);
                }
                else
                {
                    yield return (duringValue, finishedValue);
                    finishedValue = duringValue + int.Parse(instruction.Split(" ")[1]);
                    yield return (duringValue, finishedValue);
                }

                duringValue = finishedValue;
            }
        }

        private int GetSignalStrength(IEnumerable<(int during, int finished)> registerValues, int position)
            => registerValues.Skip(position).First().during * (position + 1);
    }
}
