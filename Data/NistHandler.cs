using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.Http;

namespace PeriodicTableConsoleApp.Data
{
    public class NistHandler
    {
        private readonly string databaseLink = @"https://physics.nist.gov/cgi-bin/ASD/ie.pl?spectra=Fe&submit=Retrieve+Data&units=1&format=1&remove_js=on&order=0&at_num_out=on&sp_name_out=on&ion_charge_out=on&el_name_out=on&seq_out=on&shells_out=on&conf_out=on&level_out=on&ion_conf_out=on&e_out=0&unc_out=on&biblio=on";
        private readonly string databaseLinkStart = @"https://physics.nist.gov/cgi-bin/ASD/ie.pl?spectra=";
        private readonly string databaseLinkEnd = @"&submit=Retrieve+Data&units=1&format=1&remove_js=on&order=0&at_num_out=on&sp_name_out=on&ion_charge_out=on&el_name_out=on&seq_out=on&shells_out=on&conf_out=on&level_out=on&ion_conf_out=on&e_out=0&unc_out=on&biblio=on";
        private readonly string outputDir = "";

        public async Task Interact()
        {
            using HttpClient client = new HttpClient();
            await GetDataOutput(client, databaseLink, outputDir, "H");
        }

        private async Task GetDataOutput(HttpClient client, string url, string outputPath, string elementSymbol)
        {
            string finalUrl = databaseLinkStart + elementSymbol + databaseLinkEnd;
            using HttpResponseMessage response =
                await client.GetAsync(finalUrl, HttpCompletionOption.ResponseHeadersRead);
            response.EnsureSuccessStatusCode();
            Console.WriteLine($"Downloading: {elementSymbol}");
            //await using Stream input =
            //    await response.Content.ReadAsStreamAsync();
            string content = await response.Content.ReadAsStringAsync();
            int start = content.IndexOf("<pre>") + "<pre>".Length;
            int end = content.IndexOf("</pre>", start);
            //string formattedContent = content.Substring(start, (end - start));
            Console.WriteLine(content);
            //await using FileStream output =
            //    File.Create(outputPath);
            //await input.CopyToAsync(output);
        }

    }
}
