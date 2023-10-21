using System;
using System.IO;
using System.IO.Compression;
using System.Xml.Linq;

class Program
{
    static void Main(string[] args)
    {
        // Read into an array of strings, skipping the first row.
        string[] source = File.ReadAllLines("FA.csv");
        if (source.Length > 1) // Ensure there's at least one data row.
        {
            // Create the root element for the full XML.
            XElement fullXml = new XElement("DataList",
                new XAttribute("MinCountForAsyncImport", "5"),
                new XAttribute("ExcludeConfigTables", "1"),
                new XAttribute("ProductVersion", ""),
                new XAttribute("PackageName", "FA LINQ"),
                new XAttribute("Code", "FA"),
                new XElement("FixedAssetList",
                    new XElement("TableID", "5600"),
                    new XElement("PageID", "5601")
                )
            );

            // Include the first FixedAsset element.
            XElement firstFixedAsset = new XElement("FixedAsset",
                new XElement("No",
                    new XAttribute("PrimaryKey", "1"),
                    new XAttribute("ValidateField", "1"),
                    new XAttribute("ProcessingOrder", "1"),
                    "TEST"
                ),
                new XElement("Description",
                    new XAttribute("ValidateField", "1"),
                    new XAttribute("ProcessingOrder", "2"),
                    "TEST"
                ),
                new XElement("FAClassCode",
                    new XAttribute("ValidateField", "1"),
                    new XAttribute("ProcessingOrder", "5"),
                    "TANGIBLE"
                ),
                new XElement("FASubclassCode",
                    new XAttribute("ValidateField", "1"),
                    new XAttribute("ProcessingOrder", "6"),
                    "EQUIPMENT"
                ),
                new XElement("FALocationCode",
                    new XAttribute("ValidateField", "1"),
                    new XAttribute("ProcessingOrder", "10"),
                    "NORTH"
                ),
                new XElement("SerialNo",
                    new XAttribute("ValidateField", "1"),
                    new XAttribute("ProcessingOrder", "17"),
                    "12345001"
                ),
                new XElement("FAPostingGroup",
                    new XAttribute("ValidateField", "1"),
                    new XAttribute("ProcessingOrder", "25"),
                    "EQUIPMENT"
                )
            );

            fullXml.Element("FixedAssetList").Add(firstFixedAsset);

            // Add the FixedAsset elements from the CSV.
            var fixedAssetElements = from str in source[1..] // Start reading from the second row (index 1).
                let fields = str.Split(',')
                select new XElement("FixedAsset",
                    new XElement("No", fields[0]),
                    new XElement("Description", fields[1]),
                    new XElement("FAClassCode", fields[2]),
                    new XElement("FASubclassCode", fields[3]),
                    new XElement("FALocationCode", fields[4]),
                    new XElement("SerialNo", fields[5]),
                    new XElement("FAPostingGroup", fields[6])
                );

            // Add the FixedAsset elements to FixedAssetList.
            fullXml.Element("FixedAssetList").Add(fixedAssetElements);

            // Save the full XML to FA.xml.
            XDocument xDocument = new XDocument(fullXml);
            xDocument.Save("FA.xml");

            Console.WriteLine("FA.xml file has been created.");

            // Save the GZip-compressed version.
            using (var fileStream = new FileStream("FA.rapidstart", FileMode.Create))
            using (var gzipStream = new GZipStream(fileStream, CompressionMode.Compress))
            {
                xDocument.Save(gzipStream);
            }

            Console.WriteLine("FA.rapidstart file has been created.");
        }
        else
        {
            Console.WriteLine("No data in the CSV file to process.");
        }
    }
}
