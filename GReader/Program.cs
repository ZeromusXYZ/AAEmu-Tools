using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace GReader
{
    class Program
    {
        static string GFileName = string.Empty;
        static Dictionary<string, string> ValueFields = new Dictionary<string, string>();

        static private bool HandleCommandLine()
        {
            var args = Environment.GetCommandLineArgs();
            if (args.Length <= 1)
            {
                Console.WriteLine("[INFO] GReader <filename>");
            }
            for (int i = 1; i < args.Length; i++)
            {
                if (File.Exists(args[i]))
                    GFileName = args[i];
            }
            return (GFileName != string.Empty);
        }

        static Dictionary<string, string> ConvertGFileStringList(List<string> lines)
        {
            var res = new Dictionary<string, string>();
            var currentObjectName = string.Empty;

            foreach (var l in lines)
            {
                if (l.Trim() == string.Empty)
                    continue;
                if (!l.StartsWith("  "))
                {
                    currentObjectName = l.Trim().ToLower();
                    Console.WriteLine(currentObjectName);
                }
                else
                {
                    var words = l.Split(' ').ToList();
                    for (var i = 0; i < words.Count; i++)
                        words[i] = words[i].Trim();
                    var containedValues = string.Empty;
                    var remainderString = string.Join(" ", words.GetRange(1, words.Count - 1)).Replace(',', ' ').Split(' ').ToList();
                    for (var i = 0; i < remainderString.Count; i++)
                        remainderString[i] = remainderString[i].Replace(',', ' ').Trim();
                    var remainder = new List<string>();
                    foreach (var s in remainderString)
                        if (s.Trim() != string.Empty)
                            remainder.Add(s.Trim());
                    var para = new List<string>();
                    if ((remainder.Count > 2) && (remainder[1] == "(") && (remainder[remainder.Count - 1] == ")"))
                    {
                        for (var i = 2; i < remainder.Count - 1; i++)
                            para.Add(remainder[i]);
                    }
                    var varName = remainder.Count > 1 ? remainder[0].ToLower() : string.Empty;

                    switch (varName)
                    {
                        case "coords":
                            if (para.Count == 4)
                            {
                                res.Add(currentObjectName + "." + varName + ".x", para[0]);
                                res.Add(currentObjectName + "." + varName + ".y", para[1]);
                                res.Add(currentObjectName + "." + varName + ".w", para[2]);
                                res.Add(currentObjectName + "." + varName + ".h", para[3]);
                            }
                            else
                            {
                                Console.Error.WriteLine("Invalid number of parameters for " + currentObjectName + ".coords");
                            }
                            break;
                        case "offset":
                            if (para.Count == 2)
                            {
                                res.Add(currentObjectName + "." + varName + ".x", para[0]);
                                res.Add(currentObjectName + "." + varName + ".y", para[1]);
                            }
                            else
                            {
                                Console.Error.WriteLine("Invalid number of parameters for " + currentObjectName + ".offset");
                            }
                            break;
                        default:
                            Console.Error.WriteLine("Unknown variable name " + varName + " for " + currentObjectName);
                            break;
                    }
                }

            }
            return res;
        }

        static void Main(string[] args)
        {
            if (HandleCommandLine())
            {
                try
                {
                    var vs = ConvertGFileStringList(File.ReadAllLines(GFileName).ToList());

                    foreach (var vk in vs)
                        ValueFields.Add(vk.Key, vk.Value);

                    foreach (var f in ValueFields)
                    {
                        Console.WriteLine(f.Key + " => " + f.Value);
                    }
                    Console.ReadLine();
                }
                catch (Exception x)
                {
                    Console.Error.WriteLine("Exception: " + x.Message);
                }
            }

        }
    }
}
