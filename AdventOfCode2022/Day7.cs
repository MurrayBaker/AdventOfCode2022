using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    internal class Day7 : IDay
    {
        public async Task<string> Part1()
        {
            var lines = await File.ReadAllLinesAsync("Day7.txt");

            var rootDirectory = new Folder()
            {
                Name = "/",
                ParentFolder = null,
            };

            var currentDirectory = rootDirectory;

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];

                if (line == "$ ls")
                {
                    i++;

                    line = lines[i];
                    while (!line.StartsWith("$"))
                    {
                        if (line.StartsWith("dir"))
                        {
                            currentDirectory.ChildFolders.Add(new Folder
                            {
                                ParentFolder = currentDirectory,
                                Name = line.Split(" ")[1],
                            });
                        }
                        else
                        {
                            var parts = line.Split(" ");
                            currentDirectory.ChildFiles.Add(new FileRecord(int.Parse(parts[0]), parts[1]));
                        }

                        i++;

                        if (i == lines.Length)
                        {
                            break;
                        }

                        line = lines[i];
                    }

                    i--; // Ugly hack
                    continue;
                }
                
                if (line.StartsWith("$ cd"))
                {
                    var parts = line.Split(" ");
                    var targetDirectory = parts[2];

                    if (targetDirectory == "/")
                    {
                        currentDirectory = rootDirectory;
                    }
                    else if (targetDirectory == "..")
                    {
                        currentDirectory = currentDirectory.ParentFolder;
                    }
                    else
                    {
                        currentDirectory = currentDirectory.ChildFolders.First(f => f.Name == targetDirectory);
                    }
                }
            }

            PopulateSizes(rootDirectory);

            return SumSmallChildren(rootDirectory).ToString();
        }

        public async Task<string> Part2()
        {
            var lines = await File.ReadAllLinesAsync("Day7.txt");

            var rootDirectory = new Folder()
            {
                Name = "/",
                ParentFolder = null,
            };

            var currentDirectory = rootDirectory;

            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];

                if (line == "$ ls")
                {
                    i++;

                    line = lines[i];
                    while (!line.StartsWith("$"))
                    {
                        if (line.StartsWith("dir"))
                        {
                            currentDirectory.ChildFolders.Add(new Folder
                            {
                                ParentFolder = currentDirectory,
                                Name = line.Split(" ")[1],
                            });
                        }
                        else
                        {
                            var parts = line.Split(" ");
                            currentDirectory.ChildFiles.Add(new FileRecord(int.Parse(parts[0]), parts[1]));
                        }

                        i++;

                        if (i == lines.Length)
                        {
                            break;
                        }

                        line = lines[i];
                    }

                    i--; // Ugly hack
                    continue;
                }

                if (line.StartsWith("$ cd"))
                {
                    var parts = line.Split(" ");
                    var targetDirectory = parts[2];

                    if (targetDirectory == "/")
                    {
                        currentDirectory = rootDirectory;
                    }
                    else if (targetDirectory == "..")
                    {
                        currentDirectory = currentDirectory.ParentFolder;
                    }
                    else
                    {
                        currentDirectory = currentDirectory.ChildFolders.First(f => f.Name == targetDirectory);
                    }
                }
            }

            PopulateSizes(rootDirectory);

            var unusedSpace = 70000000 - rootDirectory.Size;

            var desiredSpace = 30000000 - unusedSpace;

            var sizes = new List<(string Name, int Size)>();

            RecordSizes(rootDirectory, sizes);

            return sizes.OrderBy(s => s.Size).First(s => s.Size > desiredSpace).Size.ToString();
        }

        private static void RecordSizes(Folder directory, List<(string Name, int Size)> sizes)
        {
            sizes.Add((directory.Name, directory.Size.Value));

            foreach (var child in directory.ChildFolders)
            {
                RecordSizes(child, sizes);
            }
        }

        private static int PopulateSizes(Folder directory)
        {
            if (!directory.ChildFolders.Any())
            {
                directory.Size = directory.ChildFiles.Sum(f => f.Size);
            }
            else
            {
                directory.Size = directory.ChildFiles.Sum(f => f.Size) + directory.ChildFolders.Sum(f => f.Size ?? PopulateSizes(f));
            }

            return directory.Size.Value;
        }

        private static int SumSmallChildren(Folder directory)
        {
            var totalSize = 0;

            if (directory.Size <= 100000)
            {
                totalSize += directory.Size.Value;
            }

            totalSize += directory.ChildFolders.Sum(SumSmallChildren);

            return totalSize;
        }

        private class Folder
        {
            public string Name { get; set; }

            public List<Folder> ChildFolders { get; set; } = new();

            public List<FileRecord> ChildFiles { get; set; } = new();

            public Folder ParentFolder { get; set; }

            public int? Size { get; set; } = null;
        }

        private record FileRecord(int Size, string Name);
    }
}
