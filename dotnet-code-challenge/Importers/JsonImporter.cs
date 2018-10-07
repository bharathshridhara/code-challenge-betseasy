using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace dotnet_code_challenge.Importers
{
    public class JsonImporter : IImporter
    {
        private string _filename = "";

        public JsonImporter()
        {
            _filename = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\FeedData\\Wolferhampton_Race1.json";
        }

        public JsonImporter(string fileName)
        {
            _filename = fileName;
        }

        public IEnumerable<Horse> Import()
        {
            var horses = new List<Horse>();
            try
            {
                if (!File.Exists(_filename))
                    throw new Exception("No file found at the location");

                dynamic output = JsonConvert.DeserializeObject(File.ReadAllText(_filename));
                
                var data = output["RawData"];
                data = data["Participants"];


                

                foreach (var item in data)
                {
                    var horse = new Horse
                    {
                        Name = item["Name"],
                        Number = item["Tags"]["Number"],

                    };
                    horses.Add(horse);
                }

                data = output["RawData"]["Markets"][0]["Selections"];

                foreach(var item in data)
                {
                    var tag = item["Tags"];
                    var number = tag["participant"].ToString();

                    horses.Where(h => h.Number == int.Parse(number)).FirstOrDefault().Price =
                        float.Parse(item["Price"].ToString());
                }

                return horses;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading JSON file. Please check for correctness of the JSON file.");
                return horses;
            }
        }
    }
}
