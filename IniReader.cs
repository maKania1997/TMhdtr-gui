using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using IniParser;
using IniParser.Model;


namespace TMhdtr_gui
{
    class IniReader
    {
        /// <summary>
        /// changes specific line in file
        /// </summary>
        /// <param name="newLine_content"></param>
        /// <param name="fileName"></param>
        /// <param name="number_of_line_to_edit"></param>
        /// <param name="forDiffVar"></param>
        /// <param name="key"></param>
        static public void lineChanger(string newLine_content, string fileName, int number_of_line_to_edit, bool forDiffVar = false, string key = "")
        {
            Encoding enc = Encoding.UTF8;
            string[] arrLine = File.ReadAllLines(fileName, enc);
            int newsize = arrLine.Length + 1;
            Array.Resize(ref arrLine, newsize);
            bool exists = false;
            if (forDiffVar)
            {
                for (int i = 0; i < arrLine.Length - 1; i++)
                {
                    if (arrLine[i] == "")
                    {
                        Console.WriteLine("line is null");
                        continue;
                    }
                    string line = arrLine[i];
                    if (arrLine[i] != newLine_content && arrLine[i] != "")
                    {

                        Console.WriteLine("Line is not null and not equal");
                        string output = "";
                        try
                        {
                            output = line.Substring(0, line.IndexOf('='));
                        }
                        catch
                        {
                            Console.WriteLine("Found [main]");
                        }
                        //Console.WriteLine("Line:" + line + "\n");
                        Console.WriteLine("Output:" + output + "\n");
                        Console.WriteLine("Key:" + key + "\n");
                        if (output == key)
                        {
                            Console.WriteLine("key is equal");
                            arrLine[i] = newLine_content;
                            exists = true;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("key is not equal");


                            continue;
                        }
                    }
                    else if (arrLine[i] == newLine_content)
                    {
                        Console.WriteLine("Line is equal");
                        exists = true;
                        arrLine[i] = newLine_content;
                        break;
                    }
                }
                if (!exists)
                {
                    arrLine[newsize - 1] = newLine_content;
                }

            }
            else
            {
                arrLine[number_of_line_to_edit - 1] = newLine_content;
            }
            File.WriteAllLines(fileName, arrLine);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileToRead"></param>
        /// <param name="section"></param>
        /// <param name="key"></param>
        /// <param name="new_value"></param>
        /// <param name="fileToWriteAll"></param>
        /// <param name="fileToWriteLine"></param>
        /// <param name="forDiffVar"></param>
        static public void setNewValue(string fileToRead, string section, string key, string new_value, string fileToWriteAll, string fileToWriteLine, bool forDiffVar = false)
        {
            Encoding UTF8 = Encoding.Default;
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(fileToRead, UTF8);
            string tmheadtrack_tracking_method = data[section][key];
            data[section][key] = new_value;
            parser.WriteFile(fileToWriteAll, data, UTF8);
            string line = key + "=" + new_value;
            if (forDiffVar)
            {
                lineChanger(line, fileToWriteLine, 2, true, key);

            }
            else
            {
                lineChanger(line, fileToWriteLine, 2);

            }
        }

        static public string getValue(string fileToRead, string section, string key)
        {
            Encoding UTF8 = Encoding.Default;
            var parser = new FileIniDataParser();
            IniData data = parser.ReadFile(fileToRead, UTF8);
            string value = data[section][key];
            return value;
        }

        static public void saveConfigUser(string fileToSave, string fileToCopy)
        {
            File.Copy(fileToCopy, fileToSave,true);
        }
    }
}
