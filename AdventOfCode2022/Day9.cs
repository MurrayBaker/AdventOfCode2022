using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day9 : IDay
    {
        public async Task<string> Part1()
        {
            var lines = await File.ReadAllLinesAsync("Day9.txt");

            var headX = 0;
            var headY = 0;
            var tailX = 0;
            var tailY = 0;

            var tailPositions = new HashSet<(int, int)>();

            foreach (var line in lines)
            {
                var parts = line.Split(" ");

                var direction = parts[0];
                var moveCount = int.Parse(parts[1]);

                for (int i = 0; i < moveCount; i++)
                {
                    if (direction == "U")
                    {
                        headY += 1;
                    }
                    else if (direction == "D")
                    {
                        headY -= 1;
                    }
                    else if (direction == "L")
                    {
                        headX -= 1;
                    }
                    else
                    {
                        headX += 1;
                    }

                    (tailX, tailY) = ResolveTailPosition(headX, headY, tailX, tailY);
                    tailPositions.Add((tailX, tailY));
                }
            }

            return tailPositions.Count.ToString();
        }

        private static (int, int) ResolveTailPosition(int headX, int headY, int tailX, int tailY)
        {
            if (headX == tailX && headY == tailY)
            {
                return (headX, headY);
            }

            if (headX == tailX)
            {
                if (tailY - headY > 1)
                {
                    tailY -= 1;
                }
                else if (tailY - headY < -1)
                {
                    tailY += 1;
                }
            }
            else if (headY == tailY)
            {
                if (tailX - headX > 1)
                {
                    tailX -= 1;
                }
                else if (tailX - headX < -1)
                {
                    tailX += 1;
                }
            }
            else
            {
                if (Math.Abs(tailX - headX) > 1 || Math.Abs(tailY - headY) > 1)
                {
                    tailX += tailX > headX ? -1 : 1;
                    tailY += tailY > headY ? -1 : 1;
                }
            }

            return (tailX, tailY);
        }

        public async Task<string> Part2()
        {
            var lines = await File.ReadAllLinesAsync("Day9.txt");

            List<(int X, int Y)> snake = Enumerable.Repeat((0, 0), 10).ToList();

            var tailPositions = new HashSet<(int, int)>();

            foreach (var line in lines)
            {
                var parts = line.Split(" ");

                var direction = parts[0];
                var moveCount = int.Parse(parts[1]);

                for (int i = 0; i < moveCount; i++)
                {
                    if (direction == "U")
                    {
                        snake[0] = (snake[0].X, snake[0].Y + 1);
                    }
                    else if (direction == "D")
                    {
                        snake[0] = (snake[0].X, snake[0].Y - 1);
                    }
                    else if (direction == "L")
                    {
                        snake[0] = (snake[0].X - 1, snake[0].Y);
                    }
                    else
                    {
                        snake[0] = (snake[0].X + 1, snake[0].Y);
                    }

                    for (int j = 0; j < 9; j++)
                    {
                        var newPiece = ResolveTailPosition(snake[j].X, snake[j].Y, snake[j + 1].X, snake[j + 1].Y);
                        snake[j + 1] = newPiece;
                    }

                    tailPositions.Add(snake[9]);
                }
            }

            return tailPositions.Count.ToString();
        }
    }
}
