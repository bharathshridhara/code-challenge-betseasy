using dotnet_code_challenge.Importers;
using System;
using System.Collections.Generic;
using System.Linq;

namespace dotnet_code_challenge
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Select your option");
            Console.WriteLine("1: Read from XML");
            Console.WriteLine("2: Read from JSON");
            var key = Console.ReadKey(false);

            IEnumerable<Horse> horses = new List<Horse>();
            IImporter importer;

            while(key.Key == ConsoleKey.D1 || key.Key == ConsoleKey.D2)
            {
                if(key.Key == ConsoleKey.D1)
                {
                    importer = new XMLImporter();
                    horses = importer.Import();
                    break;
                }
                else
                {
                    importer = new JsonImporter();
                    horses = importer.Import();
                    break;
                }
            }

            //Console.WriteLine("Press any key to read from XML file and display horses");

            Console.ReadKey();

            Console.WriteLine("Now printing horses...");

            //Ascending order of horses by price
            horses = horses.OrderBy(h => h.Price);

            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("Ordering horses by ascending order of their prices.");
            Console.WriteLine("Horse Name | Price");

            foreach(var horse in horses)
                Console.WriteLine(horse.Name + " | " + horse.Price);


            Console.WriteLine("Press any key to end");
            Console.ReadKey();

        }
    }
}
