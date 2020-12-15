using System;
using System.Diagnostics;
using System.IO;

namespace Multiple_File_CUI
{
    internal class Multiple_File_CUI
    {
        private static void Help()
        {
            Console.WriteLine("Runs multiple files in a given input folder (optionally including subfolders) including arguments through a executable file. Places output in given output path, keeping folder structure.\n" +
                            "Arguments:\n" +
                            "1\t-\tPath to Executable file\n" +
                            "2\t-\tPath to INPUT FILES\n" +
                            "3\t-\tPath to OUTPUT FOLDER\n" +
                            "4\t-\tY/N run on subfolders\n" +
                            "5..n\t-\tArguments\n" +
                            "\t\tTo include INPUT FILES use [i] where they will be subsituted into\n" +
                            "\t\tTo include OUTPUT FILES use [o] where they will be subsituted into\n" +
                            "\t\t(include - or / in front of argument if needed)\n" +
                            "\t\texample: .\\Multiple_File_CUI.exe .\\example.exe .\\input .\\output N -i [i] -o [o]\n");
        }

        private static void Main(string[] args)
        {
            try
            {
                if (args[0].ToLower().Equals("help"))
                {
                    Multiple_File_CUI.Help();
                    return;
                }

                string[] inputFiles = (args[3].ToLower()[0] == 'y') ? Directory.GetFiles(args[1], "*.*", SearchOption.AllDirectories) : Directory.GetFiles(args[1], "*.*", SearchOption.TopDirectoryOnly);

                foreach (string inputPath in inputFiles)
                {
                    Process p = new Process();
                    p.StartInfo.FileName = args[0];
                    p.StartInfo.UseShellExecute = false;
                    for (int i = 4; i < args.Length; i++)
                    {
                        switch (args[i])
                        {
                            case "[i]":
                                p.StartInfo.Arguments += " " + inputPath;
                                break;
                            case "[o]":
                                p.StartInfo.Arguments += " " + inputPath.Replace(args[1], args[2]);
                                Directory.CreateDirectory(inputPath.Replace(args[1], args[2]).Substring(0, inputPath.LastIndexOf('\\') + 1));
                                break;
                            default:
                                p.StartInfo.Arguments += " " + args[i];
                                break;
                        }
                    }
                    p.Start();
                    p.WaitForExit();
                }
            }
            catch (IndexOutOfRangeException)
            {
                Console.WriteLine("Error - invalid arguments\n");
                Multiple_File_CUI.Help();
            }
            catch (IOException e)
            {
                Console.WriteLine("IO Error - " + e.Message);
            }
        }
    }
}
