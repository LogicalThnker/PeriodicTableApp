using Newtonsoft.Json;

namespace PeriodicTableConsoleApp.Data
{
    public class PubChemData
    {
        public required PubChemTable Table { get; init; }
    }

    public class PubChemTable
    {
        public required PubChemColumns Columns { get; init; }
        public required List<PubChemRow> Row {  get; init; }
    }

    public class PubChemColumns
    {
        public required List<string> Column {  get; init; }
    }

    public class PubChemRow
    {
        public required List<string> Cell { get; init; }
    }
}
