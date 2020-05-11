using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using HostFileEditor.Models;

namespace HostFileEditor.Utils
{
    public class HostFileIo
    {
        // Will move this to config file

        public static IEnumerable<HostFileElement> ReadHostFile()
        {
            var lines = File.ReadAllLines(ConfigurationManager.AppSettings["HostFileLocation"]);

            var list = new List<HostFileElement>();
            for (int i = 0; i < lines.Length; i++)
            {
                var magic = lines[i].ReadStringToChar('#').Trim();
                if (string.IsNullOrWhiteSpace(magic))
                {
                    continue;
                }

                var parts = magic.Replace("\t", " ").Split(' ');
                list.Add(new HostFileElement
                {
                    LineId = i + 1,
                    Domain = parts[1].Trim(),
                    Ip = parts[0].Trim(),
                    Guid = Guid.NewGuid()
                });
            }

            return list;
        }

        public static void SaveHostFile(IEnumerable<HostFileElement> elements)
        {
            // Will add validation later for now just checking white space but should verify domains are domains
            // not sure if to do that here or at viewer level or viewer control level
            var validElementsToSave = elements.Where(f => string.IsNullOrWhiteSpace(f.Domain) == false && string.IsNullOrWhiteSpace(f.Ip) == false).ToArray();

            var lines = File.ReadAllLines(ConfigurationManager.AppSettings["HostFileLocation"]);

            var outPutLines = new List<string>();
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i];
                if(string.IsNullOrWhiteSpace(line) && string.IsNullOrWhiteSpace(outPutLines.LastOrDefault())){continue;}
                var magic = line.ReadStringToChar('#').Trim();
                if (!string.IsNullOrWhiteSpace(magic))
                {
                    var rest = line.Substring(magic.Length);

                    var replaced = validElementsToSave.FirstOrDefault(f => f.LineId == i + 1);
                    if (replaced == null)
                    {
                        continue;
                    }

                    outPutLines.Add($"{replaced.Ip}\t{replaced.Domain}{rest}");
                    continue;
                }

                outPutLines.Add(line);
            }

            outPutLines.AddRange(validElementsToSave.Where(f => f.LineId == 0).Select(f => $"{f.Ip}\t{f.Domain}"));
            File.WriteAllText(ConfigurationManager.AppSettings["HostFileLocation"], string.Join(Environment.NewLine, outPutLines), Encoding.Default);
        }
    }
}