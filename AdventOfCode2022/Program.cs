namespace AdventOfCode2022
{
    public class Program
    {
        public async static Task Main(string[] args)
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

            Console.WriteLine(totals.Max());
            Console.WriteLine(totals.OrderByDescending(t => t).Take(3).Sum());
            Console.Read();
        }
    }
}