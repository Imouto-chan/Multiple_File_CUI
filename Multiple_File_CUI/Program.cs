using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;

namespace Multiple_File_CUI
{
    internal class Multiple_File_CUI
    {
        private static void Help()
        {
            Console.WriteLine("Runs multiple files in a given input folder (optionally including subfolders) including arguments through a executable file. Places output in given output path, keeping folder structure. " +
                            "Optionally allows you to set a specific/minimum/maximum image resolution/dimension to only run through the given executable.\n" +
                            "REQUIRES System.Drawing.Common(System.Drawing.Common.dll) for image resolution options mentioned below.\n" +
                            "Arguments:\n" +
                            "1\t-\tPath to Executable file\n" +
                            "2\t-\tPath to INPUT FILES\n" +
                            "3\t-\tPath to OUTPUT FOLDER\n" +
                            "4\t-\tY/N run on subfolders\n" +
                            "5..n\t-\tArguments\n" +
                            "\t\tTo include INPUT FILES use [i] where they will be subsituted into\n" +
                            "\t\tTo include OUTPUT FILES use [o] where they will be subsituted into\n" +
                            "\t\tTo set a specific/minimum/maximum image resolution, use set[128x128]/min[128x128]/max[128x128] respectively (in this case the reoslution given is 128 width and 128 height)\n" +
                            "\t\t(include - or / in front of argument if needed)\n\n" +
                            "\t\texample: .\\Multiple_File_CUI.exe .\\example.exe .\\input .\\output N -i [i] -o [o]\n");
        }

        private static void Main(string[] args)
        {
            /* Arguments:
             * args[0] - executable
             * args[1] - input path
             * args[2] - output path
             * args[3] - subfolder option
             * args[4..n] - arguments
             */
            try
            {
                if (args[0].ToLower().Equals("help"))
                {
                    Help();
                    return;
                }

                Regex argsRegex = new Regex(@"(set|min|max)\[([0-9]+)x([0-9]+)\]", RegexOptions.IgnoreCase);

                string arguments = "";
                int imgWidth = 0, imgHeight = 0;

                for (int i = 4; i < args.Length; i++)
                    arguments += " " + args[i];

                Match m = argsRegex.Match(arguments);

                if (m.Success)
                {
                    imgWidth = Int32.Parse(m.Groups[2].ToString());
                    imgHeight = Int32.Parse(m.Groups[3].ToString());
                    arguments = argsRegex.Replace(arguments, "");
                }

                string[] inputFiles = (args[3].ToLower()[0] == 'y') ? Directory.GetFiles(args[1], "*.*", SearchOption.AllDirectories) : Directory.GetFiles(args[1], "*.*", SearchOption.TopDirectoryOnly);

                foreach (string inputPath in inputFiles)
                {
                    if (m.Success)
                    {
                        Image img = Image.FromFile(inputPath);
                        switch (m.Groups[1].ToString().ToLower()) //Checking image resolution
                        {
                            case "set":
                                if (img.Width != imgWidth || img.Height != imgHeight)
                                    continue;
                                break;
                            case "min":
                                if (img.Width < imgWidth || img.Height < imgHeight)
                                    continue;
                                break;
                            case "max":
                                if (img.Width > imgWidth || img.Height > imgHeight)
                                    continue;
                                break;
                            default:
                                continue;
                        }
                    }

                    System.Diagnostics.Process p = new System.Diagnostics.Process();
                    p.StartInfo.FileName = args[0];
                    p.StartInfo.UseShellExecute = false;
                    p.StartInfo.Arguments = arguments;
                    //Substituting input and output paths
                    p.StartInfo.Arguments = p.StartInfo.Arguments.Replace("[i]", inputPath, StringComparison.OrdinalIgnoreCase).Replace("[o]", inputPath.Replace(args[1], args[2]), StringComparison.OrdinalIgnoreCase);
                    Directory.CreateDirectory(inputPath.Replace(args[1], args[2]).Substring(0, inputPath.LastIndexOf('\\') + 1)); //Creates folder if it does not exist
                    p.Start();
                    p.WaitForExit();
                }
            }
            catch (IndexOutOfRangeException) //Invalid amount of arguments given
            {
                Console.WriteLine("Error - invalid arguments\n");
                Help();
            }
            catch (IOException e)
            {
                Console.WriteLine("IO Error - " + e.Message);
            }
        }
    }
}
