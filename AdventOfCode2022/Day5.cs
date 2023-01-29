using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day5 : IDay
    {
        public async Task<string> Part1()
        {
            var crates = GetCrates();
            var lines = await File.ReadAllLinesAsync("Day5.txt");
            
            foreach (var line in lines)
            {
                var (MoveThisMany, From, To) = ParseInstruction(line);

                for (int i = 0; i < MoveThisMany; i++)
                {
                    crates[To] = crates[To].Prepend(Pop(crates[From])).ToList();
                }
            }

            return new string(crates.Select(stack => stack.FirstOrDefault()).ToArray());
        }

        public async Task<string> Part2()
        {
            var crates = GetCrates();
            var lines = await File.ReadAllLinesAsync("Day5.txt");

            foreach (var line in lines)
            {
                var (MoveThisMany, From, To) = ParseInstruction(line);

                var cratesPickedUp = new List<char>();

                for (int i = 0; i < MoveThisMany; i++)
                {
                    cratesPickedUp.Add(Pop(crates[From]));
                }

                cratesPickedUp.AddRange(crates[To]);
                crates[To] = cratesPickedUp;
            }

            return new string(crates.Select(stack => stack.FirstOrDefault()).ToArray());
        }

        private static T Pop<T>(List<T> input)
        {
            var first = input.First();
            input.RemoveAt(0);
            return first;
        }

        private static (int MoveThisMany, int From, int To) ParseInstruction(string input)
        {
            var numbers = input
                .Replace("move", "")
                .Replace("from", "")
                .Replace("to", "")
                .Split(" ", StringSplitOptions.RemoveEmptyEntries);

            return (int.Parse(numbers[0]), int.Parse(numbers[1]), int.Parse(numbers[2]));
        }

        /*  [T]             [P]     [J]        
            [F]     [S]     [T]     [R]     [B]
            [V]     [M] [H] [S]     [F]     [R]
            [Z]     [P] [Q] [B]     [S] [W] [P]
            [C]     [Q] [R] [D] [Z] [N] [H] [Q]
            [W] [B] [T] [F] [L] [T] [M] [F] [T]
            [S] [R] [Z] [V] [G] [R] [Q] [N] [Z]
            [Q] [Q] [B] [D] [J] [W] [H] [R] [J]
             1   2   3   4   5   6   7   8   9 
         */

        private static List<char>[] GetCrates()
            => new List<char>[]
            {
                new List<char>(), // So that I don't have to -1 all the indices
                new List<char>() { 'T', 'F', 'V', 'Z', 'C', 'W', 'S', 'Q' },
                new List<char>() { 'B', 'R', 'Q' },
                new List<char>() { 'S', 'M', 'P', 'Q', 'T', 'Z', 'B' },
                new List<char>() { 'H', 'Q', 'R', 'F', 'V', 'D' },
                new List<char>() { 'P', 'T', 'S', 'B', 'D', 'L', 'G', 'J' },
                new List<char>() { 'Z', 'T', 'R', 'W' },
                new List<char>() { 'J', 'R', 'F', 'S', 'N', 'M', 'Q', 'H' },
                new List<char>() { 'W', 'H', 'F', 'N', 'R' },
                new List<char>() { 'B', 'R', 'P', 'Q', 'T', 'Z', 'J' },
            };
    }
}