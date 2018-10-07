using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Xml.Serialization;

namespace dotnet_code_challenge.Importers
{
    public class XMLImporter : IImporter
    {
        private string _fileName = "";

        public XMLImporter(string fileName)
        {
            _fileName = fileName;
        }

        public XMLImporter()
        {
            _fileName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "\\FeedData\\Caulfield_Race1.xml";
        }

        public IEnumerable<Horse> Import()
        {
            var horses = new List<Horse>();

            try
            {
                if (!File.Exists(_fileName))
                    throw new Exception("No file found at the location");

                var xml = XDocument.Load(_fileName);

                var query = xml.Descendants("horses").Elements("horse").Where(e => !e.Attributes("Price").Any());
                var prices = new List<HorsePrice>();

                if (query == null)
                    throw new Exception("No horse information found");

                foreach (var item in query)
                {
                    if (item.Elements("trainer").Any())
                    {
                        var horse = new Horse
                        {
                            Name = item.Attribute("name").Value,
                            Country = item.Attribute("country").Value,
                            Number = int.Parse(item.Element("number").Value)
                        };
                        horses.Add(horse);
                    }
                }

                var priceQuery = xml.Descendants("horses").Elements("horse").Where(e => e.Attributes("Price").Any());
                
                foreach(var item in priceQuery)
                {
                    horses.Where(h => h.Number == int.Parse(item.Attribute("number").Value)).FirstOrDefault().Price =
                        float.Parse(item.Attribute("Price").Value);
                }

                return horses;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error reading the XML file. Please check for correctness of the JSON file.");
                return horses;
            }
            
        }
    }
}
