using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dir_Sync
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string dir1 = args[0];
            string dir2 = args[1];

            var result1 = Get(dir1);
            var result2 = Get(dir2);

            Console.WriteLine(args[0]);
            Console.WriteLine(args[1]);

            var dirsToRemove = result2.dirs.Where(x => !result1.dirs.Contains(x));
            var dirsToAdd = result1.dirs.Where(x=> !result2.dirs.Contains(x));
            var filesToRemove = result2.files.Where(x => !result1.files.Contains(x));
            var filesToAdd = result1.files.Where(x => !result2.files.Contains(x));

            foreach (var fileToRemove in filesToRemove)
                Console.WriteLine("Deleting : " + Path.Combine(dir2, fileToRemove.file));

            foreach (var dirToRemove in dirsToRemove)
                Console.WriteLine("Deleting : " + Path.Combine(dir2, dirToRemove));

            foreach (var dirToAdd in dirsToAdd)
                Console.WriteLine("Adding : " + Path.Combine(dir2, dirToAdd));
            
            foreach (var fileToAdd in filesToAdd)
                Console.WriteLine("Adding : " + Path.Combine(dir2, fileToAdd.file));

            Console.WriteLine("Press enter to close...");
            Console.ReadLine();
        }

        public static (HashSet<string> dirs, HashSet<(string file, long tick)> files) Get(string dir)
        {
            string CleanPath(string path) => path.Substring(dir.Length).TrimStart('/', '\\');

            // replace this with a hashing method if you need
            long GetUnique(string path) => new FileInfo(path).LastWriteTime.Ticks;

            var directories = Directory.GetDirectories(dir, "*.*", SearchOption.AllDirectories);
            directories = AddElementAtBeggining(dir, directories);

            var dirHash = directories.Select(CleanPath).ToHashSet();

            // this could be paralleled if need be (if using a hash) 
            var fileHash = directories.SelectMany(Directory.EnumerateFiles)
               .Select(file => (name: CleanPath(file), ticks: GetUnique(file)))
               .ToHashSet();

            return (dirHash, fileHash);
        }

        private static string[] AddElementAtBeggining(string dir, string[] directories)
        {
            int newLength = directories.Length + 1;

            string[] result = new string[newLength];

            result[0] = dir;
            
            for (int i = 0; i < directories.Length; i++)
                result[i+1] = directories[i];

            return result;
        }
    }
}
