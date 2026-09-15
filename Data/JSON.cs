using PeriodicTableConsoleApp.Data;
using Newtonsoft.Json;

namespace PeriodicTableConsoleApp.Data
{
    public class JSON
    {
        private string jsonDir = @"G:\VS Project Dir\CS\PeriodicTableConsoleApp\Assets\PubchemJSON.json";
        public PubChemData? Data;
        public void ReadJsonData()
        {
            string json = File.ReadAllText(jsonDir);
            Data = JsonConvert.DeserializeObject<PubChemData>(json);
        }
        public void OutputAllData()
        {
            foreach(PubChemRow row in Data.Table.Row)
            {
                for(int i = 0; i < row.Cell.Count; i++)
                {
                    string column = Data.Table.Columns.Column[i];
                    string value = row.Cell[i];
                    Console.WriteLine(column + " : " + row.Cell[i]);
                }
            }
        }
    }
}
