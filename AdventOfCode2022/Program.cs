namespace AdventOfCode2022
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            Console.WriteLine("Enter day");

            if (!int.TryParse(Console.ReadLine(), out var day))
            {
                Console.WriteLine("Enter an integer day");
                Console.Read();
                return;
            }

            Console.WriteLine("Enter part; 1 or 2");

            if (!int.TryParse(Console.ReadLine(), out var part) && part > 2 && part < 1)
            {
                Console.WriteLine("Enter a part; 1 or 2");
                Console.Read();
                return;
            }

            IDay dayCalculator = day switch
            {
                1 => new Day1(),
                2 => new Day2(),
                3 => new Day3(),
                4 => new Day4(),
                5 => new Day5(),
                6 => new Day6(),
                7 => new Day7(),
                _ => throw new NotImplementedException(),
            };

            Console.WriteLine(part == 1
                ? await dayCalculator.Part1()
                : await dayCalculator.Part2());

            Console.Read();
        }
    }
}