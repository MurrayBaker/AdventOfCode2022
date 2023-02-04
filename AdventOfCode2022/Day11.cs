using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day11 : IDay
    {
        public async Task<string> Part1()
        {
            var tokens = await File.ReadAllLinesAsync("Day11.txt");
            var monkeys = new List<Monkey>();

            for (int i = 0; i < tokens.Length; i += 7)
            {
                monkeys.Add(new Monkey(tokens.Skip(i).Take(6).ToArray()));
            }

            for (int i = 0; i < 20; i++)
            {
                foreach (var monkey in monkeys)
                {
                    monkey.ResolveRound(monkeys);
                }
            }

            return monkeys.OrderByDescending(m => m.InspectionCount).Select(m => m.InspectionCount).Take(2).Aggregate((a, b) => (a * b)).ToString();
        }

        public async Task<string> Part2()
        {
            var tokens = await File.ReadAllLinesAsync("Day11.txt");
            var monkeys = new List<Monkey>();

            for (int i = 0; i < tokens.Length; i += 7)
            {
                monkeys.Add(new Monkey(tokens.Skip(i).Take(6).ToArray()));
            }
            var lcm = monkeys.Select(m => m.Divisibility).Aggregate((a, b) => a * b);

            for (int i = 0; i < 10000; i++)
            {
                foreach (var monkey in monkeys)
                {
                    monkey.ResolveWorryingRound(monkeys, lcm);
                }
            }

            return monkeys.OrderByDescending(m => m.InspectionCount).Select(m => m.InspectionCount).Take(2).Aggregate((a, b) => (a * b)).ToString();
        }

        private class Monkey
        {
            public Monkey(string[] tokens)
            {
                Items = new string(tokens[1].SkipWhile(c => !char.IsNumber(c)).ToArray())
                    .Split(", ").Select(long.Parse).ToList();

                var operation = tokens[2][(tokens[2].LastIndexOf("new = old ") + 10)..].Split(" ");

                WorryEffect = operation[0] switch
                {
                    "+" => x => x + (int.TryParse(operation[1], out var y) ? y : x),
                    "*" => x => x * (int.TryParse(operation[1], out var y) ? y : x),
                    _ => throw new ArgumentOutOfRangeException(nameof(tokens)),
                };

                TargetMonkeyCalculator = x => x % int.Parse(tokens[3][(tokens[3].LastIndexOf("divisible by ") + 13)..]) == 0
                    ? int.Parse(tokens[4].Split(" ").Last())
                    : int.Parse(tokens[5].Split(" ").Last());

                Divisibility = int.Parse(tokens[3][(tokens[3].LastIndexOf("divisible by ") + 13)..]);
            }

            public void ResolveRound(List<Monkey> monkeys)
            {
                var itemCount = Items.Count;
                for (var i = 0; i < itemCount; i++)
                {
                    var item = Items[0];
                    item = WorryEffect(item);
                    item /= 3;

                    monkeys[TargetMonkeyCalculator(item)].Items.Add(item);
                    Items.RemoveAt(0);

                    InspectionCount++;
                }
            }

            public void ResolveWorryingRound(List<Monkey> monkeys, long lcm)
            {
                var itemCount = Items.Count;
                for (var i = 0; i < itemCount; i++)
                {
                    var item = Items[0];
                    item = WorryEffect(item);
                    item = item % lcm;

                    monkeys[TargetMonkeyCalculator(item)].Items.Add(item);
                    Items.RemoveAt(0);

                    InspectionCount++;
                }
            }

            public long InspectionCount { get; set; } = 0;

            public List<long> Items { get; set; } = new();

            public Func<long, long> WorryEffect { get; set; }

            public Func<long, int> TargetMonkeyCalculator { get; set; }

            public int Divisibility { get; set; }
        }
    }
}
