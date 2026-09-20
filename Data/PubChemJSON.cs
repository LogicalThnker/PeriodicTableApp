using Newtonsoft.Json;
using PeriodicTableConsoleApp.ClassLib;
using PeriodicTableConsoleApp.Data;
using System.ComponentModel.Design;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace PeriodicTableConsoleApp.Data
{
    public class PubChemJSON
    {
        private string? pcJSON, ndJSON;
        private readonly string periodicTableJsonDir = @"G:\VS Project Dir\CS\PeriodicTableConsoleApp\Assets\PeriodicTable.json";
        private readonly string pubChemJsonDir = @"G:\VS Project Dir\CS\PeriodicTableConsoleApp\Assets\PubChemJson.json";
        private readonly string nuDatJsonDir = @"G:\VS Project Dir\CS\PeriodicTableConsoleApp\Assets\NuDatJson.json";
        public PubChemData? pcData;
        public IsotopeList? ndData;
        public PeriodicTableList? InMemoryList;
        public NuDatData? isotopeRawData;

        public bool allDebug = false;
        
        public bool NuDatBool, PubChemBool, NeedsUpdate;
        private readonly HttpClient client = new HttpClient();
        public async Task GetPubChemData()
        {
            int attempts = 0;
            int maxAttempts = 1;
            if (!allDebug)
            {
                while (pcData == null && attempts < maxAttempts)
                {
                    HttpResponseMessage response = await client.GetAsync("https://pubchem.ncbi.nlm.nih.gov/rest/pug/periodictable/JSON");
                    if (response.IsSuccessStatusCode)
                    {
                        pcJSON = await response.Content.ReadAsStringAsync();
                        pcData = JsonConvert.DeserializeObject<PubChemData>(pcJSON);
                        string pcJsonOutput = pcJSON;
                        File.WriteAllText(pubChemJsonDir, pcJsonOutput);
                        Console.WriteLine("PubChemJson.json created");
                    }
                    else
                    {
                        Console.WriteLine("PubChem failed.");
                        Console.WriteLine($"Status Code: {(int)response.StatusCode}");
                        Console.WriteLine($"Status: {response.StatusCode}");
                        string errorContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Response: {errorContent}");
                        await Task.Delay(5000);
                    }
                    attempts++;
                }
            }
            else
            {
                while (pcData == null && attempts < maxAttempts)
                {
                    pcJSON = File.ReadAllText(pubChemJsonDir);
                    pcData = JsonConvert.DeserializeObject<PubChemData>(pcJSON);
                    attempts++;
                }
            }
        }
        public async Task GetNuDatData()
        {
            int attempts = 0;
            int maxAttempts = 1;
            if (!allDebug)
            {
                while (isotopeRawData == null && attempts < maxAttempts)
                {
                    HttpResponseMessage response = await client.GetAsync("https://www.nndc.bnl.gov/nudat3/data/output.json");
                    if (response.IsSuccessStatusCode)
                    {
                        ndJSON = await response.Content.ReadAsStringAsync();
                        isotopeRawData = JsonConvert.DeserializeObject<NuDatData>(ndJSON);
                        string ndJsonOutput = ndJSON;
                        File.WriteAllText(nuDatJsonDir, ndJsonOutput);
                        Console.WriteLine("NuDatJson.json created.");
                    }
                    else
                    {
                        Console.WriteLine("NuDat failed.");
                        Console.WriteLine($"Status Code: {(int)response.StatusCode}");
                        Console.WriteLine($"Status: {response.StatusCode}");
                        string errorContent = await response.Content.ReadAsStringAsync();
                        Console.WriteLine($"Response: {errorContent}");
                    }
                    attempts++;
                }
            }
            else
            {
                while(ndData == null && attempts < maxAttempts)
                {
                    ndJSON = File.ReadAllText(nuDatJsonDir);
                    isotopeRawData = JsonConvert.DeserializeObject<NuDatData>(ndJSON);
                    attempts++;
                }
            }
        }

        public async Task PutTogether()
        {
            if (NuDatBool && PubChemBool)
            {
                await GetPubChemData();
                await GetNuDatData();
                await InMemoryListPopulate(NuDatBool, PubChemBool);
            }
        }

        public void DumpAllData()
        {
            
        }

        private async Task InMemoryListPopulate(bool NuDatB, bool PubChemB)
        {
            if(NuDatB && PubChemB)
            {
                await PubChemReadIntoMemory();
                await PopulateIsotopeBuckets();
                await AddIsotopes();
                await WriteFromMemoryToFile();
                Console.WriteLine("Tasks are 'done'");
            }
        }

        public async Task PubChemReadIntoMemory()
        {
            await Task.Run(() => {
                int count = 0;
                PeriodicTableList outputPeriodicTable = new PeriodicTableList();
                if (pcData != null)
                {
                    outputPeriodicTable.elements = new Element[pcData.Table.Row.Count];
                }
                Element element;
                if (pcData != null)
                {
                    Console.WriteLine("pcData != null");
                    foreach (PubChemRow row in pcData.Table.Row)
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
                else
                {
                    Console.WriteLine("pcData == null");
                }
            });
            
        }

        public async Task WriteFromMemoryToFile()
        {
            await Task.Run(() =>
            {
                try
                {
                    if (InMemoryList != null && InMemoryList.elements != null)
                    {
                        string outputJson = JsonConvert.SerializeObject(
                            InMemoryList, Formatting.Indented,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });
                        File.WriteAllText(
                            periodicTableJsonDir,
                            outputJson);
                    }
                    else
                    {
                        Console.WriteLine("In Memory List is null.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            });
        }
        public async Task PopulateIsotopeBuckets()
        {
            await Task.Run(() =>
            {
                try
                {
                    ndData = new IsotopeList
                    {
                        IsotopeBuckets = new Dictionary<int, List<Isotope>>()
                    };

                    if (ndData != null && isotopeRawData?.nuclides != null)
                    {
                        foreach (Isotope isotope in isotopeRawData.nuclides)
                        {
                            int atomicNumber = isotope.z;
                            if (atomicNumber < 1)
                                continue;
                            if (!ndData.IsotopeBuckets.ContainsKey(atomicNumber))
                            {
                                ndData.IsotopeBuckets[atomicNumber] = new List<Isotope>();
                            }
                            ndData.IsotopeBuckets[atomicNumber].Add(isotope);
                        }
                    }
                    else
                    {
                        Console.WriteLine("IsotopeList == null");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            });
        }
        public async Task AddIsotopes()
        {
            await Task.Run(() =>
            {
                try
                {
                    if (periodicTableJsonDir != null && ndJSON != null)
                    {
                        if (InMemoryList != null)
                        {
                            foreach(Element element in InMemoryList.elements)
                            {
                                element.Isotopes = ndData.IsotopeBuckets[element.AtomicNumber].ToArray();
                            }
                        }
                        else
                        {
                            Console.WriteLine("InMemoryList or IsotopeBuckets == null");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Neither Periodic Table or Nudat Json exists. Or just one doesn't.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            });
        }
    }
}
