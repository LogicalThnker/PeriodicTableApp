using PeriodicTableConsoleApp.Data;
using Newtonsoft.Json;
using PeriodicTableConsoleApp.ClassLib;

namespace PeriodicTableConsoleApp.Data
{
    public class PubChemJSON
    {
        private string jsonDir = @"G:\VS Project Dir\CS\PeriodicTableConsoleApp\Assets\PubchemJSON.json";
        public PubChemData? Data;
        public PeriodicTableList? InMemoryList;
        public void ReadJsonData()
        {
            string json = File.ReadAllText(jsonDir);
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
                    @"G:\VS Project Dir\CS\PeriodicTableConsoleApp\Data\PeriodicTable.json",
                    outputJson);
            }
            else
            {
                Console.WriteLine("In Memory List is null.");
            }
        }
    }
}
