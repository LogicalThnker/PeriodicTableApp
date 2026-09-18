using PeriodicTableConsoleApp.Data;
using Newtonsoft.Json;
using PeriodicTableConsoleApp.ClassLib;
using System.Data;

namespace PeriodicTableConsoleApp.Data
{
    public class PubChemJSON
    {
        private string pubChemJsonDir = @"G:\VS Project Dir\CS\PeriodicTableConsoleApp\Assets\PubChemElements_all.json";
        private string nuDatJsonDir = @"G:\VS Project Dir\CS\PeriodicTableConsoleApp\Assets\nndc_nudat_data_export.json";
        private string periodicTableJsonDir = @"G:\VS Project Dir\CS\PeriodicTableConsoleApp\Data\PeriodicTable.json";
        public PubChemData? Data;
        public PeriodicTableList? InMemoryList, IsotopeAddedList;
        public IsotopeList? isotopeList;
        public void ReadJsonData()
        {
            string json = File.ReadAllText(pubChemJsonDir);
            Data = JsonConvert.DeserializeObject<PubChemData>(json);
        }

        public void ReadIntoMemory()
        {
            int count = 0;
            PeriodicTableList outputPeriodicTable = new PeriodicTableList();
            if(Data != null)
            {
                outputPeriodicTable.elements = new Element[Data.Table.Row.Count];
            }
            Element element;
            if(Data != null)
            {
                foreach(PubChemRow row in Data.Table.Row)
                {
                    element = new Element
                    {
                        AtomicNumber = int.Parse(row.Cell[0]),
                        Symbol = row.Cell[1],
                        Name = row.Cell[2],
                        AtomicMass = double.Parse(row.Cell[3]),
                        CPKHexColor = string.IsNullOrEmpty(row.Cell[4])
                        ? null : row.Cell[4],
                        ElectronConfiguration = string.IsNullOrEmpty(row.Cell[5])
                        ? null : row.Cell[5],
                        Electronegativity = string.IsNullOrEmpty(row.Cell[6])
                        ? null : double.Parse(row.Cell[6]),
                        AtomicRadius = string.IsNullOrEmpty(row.Cell[7])
                        ? null : double.Parse(row.Cell[7]),
                        IonizationEnergy = string.IsNullOrEmpty(row.Cell[8])
                        ? null : double.Parse(row.Cell[8]),
                        ElectronAffinity = string.IsNullOrEmpty(row.Cell[9])
                        ? null : double.Parse(row.Cell[9]),
                        OxidationStates = string.IsNullOrEmpty(row.Cell[10])
                        ? null : Array.ConvertAll(
                            row.Cell[10].Split(','),
                            int.Parse),
                        StandardState = string.IsNullOrEmpty(row.Cell[11])
                        ? null : row.Cell[11],
                        MeltingPoint = string.IsNullOrEmpty(row.Cell[12])
                        ? null : double.Parse(row.Cell[12]),
                        BoilingPoint = string.IsNullOrEmpty(row.Cell[13])
                        ? null : double.Parse(row.Cell[13]),
                        Density = string.IsNullOrEmpty(row.Cell[14])
                        ? null : double.Parse(row.Cell[14]),
                        GroupBlock = string.IsNullOrEmpty(row.Cell[15])
                        ? null : row.Cell[15],
                        YearDiscovered = string.IsNullOrEmpty(row.Cell[16])
                        ? null : row.Cell[16]

                    };
                    if (outputPeriodicTable.elements != null)
                    {
                        outputPeriodicTable.elements[count] = element;
                        count++;
                    }
                }
                InMemoryList = outputPeriodicTable;
            }
        }

        public void WriteFromMemoryToFile()
        {
            ReadJsonData();
            ReadIntoMemory();
            if (InMemoryList != null && InMemoryList.elements != null)
            {
                string outputJson = JsonConvert.SerializeObject(
                    InMemoryList, Formatting.Indented);
                File.WriteAllText(
                    periodicTableJsonDir,
                    outputJson);
            }
            else
            {
                Console.WriteLine("In Memory List is null.");
            }
        }
        public void WriteIsotopesAddedList()
        {
            if (IsotopeAddedList != null)
            {
                string outputJson = JsonConvert.SerializeObject(
                    IsotopeAddedList, Formatting.Indented);
                File.WriteAllText(
                    periodicTableJsonDir,
                    outputJson);
            }
        }
        public void AddIsotopes()
        {
            // if both the json for the pre-isotope layer (or the post one), and the nudat json import with isotopes exists.
            if (File.Exists(periodicTableJsonDir) && File.Exists(nuDatJsonDir))
            {
                Console.WriteLine("Periodic Table Json Exists.");
                Console.WriteLine("Nudat Json Exists.");
                string json = File.ReadAllText(periodicTableJsonDir);
                IsotopeAddedList = JsonConvert.DeserializeObject<PeriodicTableList>(json);
                int elementCounter = 1;
                if (IsotopeAddedList != null)
                {
                    foreach (Element element in IsotopeAddedList.elements)
                    {
                        Console.WriteLine(elementCounter + " : " + element.Name);
                        element.Isotopes = isotopeList.IsotopeBuckets[element.AtomicNumber].ToArray();
                        elementCounter++;
                    }
                }
                else
                {
                    Console.WriteLine("IsotopeAddedList == null");
                }
            }
            else
            {
                Console.WriteLine("Neither Periodic Table or Nudat Json exists. Or just one doesn't.");
            }
        }

        public void PopulateIsotopeBuckets()
        {
            string json = File.ReadAllText(nuDatJsonDir);
            Dictionary<string, Isotope>? isotopeRawData =
                JsonConvert.DeserializeObject<Dictionary<string, Isotope>>(json);
            isotopeList = new IsotopeList
            {
                IsotopeBuckets = new Dictionary<int, List<Isotope>>()
            };

            if (isotopeList != null && isotopeRawData != null)
            {
                foreach (Isotope isotope in isotopeRawData.Values)
                {
                    int atomicNumber = isotope.z;
                    if (atomicNumber < 1)
                        continue;
                    if (!isotopeList.IsotopeBuckets.ContainsKey(atomicNumber))
                    {
                        isotopeList.IsotopeBuckets[atomicNumber] = new List<Isotope>();
                    }
                    isotopeList.IsotopeBuckets[atomicNumber].Add(isotope);
                }
            }
            else
            {
                Console.WriteLine("IsotopeList == null");
            }
            
        }
    }
}
