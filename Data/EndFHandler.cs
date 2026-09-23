using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using System.Net.Http;

namespace PeriodicTableConsoleApp.Data
{
    public class EndFHandler
    {
        private readonly string NeutronURL = "https://www-nds.iaea.org/fendl/data/neutron/fendl-FENDL-3.2c-neutron-endf.zip";
        private readonly string ProtonURL = "https://www-nds.iaea.org/fendl/data/proton/fendl-FENDL-3.2c-proton-endf.zip";
        private readonly string DeuteronURL = "https://www-nds.iaea.org/fendl/data/deuteron/fendl-FENDL-3.2c-deuteron-endf.zip";

        private readonly string NeutronZipPath = @"G:\VS Project Dir\CS\PeriodicTableConsoleApp\Data\EndF\Zips\Neutron.zip";
        private readonly string ProtonZipPath = @"G:\VS Project Dir\CS\PeriodicTableConsoleApp\Data\EndF\Zips\Proton.zip";
        private readonly string DeuteronZipPath = @"G:\VS Project Dir\CS\PeriodicTableConsoleApp\Data\EndF\Zips\Deuteron.zip";

        private readonly string NeutronEndFPath = @"G:\VS Project Dir\CS\PeriodicTableConsoleApp\Data\EndF\Neutron\";
        private readonly string ProtonEndFPath = @"G:\VS Project Dir\CS\PeriodicTableConsoleApp\Data\EndF\Proton\";
        private readonly string DeuteronEndFPath = @"G:\VS Project Dir\CS\PeriodicTableConsoleApp\Data\EndF\Deuteron\";

        public long NumberOfFiles, NumberOfLines, NumberOfChars;

        // --- Download/Unzip stuff --- START --- //
        public async Task DownloadZips()
        {
            using HttpClient client = new HttpClient();
            if(File.Exists(NeutronZipPath) && File.Exists(ProtonZipPath) && File.Exists(DeuteronZipPath))
            {
                Console.WriteLine("Files All Exist Already, Not Downloading Them");
            }
            else
            {
                await DownloadZip(client, NeutronURL, NeutronZipPath);
                await DownloadZip(client, ProtonURL, ProtonZipPath);
                await DownloadZip(client, DeuteronURL, DeuteronZipPath);
            }
        }

        private async Task DownloadZip(
            HttpClient client, string url, string outputPath)
        {
            using HttpResponseMessage response =
                await client.GetAsync(url, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            Console.WriteLine($"Downloading: {url}");
            await using Stream input =
                await response.Content.ReadAsStreamAsync();
            await using FileStream output =
                File.Create(outputPath);
            await input.CopyToAsync(output);
        }
        public void ExtractZips()
        {
            ExtractZip(NeutronZipPath, NeutronEndFPath);
            ExtractZip(ProtonZipPath, ProtonEndFPath);
            ExtractZip(DeuteronZipPath, DeuteronEndFPath);
        }
        private void ExtractZip(string zipPath, string outputPath)
        {
            if (EndfFFileExists(outputPath))
            {
                Console.WriteLine($"ZIP: {zipPath}");
                Console.WriteLine($"Is Already Extracted At, {outputPath}");
            }
            else
            {
                ZipFile.ExtractToDirectory(
                zipPath, outputPath, overwriteFiles: true);
            }
        }

        public bool EndfFFileExists(string path)
        {
            return Directory.EnumerateFiles(
                path, "*.endf", SearchOption.AllDirectories).Any();
        }
        // --- Download/Unzip stuff --- END --- //

        public void ConvertInputToOutputFormat(string inputPath)
        {
            // Input = RawInputData.endf
            // Output = EndFModelxxx.json
            int numberOfFiles = 0;
            int numberOfLines = 0;
            int numberOfChars = 0;
            IEnumerable<string> files =
                Directory.EnumerateFiles(inputPath, "*.endf");
            foreach (string file in files)
            {
                foreach (string line in File.ReadLines(file))
                {
                    numberOfLines++;
                    numberOfChars += line.Length;
                }
                numberOfFiles++;
            }
            NumberOfFiles += numberOfFiles;
            NumberOfLines += numberOfLines;
            NumberOfChars += numberOfChars;
        }

    }
}
